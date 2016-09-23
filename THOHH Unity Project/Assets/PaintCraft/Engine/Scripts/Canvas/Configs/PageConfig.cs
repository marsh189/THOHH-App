using System.IO;
using UnityEngine;

namespace PaintCraft.Canvas.Configs{	
	public abstract class PageConfig : ScriptableObject
	{
	    public string UniqueId;

	    public string IconSavePath
	    {
	        get
	        {
	            string dir = Path.Combine( Application.persistentDataPath , "icons");
	            if (!Directory.Exists(dir))
	            {
	                Directory.CreateDirectory(dir);
	            }
	            return Path.Combine(dir, UniqueId + ".jpg");
	        }
	    }
	    abstract public Vector2 GetSize();
	}
}
