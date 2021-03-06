using System;
using System.Windows.Controls;
using Lazybones.Properties;

namespace Lazybones.UI
{
	public sealed class TimerDisplay : WrapPanel
	{
		private readonly Settings _applicationSettings = Settings.Default;
		private readonly TimeSpan _oneSecondInterval = new TimeSpan(0, 0, 1);
		private TextBlock _playHourDisplay;
		private TextBlock _workHourDisplay;

		public static TimeSpan WorkTime { get; set; }
		public static TimeSpan PlayTime { get; set; }

		protected override void OnInitialized(EventArgs e)
		{
			_workHourDisplay = (TextBlock)Children[0];
			_playHourDisplay = (TextBlock)Children[1];

			base.OnInitialized(e);
		}

		public void ResetWorkTimer()
		{
			WorkTime = new TimeSpan();

			UpdateUI();
		}

		public void Increment()
		{
			WorkTime += _oneSecondInterval;

			if (WorkTime.TotalSeconds%_applicationSettings.WorkToPlayTimeRatio == 0)
				PlayTime += _oneSecondInterval;

			UpdateUI();
		}

		public void Decrement()
		{
			if (PlayTime.TotalSeconds == 0)
			{
				OnPlayTimeFinished(EventArgs.Empty);
			}
			else
			{
				PlayTime -= _oneSecondInterval;
			}

			UpdateUI();
		}

		private void UpdateUI()
		{
			_workHourDisplay.Text = string.Format(Properties.Resources.TimerFormatString, WorkTime);
			_playHourDisplay.Text = string.Format(Properties.Resources.TimerFormatString, PlayTime);
		}

		public event EventHandler<EventArgs> PlayTimeFinished;

		private void OnPlayTimeFinished(EventArgs e)
		{
			if (PlayTimeFinished != null)
				PlayTimeFinished(this, e);
		}
	}
}