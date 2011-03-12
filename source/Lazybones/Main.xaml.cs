using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;
using Lazybones.Media;
using Lazybones.Utils;

namespace Lazybones
{
	public partial class Main
	{
		private readonly TimeSpan _oneSecond = new TimeSpan(0, 0, 1);
		private TimerMode _timerMode;

		public Main()
		{
			InitializeComponent();
			InitialiseTimer();
			InitialiseUI();
			InitialiseJumpList();

			Closing += WindowClosingEventHandler;
		}

		private static void InitialiseJumpList()
		{
			var tasks = new JumpList();
			
			var exitTask = new JumpTask();
			exitTask.Title = "Exit Lazybones";
			exitTask.Description = "Exits Lazybones application";
			var entryAssembly = Assembly.GetEntryAssembly();
			exitTask.ApplicationPath = entryAssembly.Location;
			exitTask.Arguments = "/q";
            tasks.JumpItems.Add(exitTask);

			tasks.Apply();
			JumpList.SetJumpList(Application.Current, tasks);
		}

		private void InitialiseTimer()
		{
			_timerMode = TimerMode.Rest;

			var timer = new DispatcherTimer();
			timer.Interval = _oneSecond;
			timer.Tick += (sender, e) =>
			              	{
			              		switch (_timerMode)
			              		{
			              			case TimerMode.Work:
			              				TimerDisplay.Invoke(x => x.Increment());
			              				break;
			              			case TimerMode.Play:
			              				TimerDisplay.Invoke(x => x.Decrement());
			              				break;
			              		}
			              	};
			timer.Start();
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
			ImPlayingButton.Click += ImPlayingButtonClickEventHandler;

			TimerDisplay.PlayTimeFinished += PlayTimeFinishedEventHandler;
		}

		private void ImWorkingButtonClickEventHandler(object sender, RoutedEventArgs e)
		{
			_timerMode = TimerMode.Work;
		}

		private void ImRestingButtonClickEventHandler(object sender, RoutedEventArgs e)
		{
			_timerMode = TimerMode.Rest;
			TimerDisplay.Invoke(x => x.ResetWorkTimer());
		}

		private void ImPlayingButtonClickEventHandler(object sender, RoutedEventArgs e)
		{
			_timerMode = TimerMode.Play;
			TimerDisplay.Invoke(x => x.ResetWorkTimer());
		}

		private void PlayTimeFinishedEventHandler(object sender, EventArgs e)
		{
			AudioPlayer.PlayPlayTimeOverNotificationTrack();
		}

		private void WindowClosingEventHandler(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			WindowState = WindowState.Minimized;
		}
	}
}