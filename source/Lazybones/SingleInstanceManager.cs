using System;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;

namespace Lazybones
{
	public sealed class SingleInstanceManager : WindowsFormsApplicationBase
	{
		public SingleInstanceManager()
		{
			IsSingleInstance = true;
		}

		public App App { get; private set; }

		[STAThread]
		public static void Main(string[] args)
		{
			(new SingleInstanceManager()).Run(args);
		}

		protected override bool OnStartup(StartupEventArgs e)
		{
			App = new App();
			App.Run();

			return false;
		}

		protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
		{
			base.OnStartupNextInstance(eventArgs);

			App.ActivateMainWindow();
			App.ProcessArgs(eventArgs.CommandLine.ToArray(), false);
		}
	}
}