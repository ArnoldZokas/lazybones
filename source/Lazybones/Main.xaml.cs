using System;
using System.Windows;

namespace Lazybones
{
	public partial class Main : Window
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

			ImRestingButton.AssociatedButtons.Add(ImWorkingButton);
			ImRestingButton.AssociatedButtons.Add(ImPlayingButton);

			ImPlayingButton.AssociatedButtons.Add(ImWorkingButton);
			ImPlayingButton.AssociatedButtons.Add(ImRestingButton);
		}
	}
}