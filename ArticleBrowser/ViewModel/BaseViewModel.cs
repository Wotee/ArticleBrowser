using System.ComponentModel;
using System.Runtime.CompilerServices;

//using PropertyChanged;

namespace WordAddIn1.ViewModel
{
	/// <summary>
	/// A base view model that fires PropertyChanged events as needed
	/// </summary>
	public class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// The event that is fired when any child property changes its value
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#region EventHandling

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(storage, value)) return false;
			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}