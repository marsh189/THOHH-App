using UnityEngine;
using System;
using System.Collections.Generic;

namespace PaintCraft.Controllers
{
    public enum CameraOrientation:int{
        Horizontal,
        Horizontal180,
        Vertical90,
        VerticalMinus90
    }


    [Serializable]
    public class CameraSizeHandler 
    {
        public CameraOrientation Orientation;
        public Color    BackgroundColor = Color.gray;
        public Rect       ViewPortRect =  new Rect(0.0f, 0.0f, 1.0f, 1.0f); // First we calc this screen rect
        public RectOffset ViewPortOffset; // then we apply offset(positive values will reduce rects
        public Vector2 CanvasMargin = new Vector2(22.0f,22.0f);
        public float Elasticity = 0.4f;
        public float MaxSpeed = 1000.0f;
        CanvasController canvas;
        bool enabled = false;
        Camera camera;

        static Dictionary<CameraOrientation, Quaternion> rotationByOrientation 
            = new Dictionary<CameraOrientation, Quaternion>(){
            {CameraOrientation.Horizontal     , Quaternion.Euler(0.0f, 0.0f,   0.0f)},
            {CameraOrientation.Horizontal180  , Quaternion.Euler(0.0f, 0.0f, 180.0f)},
            {CameraOrientation.Vertical90     , Quaternion.Euler(0.0f, 0.0f,  90.0f)},
            {CameraOrientation.VerticalMinus90, Quaternion.Euler(0.0f, 0.0f, -90.0f)},
        };
   

        public void Init(Camera camera, CanvasController canvas){
            this.camera = camera;
            this.canvas = canvas;
            SetupCameraParameters();
        }

        void SetupCameraParameters(){               
            int cullingMask = camera.cullingMask;
            int layer= camera.gameObject.layer;
            camera.gameObject.layer = 0;
            camera.CopyFrom(canvas.CanvasCameraController.Camera);
            camera.cullingMask = cullingMask;
            camera.transform.position = canvas.CanvasCameraController.transform.position;
            camera.targetTexture = null;
            camera.ResetAspect ();
            camera.transform.rotation = rotationByOrientation[Orientation];
            camera.clearFlags = CameraClearFlags.Color;
            camera.backgroundColor = BackgroundColor;
            if (Orientation == CameraOrientation.Horizontal 
                || Orientation == CameraOrientation.Horizontal180){                
                camera.orthographicSize = canvas.CanvasCameraController.Camera.orthographicSize ; 
            } else {
                camera.orthographicSize = canvas.Width / 2.0f;
            }
            camera.gameObject.layer = layer;

            SetupCameraViewPort();
        }
            
        void SetupCameraViewPort(){
            camera.rect = ViewPortRect;
            Rect pixelRect = camera.pixelRect;
            pixelRect.x+=ViewPortOffset.left;
            pixelRect.y+= ViewPortOffset.bottom;
            pixelRect.width -= ViewPortOffset.horizontal;
            pixelRect.height -= ViewPortOffset.vertical;
            camera.pixelRect = pixelRect;

        }



        Vector3 targetPosition;
        private Vector3 velocity = Vector3.zero;
        public void LateUpdate()
        {
            
            if (camera.transform.position != targetPosition && enabled)
            {
                if (Vector3.Distance(camera.transform.position, targetPosition) < 1.0f)
                {
                    camera.transform.position = targetPosition;
                    enabled = false;
                }
                else
                {
                    camera.transform.position = Vector3
                        .SmoothDamp(camera.transform.position, targetPosition, ref velocity, Elasticity, MaxSpeed);
                }
            }
        }

        public void SetCameraNewOrthoSize(float newSize)
        {
            float camMaxSize = (Orientation == CameraOrientation.Horizontal || Orientation == CameraOrientation.Horizontal180)
                ? canvas.Height / 2.0f
                : canvas.Width / 2.0f;
            float camMinSize = camMaxSize / ((float)canvas.CamMaxZoomInPercent / 100.0f);
            camera.orthographicSize = Mathf.Clamp(newSize, camMinSize, camMaxSize);
        }

        public void ForceDisableMove()
        {
            enabled = false;
        }

        
        public void CheckBounds()
        {
            float camYTopLimit = canvas.transform.position.y + (canvas.Height/2.0f) + CanvasMargin.y - camera.orthographicSize;
            float camYBottomLimit = canvas.transform.position.y - (canvas.Height / 2.0f) - CanvasMargin.y + camera.orthographicSize;

            float camLeftLimit = canvas.transform.position.x - (canvas.Width/2.0f) - CanvasMargin.x +
                                 camera.orthographicSize*camera.aspect;
            float camRightLimit = canvas.transform.position.x + (canvas.Width / 2.0f) + CanvasMargin.x -
                                 camera.orthographicSize * camera.aspect;

            Vector3 camPosition = camera.transform.position;
            camPosition.x = Mathf.Clamp(camPosition.x, Mathf.Min(camLeftLimit, camRightLimit), Mathf.Max(camLeftLimit, camRightLimit));
            camPosition.y = Mathf.Clamp(camPosition.y, Mathf.Min(camYBottomLimit, camYTopLimit), Mathf.Max(camYBottomLimit, camYTopLimit));
            targetPosition = camPosition;
            velocity = Vector3.zero;
            enabled = targetPosition != camera.transform.position;
        }
    }
}
