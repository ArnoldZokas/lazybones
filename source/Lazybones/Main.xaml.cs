using System;
using System.Threading;
using System.Threading.Tasks;
using Lazybones.Utils;
using System.Windows;

namespace Lazybones
{
	public partial class Main
	{
		public Main()
		{
			InitializeComponent();
			InitialiseUI();
		}

		private void InitialiseUI()
		{
			ImWorkingButton.AssociatedButtons.Add(ImRestingButton);
			ImWorkingButton.AssociatedButtons.Add(ImPlayingButton);
			ImWorkingButton.Click += ImWorkingButtonClickEventHandler;

			ImRestingButton.AssociatedButtons.Add(ImWorkingButton);
			ImRestingButton.AssociatedButtons.Add(ImPlayingButton);

			ImPlayingButton.AssociatedButtons.Add(ImWorkingButton);
			ImPlayingButton.AssociatedButtons.Add(ImRestingButton);
		}

		private void ImWorkingButtonClickEventHandler(object sender, RoutedEventArgs e)
		{
			var token = new CancellationToken();
			var task = new Task(() =>
			                    	{
			                    		while (!token.IsCancellationRequested)
			                    		{
			                    			Thread.Sleep(1000);
			                    			TimerDisplay.Invoke(x => x.Increment());
			                    		}
			                    	}, token);

			task.Start();
		}
	}
}