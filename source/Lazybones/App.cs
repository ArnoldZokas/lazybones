using System.Linq;
using System.Windows;

namespace Lazybones
{
	public class App : Application
	{
		public Main MyWindow { get; private set; }

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

			if (args.First().ToLowerInvariant() == CommandLineCodes.Exit)
				Shutdown();
		}

		public void ActivateMainWindow()
		{
			MainWindow.Activate();
		}
	}
}