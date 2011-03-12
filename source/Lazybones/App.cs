using System.Linq;
using System.Windows;

namespace Lazybones
{
	public class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			ProcessArgs(e.Args, true);

			MainWindow = new Main();
			MainWindow.Show();
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
			}
		}

		public void ActivateMainWindow()
		{
			MainWindow.Activate();
		}
	}
}