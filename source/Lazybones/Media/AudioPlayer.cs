using System;
using System.IO;
using System.Media;

namespace Lazybones.Media
{
	public class AudioPlayer
	{
		public static void PlayNotificationTrack()
		{
			var windowsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
			var audioFilePath = Path.Combine(windowsFolderPath, "Media\\Windows Notify.wav");
			var player = new SoundPlayer(audioFilePath);
			player.Play();
		}
	}
}