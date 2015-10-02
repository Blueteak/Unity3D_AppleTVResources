using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.IO;

public class AppleTVIconsBuildPostProcess 
{

	static string contentJson = 
			@"{
			  ""images"" : [
			    {
			      ""idiom"" : ""tv"",
				  ""filename"" : ""filename.png"",
			      ""scale"" : ""1x""
			    }
			  ]
			}";
	static class BuildPostProcessor
	{

		[PostProcessBuild()]
		public static void OnPostprocessBuild (BuildTarget target, string pathToBuiltProject)
		{
			AppleTVIcons icons = Resources.Load<AppleTVIcons> ("AppleTVIcons");
			Debug.Log ("Path to built project: " + pathToBuiltProject);
			string brandAssetsPath = "/Unity-iPhone/Images.xcassets/AppIcon.brandassets";
			string smallImageStack = "/App Icon - Small.imagestack";
			string largeImageStack = "/App Icon - Large.imagestack";
			string topShelfImageStack = "/Top Shelf Image.imageset";
			string front = "/Front.imagestacklayer";
			string middle = "/Middle.imagestacklayer";
			string back = "/Back.imagestacklayer";
			string content = "/Content.imageset";

			// Create directories
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + smallImageStack + front + content);
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + smallImageStack + middle + content);
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + smallImageStack + back + content);
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + largeImageStack + front + content);
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + largeImageStack + middle + content);
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + largeImageStack + back + content);
			Directory.CreateDirectory (pathToBuiltProject + brandAssetsPath + topShelfImageStack);


			// Copy pre made json
			string pathToJson = Path.GetDirectoryName (AssetDatabase.GetAssetPath (icons));
			File.Copy (pathToJson + "/BrandAssetsContents.json", pathToBuiltProject + brandAssetsPath + "/Contents.json", true);
			File.Copy (pathToJson + "/ImageStackContents.json", pathToBuiltProject + brandAssetsPath + smallImageStack + "/Contents.json", true);
			File.Copy (pathToJson + "/ImageStackContents.json", pathToBuiltProject + brandAssetsPath + largeImageStack + "/Contents.json", true);

			// Images
			CopyImage (icons._SmallIconFront, pathToBuiltProject + brandAssetsPath + smallImageStack + front + content);
			CopyImage (icons._SmallIconMiddle, pathToBuiltProject + brandAssetsPath + smallImageStack + middle + content);
			CopyImage (icons._SmallIconBack, pathToBuiltProject + brandAssetsPath + smallImageStack + back + content);

			CopyImage (icons._LargeIconFront, pathToBuiltProject + brandAssetsPath + largeImageStack + front + content);
			CopyImage (icons._LargeIconMiddle, pathToBuiltProject + brandAssetsPath + largeImageStack + middle + content);
			CopyImage (icons._LargeIconBack, pathToBuiltProject + brandAssetsPath + largeImageStack + back + content);

			CopyImage (icons._TopShelfIcon, pathToBuiltProject + brandAssetsPath + topShelfImageStack);

			// Remove old style app icon
			if (Directory.Exists (pathToBuiltProject + "/Unity-iPhone/Images.xcassets/AppIcon.appiconset"))
			{
				Directory.Delete (pathToBuiltProject + "/Unity-iPhone/Images.xcassets/AppIcon.appiconset", true);
			}
		
		}

		static void CopyImage(Texture2D image, string targetPath)
		{
			if (image)
			{
				string pathToIcon = AssetDatabase.GetAssetPath (image);
				string iconFileName = Path.GetFileName (pathToIcon);
				File.Copy (pathToIcon, targetPath + "/" + iconFileName, true);
				string jsonOutput = contentJson.Replace ("filename.png", iconFileName);
				File.WriteAllText (targetPath + "/Contents.json", jsonOutput);
			}
		}
	}
}
