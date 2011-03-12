using System;
using System.IO;
using System.Media;

namespace Lazybones.Media
{
	public static class AudioPlayer
	{
		public static void PlayModeChangeNotificationTrack()
		{
			PlayTrack("Media\\Windows Notify.wav", false);
		}

		public static void PlayPlayTimeOverNotificationTrack()
		{
			PlayTrack("Media\\Windows Battery Critical.wav");
		}

		private static void PlayTrack(string audiofileName, bool loop = true)
		{
			var windowsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
			var audioFilePath = Path.Combine(windowsFolderPath, audiofileName);
			var player = new SoundPlayer(audioFilePath);

			if (loop)
				player.PlayLooping();
			else
				player.Play();
		}
	}
}