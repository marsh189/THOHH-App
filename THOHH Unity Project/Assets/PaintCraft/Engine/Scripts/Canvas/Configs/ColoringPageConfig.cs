﻿using System;
using PaintCraft.Utils;
using UnityEngine;

namespace PaintCraft.Canvas.Configs
{
    [CreateAssetMenu(menuName = "PaintCraft/ColoringPageConfig")]
    public class ColoringPageConfig : AdvancedPageConfig
    {

        [TexturePath] public string outlinePath;
        [TexturePath] public string RegionPath;
        [TexturePath] public string IconPath;

        [NonSerialized]
        Texture2D outlineTexture;

        public override Texture2D OutlineTexture
        {
            get
            {
                if (outlineTexture == null)
                {
                    outlineTexture = Resources.Load<Texture2D>(OutlinePath);
                }
                return outlineTexture;
            }
        }



        [NonSerialized]
        Texture2D regionTexture;
        public override Texture2D RegionTexture
        {
            get
            {
                if (regionTexture == null)
                {
                    regionTexture = Resources.Load<Texture2D>(RegionPath);
                }
                return regionTexture;
            }
        }

        public Sprite Icon
        {
            get { return Resources.Load<Sprite>(IconPath); }
        }

        public string OutlinePath
        {
            get
            {
                return outlinePath;
            }

            set
            {
                outlinePath = value;
            }
        }

        public override Vector2 GetSize()
        {
            if (OutlineTexture == null)
            {
                Debug.LogErrorFormat("Outline picture must be set at {0} , ResourcePath {1}", name, OutlinePath);
                return Vector2.zero;
            }
            else
            {
                return new Vector2(OutlineTexture.width, OutlineTexture.height);
            }
        }
    }
}
