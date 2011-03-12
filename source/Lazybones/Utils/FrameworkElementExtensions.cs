using System;
using System.Windows;

namespace Lazybones.Utils
{
	public static class FrameworkElementExtensions
	{
		public static void Invoke<T>(this T target, Action<T> action) where T : FrameworkElement
		{
			if (target == null) throw new ArgumentNullException("target");

			target.Dispatcher.Invoke(new Action(() => action(target)));
		}
	}
}