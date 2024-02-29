using System.IO;
using UnityEngine;

namespace Area730.MoreAppsPage
{
    class Utilswl
    {
        public const string MORE_APPS_FILENAME = "Area730_MoreApps.json";

        public static string GetSettingsFilePath()
        {
            return Path.Combine(Application.persistentDataPath, MORE_APPS_FILENAME);
        }

        public static bool ConfigFileExists()
        {
            return File.Exists(GetSettingsFilePath());
        }

        public static Texture2D LoadPNG(string filePath)
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
            }
            return tex;
        }

        public static Sprite SpriteFromTex2d(Texture2D tex)
        {
            Texture2D old   = tex;
            Texture2D left  = new Texture2D((int)(old.width), old.height, old.format, false);
            Color[] colors  = old.GetPixels(0, 0, (int)(old.width), old.height);
            left.SetPixels(colors);
            left.Apply();

            Sprite sprite   = Sprite.Create(left, new Rect(0, 0, left.width, left.height), new Vector2(0.5f, 0.5f), 40);
            return sprite;
        }

        public static string GetImagePath(int index)
        {
            return Path.Combine(Application.persistentDataPath, "MoreAppIcon_" + index + ".png");
        }

        public static Sprite GetSprite(int index)
        {
            return SpriteFromTex2d(LoadPNG(GetImagePath(index)));
        }


    }
}
