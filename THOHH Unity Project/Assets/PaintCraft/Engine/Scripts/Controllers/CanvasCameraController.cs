using UnityEngine;
using System.Collections.Generic;


namespace PaintCraft.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CanvasCameraController : MonoBehaviour {
		public Camera Camera {get; private set;}
        CanvasController canvasCtrl;
        public void Init(CanvasController canvas){
			Camera = GetComponent<Camera>();
            if (Camera == null){
                Camera = gameObject.AddComponent<Camera>();
            }

			Camera.orthographic = true;
			Camera.clearFlags = CameraClearFlags.Color;
			Camera.backgroundColor = Color.black;
			Camera.orthographicSize =  (float)canvas.Height / 2.0f;
			Camera.aspect = (float)canvas.Width / (float)canvas.Height;

			Camera.targetTexture = canvas.BackLayerController.RenderTexture;			
			Camera.clearFlags= CameraClearFlags.Nothing;
            canvasCtrl = canvas;
		}
              
        bool clearTexture = false;
        System.Action onClearDone;
        public void ClearRenderTexture(System.Action onClearDone){
            clearTexture = true;
            this.onClearDone = onClearDone;
        }

        void OnPostRender(){
            if (clearTexture){
                clearTexture = false;
                ClearTexture() ;   
            }
        }

        void ClearTexture(){
            AnalyticsWrapper.CustomEvent("ClearCanvas", new Dictionary<string, object>());
            RenderTexture currentRT  = RenderTexture.active;
            RenderTexture.active = Camera.targetTexture;         
            Camera.targetTexture.DiscardContents();
            GL.Clear(false, true, canvasCtrl.DefaultBGColor);
            RenderTexture.active = currentRT;
            if (onClearDone != null){
                onClearDone();
                onClearDone = null;
            }
        }
    }
}
