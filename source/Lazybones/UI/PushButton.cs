using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Lazybones.Media;
using Lazybones.Utils;

namespace Lazybones.UI
{
	public sealed class PushButton : Button
	{
		public PushButton()
		{
			AssociatedButtons = new List<PushButton>();
		}

		public IList<PushButton> AssociatedButtons { get; private set; }

		protected override void OnClick()
		{
			// enable associated buttons
			AssociatedButtons.Where(button => button != this).Apply(button => button.IsEnabled = true);

			IsEnabled = false;
			AudioPlayer.PlayNotificationTrack();

			base.OnClick();
		}
	}
}