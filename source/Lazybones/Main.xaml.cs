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
		private static Action _workModeActivator;
		private static Action _restModeActivator;
		private static Action _playModeActivator;
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
			var entryAssembly = Assembly.GetEntryAssembly();
			var applicationPath = entryAssembly.Location;
			var jumpList = new JumpList();

			// "I am... Working"
			var workModeSwitchTask = new JumpTask();
			workModeSwitchTask.Title = "Working";
			workModeSwitchTask.Description = "Switch to 'Work' mode";
			workModeSwitchTask.IconResourceIndex = -1;
			workModeSwitchTask.CustomCategory = "I am...";
			workModeSwitchTask.ApplicationPath = applicationPath;
			workModeSwitchTask.Arguments = CommandLineCodes.IAmWorking;
			jumpList.JumpItems.Add(workModeSwitchTask);

			// "I am... Resting"
			var restModeSwitchTask = new JumpTask();
			restModeSwitchTask.Title = "Resting";
			restModeSwitchTask.Description = "Switch to 'Rest' mode";
			restModeSwitchTask.IconResourceIndex = -1;
			restModeSwitchTask.CustomCategory = "I am...";
			restModeSwitchTask.ApplicationPath = applicationPath;
			restModeSwitchTask.Arguments = CommandLineCodes.IAmResting;
			jumpList.JumpItems.Add(restModeSwitchTask);

			// "I am... Playing"
			var playModeSwitchTask = new JumpTask();
			playModeSwitchTask.Title = "Playing";
			playModeSwitchTask.Description = "Switch to 'Play' mode";
			playModeSwitchTask.IconResourceIndex = -1;
			playModeSwitchTask.CustomCategory = "I am...";
			playModeSwitchTask.ApplicationPath = applicationPath;
			playModeSwitchTask.Arguments = CommandLineCodes.IAmPlaying;
			jumpList.JumpItems.Add(playModeSwitchTask);

			// "Exit"
			var exitTask = new JumpTask();
			exitTask.Title = "Exit";
			exitTask.Description = "Exit Lazybones";
			exitTask.IconResourceIndex = -1;
			exitTask.ApplicationPath = applicationPath;
			exitTask.Arguments = "/q";
			jumpList.JumpItems.Add(exitTask);

			jumpList.Apply();
			JumpList.SetJumpList(Application.Current, jumpList);
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
			_workModeActivator = () => ImWorkingButton.Activate();

			ImRestingButton.AssociatedButtons.Add(ImWorkingButton);
			ImRestingButton.AssociatedButtons.Add(ImPlayingButton);
			ImRestingButton.Click += ImRestingButtonClickEventHandler;
			_restModeActivator = () => ImRestingButton.Activate();

			ImPlayingButton.AssociatedButtons.Add(ImWorkingButton);
			ImPlayingButton.AssociatedButtons.Add(ImRestingButton);
			ImPlayingButton.Click += ImPlayingButtonClickEventHandler;
			_playModeActivator = () => ImPlayingButton.Activate();

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

		public static void SwitchToWorkMode()
		{
			_workModeActivator();
		}

		public static void SwitchToRestMode()
		{
			_restModeActivator();
		}

		public static void SwitchToPlayMode()
		{
			_playModeActivator();
		}
	}
}