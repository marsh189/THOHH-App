using UnityEngine;
using UnityEngine.EventSystems;
using PaintCraft.Canvas;
using PaintCraft.Tools;
using System.Collections.Generic;
using UnityEngine.Serialization;
using PaintCraft;

namespace PaintCraft.Controllers{    
	[RequireComponent(typeof(Camera))]
    public class ScreenCameraController : MonoBehaviour {		
        
		public LineConfig LineConfig;
		public CanvasController Canvas;
        public Camera Camera { get; private set; }
        public bool ZoomOnMouseScroll = false;
        [FormerlySerializedAs("CameraBounds")]
        public CameraSizeHandler CameraSize;

        Dictionary<int, BrushContext> contextByTouchId  = new Dictionary<int, BrushContext>();
        public Dictionary<int, BrushContext> ContextByTouchId
        {
            get { return contextByTouchId; } 
        }

        void Start(){
          
            if (Canvas == null){
                Debug.LogError("you have to provide link to the Canvas for this component", gameObject);
            }

            Camera = GetComponent<Camera>();
            if (Camera == null){
                Debug.Log("you have to add camera component to this object", gameObject);
            }

            CameraSize.Init(Camera, Canvas);          
        }


	    void Update()
	    {            
            HandleZoomOnScroll();
            if (!HandleTouch()){                
                HandleMouseEvents();
            }
            CameraSize.LateUpdate();
        }

        void LateUpdate(){
            
        }

        void HandleZoomOnScroll()
        {
            if (ZoomOnMouseScroll && Camera.pixelRect.Contains(Input.mousePosition))
            {                
                float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel") * 100.0f;
                if (mouseScrollWheel != 0.0f)
                {
                    CameraSize.SetCameraNewOrthoSize(Camera.orthographicSize - mouseScrollWheel );
                    CameraSize.CheckBounds();
                }
            }
        }

        /// <summary>
        /// Handles the touch.
        /// </summary>
        /// <returns><c>true</c>, if touch was handled, <c>false</c> otherwise.</returns>
        bool HandleTouch(){
            if (Input.touchCount > 0){
                foreach (var touch in Input.touches)
                {                    
                    switch(touch.phase){
                        case TouchPhase.Began:
                            HandleTouchesBegan(touch.fingerId, touch.position);
                            break;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            Debug.Log(touch.phase);
                            HandleTouchesCancelledOrEnded(touch.fingerId, touch.position);
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            HandleTouchesMoved(touch.fingerId, touch.position);
                            break;
                    }
                }

                return true;
            }
            return false;
        }

        void HandleMouseEvents()
        {            
            if (Input.GetMouseButtonDown(0)){
                HandleTouchesBegan(0,Input.mousePosition);
            } else if (Input.GetMouseButton(0)){
                HandleTouchesMoved(0, Input.mousePosition);
            } else if (Input.GetMouseButtonUp(0)){                
                HandleTouchesCancelledOrEnded(0, Input.mousePosition);
            }
        }

	    


        void HandleTouchesCancelledOrEnded (int touchId, Vector2 touchScreenPosition)
		{           
            if (!contextByTouchId.ContainsKey(touchId)){
                return; //initiated on different camera
            }   
            BrushContext bc = contextByTouchId[touchId];
			
            AnalyticsWrapper.CustomEvent("TouchEnbded", new Dictionary<string, object>
              {
                { "HandlerName", gameObject.name},
                { "ToolName", LineConfig.Brush.name },
                { "TouchId", touchId}/*,
                { "TotalTouch", e.Touches.Count}*/
              });

            AddNewPointToBrushContext(touchScreenPosition, bc);
         
            bc.ApplyFilters(true);
    			
            contextByTouchId.Remove(touchId);
			if (ContextByTouchId.Count == 0){
				MakeSnapshot();  
			}
		}


        void HandleTouchesMoved (int touchId, Vector2 touchScreenPosition)
		{          
            if (!contextByTouchId.ContainsKey(touchId)){
                return; //initiated on different camera
            }   
            BrushContext bc = contextByTouchId[touchId];
            AddNewPointToBrushContext(touchScreenPosition, bc);
            bc.ApplyFilters(false);
        }


        void HandleTouchesBegan (int touchId, Vector2 touchScreenPosition)
		{
            
            if (EventSystem.current == null){
                Debug.LogError("you have to add event system to the scene. e.g. from Unity UI");
                return;
            } 

            if (!Camera.pixelRect.Contains(touchScreenPosition) 
                || EventSystem.current.IsPointerOverGameObject()){
                return; // handle on different camera or ignore
            }

            if (ContextByTouchId.ContainsKey(touchId)){
                HandleTouchesCancelledOrEnded(touchId, touchScreenPosition);
            }
                
            AnalyticsWrapper.CustomEvent("TouchBegan", new Dictionary<string, object>
              {
                { "HandlerName", gameObject.name},
                { "ToolName", LineConfig.Brush.name },
                { "TouchId", touchId}/*,
                { "TotalTouch", e.Touches.Count}*/
              });

            BrushContext bc =  new BrushContext(Canvas, LineConfig, this);				
			if (ContextByTouchId.Count == 0){
				StoreStateBeforeSnapshot();
			}
            ContextByTouchId.Add(touchId, bc);
            bc.ResetBrushContext();

            AddNewPointToBrushContext(touchScreenPosition, bc);				
            bc.ApplyFilters(false);            	
		}

		void AddNewPointToBrushContext (Vector2 position, BrushContext brushContext)
		{
			if (Input.touchCount < 2)
			{
				Vector3 screenPosition = position;
				screenPosition.z = transform.position.z;
				Vector3 worldPoint = Camera.ScreenToWorldPoint (screenPosition);
				brushContext.AddPoint (worldPoint, screenPosition);
			}
		}

		SnapshotCommand snapCommand;
		public void StoreStateBeforeSnapshot(){
			snapCommand = new SnapshotCommand(Canvas.UndoManager);
			snapCommand.BeforeCommand();
		}

		void MakeSnapshot(){
			snapCommand.AfterCommand();
			Canvas.UndoManager.AddNewCommandToHistory(snapCommand);
            Canvas.SaveChangesToDisk();
		}                   
	}
}
