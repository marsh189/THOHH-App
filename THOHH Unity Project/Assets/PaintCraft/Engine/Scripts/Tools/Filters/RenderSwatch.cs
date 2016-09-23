using UnityEngine;
using PaintCraft.Tools;
using System.Collections.Generic;
using PaintCraft.Utils;
using NodeInspector;


namespace PaintCraft.Tools.Filters{


	[NodeMenuItem("Renderer/RenderSwatch")]
    public class RenderSwatch : FilterWithNextNode {
        public Material NormalMaterial;
        public Material RegionMaterial;        


		int lastUsedFrame = -1;

		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
			LinkedListNode<Point> point = null;

			if (Time.frameCount != lastUsedFrame){
				FlushUsedMeshes();
			}
			point = brushLineContext.Points.Last;
			Mesh mesh;
            Material mat;
			while (point != null ){
				if (!point.Value.IsBasePoint){
                    Vector3 pointPosition = point.Value.Position;
                    pointPosition.z = brushLineContext.Canvas.transform.position.z + brushLineContext.Canvas.BrushOffset;
                    mesh = GetMesh(brushLineContext, point.Value);
                    mat = GetMaterial(brushLineContext);
                    if (point.Value.Status == PointStatus.ReadyToApply){                                                
                        Graphics.DrawMesh(mesh, pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), mat, 
                            brushLineContext.Canvas.BrushLayerId, brushLineContext.Canvas.CanvasCameraController.Camera);                       
						point.Value.Status = PointStatus.CopiedToCanvas;                                 
                        usedMeshes.Enqueue(mesh);
					} else if (point.Value.Status == PointStatus.Temporary){                                                						
                        Graphics.DrawMesh(mesh, pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), mat, 
                            brushLineContext.Canvas.TempRenderLayerId, brushLineContext.SourceInputHandler.Camera);
						usedMeshes.Enqueue(mesh);
					}
				}

				point = point.Previous;
			}

            if (brushLineContext.IsLastPointInLine){
                meshPool.Clear();
                usedMeshes.Clear();
            }
			return true;
		}

        Queue<Mesh> meshPool = new Queue<Mesh>();
        Queue<Mesh> usedMeshes = new Queue<Mesh>();
		Mesh GetMesh(BrushContext brushLineContext, Point point){
			float width = point.Size.x * point.Scale;
			float height = point.Size.y * point.Scale;
			width = Mathf.Max(brushLineContext.Brush.MinSize.x, width);
			height = Mathf.Max(brushLineContext.Brush.MinSize.y, height);
			Mesh result;
			if (meshPool.Count > 0){
				result = meshPool.Dequeue();
				MeshUtil.ChangeMeshSize(result, width, height);
			} else {
                result = MeshUtil.CreatePlaneMesh(width, height);
            }
			MeshUtil.ChangeMeshColor(result,  point.PointColor.Color);
            MeshUtil.UpdateMeshUV2(result, width, height, point.Position, point.Rotation, (float)brushLineContext.Canvas.Width, (float)brushLineContext.Canvas.Height, brushLineContext.Canvas.transform.position);
			return result;

		}

		void FlushUsedMeshes(){            
			while (usedMeshes.Count > 0){
				meshPool.Enqueue(usedMeshes.Dequeue());
			}
            lastUsedFrame = Time.frameCount;
		}

		#endregion


        Material GetMaterial(BrushContext brushLineContext){            
            if (brushLineContext.Canvas.RegionTexture != null){
                if (RegionMaterial != null){                    
                    return GetCachedMaterial(brushLineContext, RegionMaterial, brushLineContext.Canvas.RegionTexture);
                } else {
                    Debug.LogError("You didn't specified regionlayer material for this brush",this);
                    return GetCachedMaterial(brushLineContext, NormalMaterial);
                }
            } else {
                return GetCachedMaterial(brushLineContext, NormalMaterial);
            }
        }

        const int materialCacheSize = 5;
        static Dictionary<int, Material> materialCache = new Dictionary<int, Material>();
        static Queue<int>  materialAddOrder = new Queue<int>();
        Material GetCachedMaterial(BrushContext brushContext, Material parentMaterial, Texture regionTexture = null){ 
            Material result;
            int cacheId = (parentMaterial.GetInstanceID() * 31
                + (regionTexture == null ? 0 : regionTexture.GetInstanceID()*17) ^ brushContext.LineConfig.GetInstanceID());            
            materialCache.TryGetValue(cacheId, out result);
            if (result == null ){                
                result = new Material(parentMaterial);
                materialCache.Add(cacheId, result);
                materialAddOrder.Enqueue(cacheId);
                if (materialCache.Count > materialCacheSize){
                    materialCache.Remove(materialAddOrder.Dequeue());
                }
            }           

            if (regionTexture != null && (brushContext.IsFirstPointInLine || brushContext.IsLastPointInLine)){ //First and last for brush and bucket. maybe need to avoid this. and set this param every time?                

                result.SetTexture("_RegionTex", regionTexture);
                result.SetFloat("_OriginX", brushContext.FirstPointUVPosition.x);
                result.SetFloat("_OriginY", brushContext.FirstPointUVPosition.y);               
            }

            if (brushContext.ClippingMaskOffset != Vector2.zero && result.HasProperty("_ClippingMask")){
                result.SetTextureOffset("_ClippingMask", brushContext.ClippingMaskOffset );
            }

            return result;
        }
	}
}
