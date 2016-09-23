using UnityEngine;
using PaintCraft.Controllers;
using NodeInspector;

namespace PaintCraft.Tools.Filters
{
    [NodeMenuItem("Camera/CameraControllFilter")]
    public class CameraControllFilter : FilterWithNextNode
    {        
        public float ScreenMagnitudeToCameraStepRatio = 0.5f;
        Vector2 startPoint;
        float startMagnitude;
        float originalCamSize;

        public override bool FilterBody(BrushContext brushLineContext)
        {
            ScreenCameraController inputHandler = brushLineContext.SourceInputHandler;
            int firstId = int.MaxValue;
            foreach (var keyId in inputHandler.ContextByTouchId.Keys)
            {
                if (keyId < firstId)
                {
                    firstId = keyId;
                }        
            }

            if (firstId == int.MaxValue)
            {
                Debug.LogWarning("can't find proper touch info");
                return false;
            }


            if (brushLineContext != inputHandler.ContextByTouchId[firstId])
            {
                return false; //we could get several touches here, so we just ignore another touch info and handle all input touches just once in context with smalles touchId
            }


            int nextId = int.MaxValue;
            foreach (var keyId in inputHandler.ContextByTouchId.Keys)
            {
                if (keyId < nextId && keyId > firstId)
                {
                    nextId = keyId;
                }
            }


            //handle move
            Vector2 currentPoint = brushLineContext.Points.Last.Value.Position;
            Vector2 currentScreenPosition = brushLineContext.Points.Last.Value.ScreenPosition;

            if (brushLineContext.Points.Last.Value.BasePointId == 0)
            {
                startPoint = currentPoint;
                brushLineContext.SourceInputHandler.CameraSize.ForceDisableMove();
            }
            else
            {
                if (nextId == int.MaxValue) // only move with single touch
                {
                    MoveCameraAccordingToTwoGlobalPosition(startPoint, currentPoint, inputHandler);
                    if (nextId == int.MaxValue && brushLineContext.IsLastPointInLine)
                    {                       
                        brushLineContext.SourceInputHandler.CameraSize.CheckBounds();
                    }
                }
            }
            
            //handle size
            if (nextId != int.MaxValue)
            {
                BrushContext nextPointContext = inputHandler.ContextByTouchId[nextId];
                if (nextPointContext != null) // we need this check because second touch could terminate before first one
                {                                        
                    Vector2 nextScreenPosition = nextPointContext.Points.Last.Value.ScreenPosition;
                    float currentMagnitude = Vector2.Distance(currentScreenPosition, nextScreenPosition);
                    if (nextPointContext.Points.Last.Value.BasePointId == 0)
                    {
                        startMagnitude = currentMagnitude;                       
                        originalCamSize = inputHandler.Camera.orthographicSize;
                    }
                    else
                    {                        
                        float newSize = originalCamSize + (startMagnitude - currentMagnitude) * ScreenMagnitudeToCameraStepRatio;
                        inputHandler.CameraSize.SetCameraNewOrthoSize(newSize);                      
                    }
                }
                if (brushLineContext.IsLastPointInLine)
                {
                    brushLineContext.SourceInputHandler.CameraSize.CheckBounds();
                }
            }

            return true;
        }

        void MoveCameraAccordingToTwoGlobalPosition(Vector2 originalPoint, Vector2 currentPoint, ScreenCameraController inputHandler)
        {
            Vector2 currentCamPosition = inputHandler.Camera.WorldToScreenPoint(currentPoint);
            Vector2 oldCamPosition = inputHandler.Camera.WorldToScreenPoint(originalPoint);
            Vector2 diff = oldCamPosition - currentCamPosition;
            inputHandler.transform.Translate(diff);
        }

    }
}