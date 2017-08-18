using System.ComponentModel;
using PropertyChanged;

namespace ArticleBrowserAddIn.Models.ViewModels.Base
{
	/// <summary>
	/// A base view model that fires PropertyChanged events as needed
	/// </summary>
	[ImplementPropertyChanged]
	public class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// The event that is fired when any child property changes its value
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
	}
}