using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PaintCraft.Utils;
using PaintCraft.Canvas;
using PaintCraft.Canvas.Configs;
using PaintCraft;


namespace PaintCraft.Controllers{

    public class CanvasController : MonoBehaviour
    {		
		public float Width {get; private set;}
		public float Height {get; private set;}
		public Vector2 Size{ 
			get{
				return new Vector2((float) Width, (float) Height);
			}
		}

        public float CamMaxZoomInPercent = 500.0f;

        public PageConfig PageConfig;

        [HideInInspector]
		public Texture2D OutlineTexture;
        [HideInInspector]
		public Texture2D RegionTexture;
		public float OutlineLayerOffset = -25.0f;
       
		[HideInInspector]
        public BackLayerController BackLayerController;
		[HideInInspector]
		public OutlineLayerController OutlineLayerController;

		public Material               BackLayerMaterial;		
        public CanvasCameraController CanvasCameraController;
		public float BackLayerOffset = 100.0f;

		
		public Material OutlineMaterial;
		public float BrushOffset = 50.0f;
		public int BrushLayerId = 9;
		public int TempRenderLayerId = 10;
		public UndoManager UndoManager;

        public int HistorySize = 10;

        public int PreviewIconWidth = 440;
        public int PreviewIconHeight = 330;

		public Color DefaultBGColor = Color.black;       	

        public Vector2 RenderTextureSize { get; private set; }
        public bool ForceClearOnStart = false;


		void Awake(){           

            if (PageConfig == null){
                Debug.LogError("You have to provide page config for this component", gameObject);
                return;
            }

		    if (AppData.SelectedPageConfig != null)
		    {
		        PageConfig = AppData.SelectedPageConfig;
		    }

		    Width = PageConfig.GetSize().x;
		    Height = PageConfig.GetSize().y;

		    if (Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WSAPlayerARM ||
                Application.platform == RuntimePlatform.IPhonePlayer)
		    {
                //Debug.LogFormat("Scren size: {0} {1}", Screen.width, Screen.height);
		        float maxWidth = Mathf.Max(Screen.width, Screen.height);
		        if (maxWidth < (float) Width)
		        {
                    Debug.Log("im on slow mobile");
		            float maxHeight = maxWidth * (float) Height/(float) Width;
		            RenderTextureSize = new Vector2(maxWidth, maxHeight);
		            HistorySize = 5;
                } else {
                    RenderTextureSize = new Vector2(Width, Height);
                }	       
		    }
		    else
		    {
                RenderTextureSize = new Vector2(Width, Height);
            }



            if (PageConfig is AdvancedPageConfig)
		    {
                OutlineTexture = (PageConfig as AdvancedPageConfig).OutlineTexture;
                RegionTexture = (PageConfig as AdvancedPageConfig).RegionTexture;               
		    }


			if (BackLayerController == null){
				BackLayerController = GOUtil.CreateGameObject<BackLayerController>("BackLayer", gameObject, BackLayerOffset);
			}


			if (OutlineLayerController == null && OutlineMaterial != null && OutlineTexture != null){
				OutlineLayerController = GOUtil.CreateGameObject<OutlineLayerController>("Outline", gameObject, OutlineLayerOffset);
				OutlineLayerController.gameObject.layer = 0;
				OutlineLayerController.Init(this);
			}

			BackLayerController   .Init(this);
			BackLayerController.gameObject.layer = 0;
            CanvasCameraController.Init(this);

            UndoManager = new UndoManager(this, HistorySize);		
        }
                
        IEnumerator Start(){            
            if (ForceClearOnStart){
                yield return null;
                ClearCanvas();
            }
        }

        public void ClearCanvas(){            
            CanvasCameraController.ClearRenderTexture(()=>SaveChangesToDisk());
		}

		public void Undo(){
			UndoManager.Undo();
		}

		public void Redo(){
			UndoManager.Redo();
		}


        string SaveDirectory
        {
            get
            {
                string dir = Path.Combine( Application.persistentDataPath , "Saves");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;
            }
        }

        string SaveFilePath
        {
            get
            {
                return Path.Combine( SaveDirectory ,PageConfig.UniqueId + ".jpg");
            }
        }

        Texture2D tmpTexture2D;

        public Texture2D TmpTexture2D
        {
            get
            {
                if (tmpTexture2D == null)
                {
                    tmpTexture2D = new Texture2D((int)RenderTextureSize.x, (int)RenderTextureSize.y, TextureFormat.RGB24, false);
                }
                return tmpTexture2D;
            }
        }

        Texture2D tmpTextureIcon2D;

        public Texture2D TmpTextureIcon2D
        {
            get
            {
                if (tmpTextureIcon2D == null)
                {
                    tmpTextureIcon2D = new Texture2D(PreviewIconWidth
                        , PreviewIconHeight, TextureFormat.RGB24, false);
                }
                return tmpTextureIcon2D;
            }
        }

        public void SaveChangesToDisk()
        {
            StartCoroutine(DoSaveChangesToDisk());
        }

            

        IEnumerator DoSaveChangesToDisk()
        {
            yield return new WaitForEndOfFrame();
            RenderTexture tmp = RenderTexture.active;
            RenderTexture.active = BackLayerController.RenderTexture;
            
            TmpTexture2D.ReadPixels(new Rect(0,0,RenderTextureSize.x, RenderTextureSize.y),0,0,false );
            
            File.WriteAllBytes(SaveFilePath, TmpTexture2D.EncodeToJPG(100));

            RenderTexture downscaledRT = RenderTexture.GetTemporary(440, 330);
            Graphics.Blit(CanvasCameraController.Camera.targetTexture, downscaledRT);
            RenderTexture.active = downscaledRT;

            TmpTextureIcon2D.ReadPixels(new Rect(0, 0, 440, 330), 0, 0, false);
            System.IO.File.WriteAllBytes(PageConfig.IconSavePath, TmpTextureIcon2D.EncodeToJPG(100));
            RenderTexture.ReleaseTemporary(downscaledRT);
            RenderTexture.active = tmp;
        }

        public bool LoadFromDiskOrClear()
        {
            if (File.Exists(SaveFilePath) && !string.IsNullOrEmpty(PageConfig.name))
            {
                if (TmpTexture2D.LoadImage(File.ReadAllBytes(SaveFilePath)))
                {
                    CanvasCameraController.Camera.targetTexture.DiscardContents();
                    Graphics.Blit(TmpTexture2D, CanvasCameraController.Camera.targetTexture);
                    return true;
                }
            }
            
            ClearCanvas();
            return false;                        
        }

        void Update()
        {
            HandleChangeScreenSize();
        }

        int oldWIdth = -1, oldHeight = -1;

        void HandleChangeScreenSize()
        {
            if (Screen.width != oldWIdth || Screen.height != oldHeight)
            {
                oldWIdth = Screen.width;
                oldHeight = Screen.height;
                LoadFromDiskOrClear();
            }
        }


    }




    public enum CanvasSizeType{
		ScreenSize,
		FixedSize,
		OutlineImageSize
	}
}
