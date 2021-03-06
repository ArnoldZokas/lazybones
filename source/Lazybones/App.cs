using System.Linq;
using System.Windows;

namespace Lazybones
{
	public class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = new Main();
			MainWindow.Show();

			ProcessArgs(e.Args, true);
		}

		public void ProcessArgs(string[] args, bool firstInstance)
		{
			if (args.Length == 0)
				return;
			
			switch (args.First().ToLowerInvariant())
			{
				case CommandLineCodes.Exit:
					Shutdown();
					break;
				case CommandLineCodes.IAmWorking:
					Main.SwitchToWorkMode();
					break;
				case CommandLineCodes.IAmResting:
					Main.SwitchToRestMode();
					break;
				case CommandLineCodes.IAmPlaying:
					Main.SwitchToPlayMode();
					break;
			}
		}

		public void ActivateMainWindow()
		{
			MainWindow.Activate();
		}
	}
}