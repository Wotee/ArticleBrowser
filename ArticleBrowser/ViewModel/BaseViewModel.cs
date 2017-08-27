using System.ComponentModel;
using System.Runtime.CompilerServices;
using WordAddIn1.Model.DataHandler;

namespace WordAddIn1.ViewModel
{
	/// <summary>
	/// A base view model that fires PropertyChanged events as needed
	/// </summary>
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// The event that is fired when any child property changes its value
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		protected IDataHandler DataHandler;

		public IDataHandler GetDataHandler()
		{
			return DataHandler;
		}


		#region EventHandling
		/// <summary>
		/// Generic method for updating property and launching event
		/// </summary>
		/// <typeparam name="T">Type of updated property</typeparam>
		/// <param name="storage">Current value of the property</param>
		/// <param name="value">New value of the property</param>
		/// <param name="propertyName">Name of the property</param>
		/// <returns>True</returns>
		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(storage, value)) return false;
			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		/// <summary>
		/// Event for PropertyChange
		/// </summary>
		/// <param name="propertyName">Name of the property</param>
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}