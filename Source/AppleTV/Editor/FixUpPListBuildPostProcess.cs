using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.IO;
using System.Diagnostics;

public class FixPlistBuildPostProcess 
{
	static class BuildPostProcessor
	{
		[PostProcessBuild()]
		public static void OnPostprocessBuild (BuildTarget target, string pathToBuiltProject)
		{
			// Add game controller capabilities
			RunPlistBuddyCommand(pathToBuiltProject, "Delete :GCSupportedGameControllers");
			RunPlistBuddyCommand(pathToBuiltProject, "Delete :GCSupportsControllerUserInteraction");
			RunPlistBuddyCommand(pathToBuiltProject, "Add :GCSupportedGameControllers array");

			// Add additional game controller types here
			RunPlistBuddyCommand(pathToBuiltProject, "Add :GCSupportedGameControllers:0:ProfileName string 'MicroGamepad'");

			RunPlistBuddyCommand(pathToBuiltProject, "Add :GCSupportsControllerUserInteraction bool true");

			// Fix up required device capabilities
			RunPlistBuddyCommand(pathToBuiltProject, "Set :UIRequiredDeviceCapabilities:0 'arm64'");
        }

		static void RunPlistBuddyCommand(string workingDirectory, string arguments)
		{
			ProcessStartInfo proc = new ProcessStartInfo ("/usr/libexec/PlistBuddy", "Info.plist -c \"" + arguments + "\"");
			proc.WorkingDirectory = workingDirectory;
			proc.UseShellExecute = false;
			Process.Start(proc);
        }

	}
}
