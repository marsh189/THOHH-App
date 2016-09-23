using UnityEngine;
using System.Collections;


namespace PaintCraft.Utils
{
	public class TextureUtil {
		public static void UpdateRenderTextureSize(RenderTexture texture, float width, float height){
			if (texture.width != width || texture.height != height)
			{
                texture.width = Mathf.CeilToInt(width);
				texture.height = Mathf.CeilToInt(height);
			}
            
		}


		public static RenderTexture SetupRenderTextureOnMaterial(Material mat, float width, float height){
			if (mat.mainTexture == null || !(mat.mainTexture is RenderTexture)){
				mat.mainTexture = CreateRenderTexture(width, height);
			} else {
				UpdateRenderTextureSize(mat.mainTexture as RenderTexture, width, height);
			}
			return mat.mainTexture as RenderTexture;
		}

		public static RenderTexture CreateRenderTexture(float width, float height){
			RenderTexture result = new RenderTexture(Mathf.CeilToInt(width), Mathf.CeilToInt(height), 0, RenderTextureFormat.ARGB32);
			return result;
		}

	}
}