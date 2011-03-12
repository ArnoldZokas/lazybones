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
		}

		public void ActivateMainWindow()
		{
			MainWindow.Activate();
		}
	}
}