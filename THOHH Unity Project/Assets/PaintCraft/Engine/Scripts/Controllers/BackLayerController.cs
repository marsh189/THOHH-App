using UnityEngine;
using PaintCraft.Utils;


namespace PaintCraft.Controllers
{
	public class BackLayerController : MonoBehaviour {
		public RenderTexture RenderTexture { get; private set;}
		CanvasController canvas;


		public void Init(CanvasController canvas){
			this.canvas = canvas;
			UpdateMeshSize();	
		}

		void UpdateMeshSize(){
			MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
			Mesh mesh = MeshUtil.CreatePlaneMesh(canvas.Width, canvas.Height);
			mf.mesh = mesh;
			MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);
			mr.material = canvas.BackLayerMaterial;
			RenderTexture = TextureUtil.SetupRenderTextureOnMaterial(mr.material, canvas.RenderTextureSize.x, canvas.RenderTextureSize.y);

		}
	}
}
