using System.Threading;
using System.Windows;
using Lazybones.Utils;

namespace Lazybones
{
	public partial class Main
	{
		private TimerMode _timerMode;

		public Main()
		{
			InitializeComponent();
			InitialiseTimer();
			InitialiseUI();
		}

		private void InitialiseTimer()
		{
			_timerMode = TimerMode.Rest;

			TimerCallback timerCallback = state =>
			                  	{
			                  		if (_timerMode == TimerMode.Work)
			                  		{
			                  			TimerDisplay.Invoke(x => x.Increment());
			                  		}
			                  	};

			new Timer(timerCallback, null, 0, 1000);
		}

		private void InitialiseUI()
		{
			ImWorkingButton.AssociatedButtons.Add(ImRestingButton);
			ImWorkingButton.AssociatedButtons.Add(ImPlayingButton);
			ImWorkingButton.Click += ImWorkingButtonClickEventHandler;

			ImRestingButton.AssociatedButtons.Add(ImWorkingButton);
			ImRestingButton.AssociatedButtons.Add(ImPlayingButton);
			ImRestingButton.Click += ImRestingButtonClickEventHandler;

			ImPlayingButton.AssociatedButtons.Add(ImWorkingButton);
			ImPlayingButton.AssociatedButtons.Add(ImRestingButton);
		}

		private void ImWorkingButtonClickEventHandler(object sender, RoutedEventArgs e)
		{
			_timerMode = TimerMode.Work;
		}

		private void ImRestingButtonClickEventHandler(object sender, RoutedEventArgs e)
		{
			_timerMode = TimerMode.Rest;
		}
	}
}