using System;
using System.Windows.Input;

namespace ArticleBrowserAddIn.Models.ViewModels.Base
{
	/// <summary>
	/// A basic command that uns an Action
	/// </summary>
	public class RelayCommand : ICommand
	{
		#region Private Members

		private readonly Action _mAction;

		#endregion

		#region Public Events

		/// <summary>
		/// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
		/// </summary>
		public event EventHandler CanExecuteChanged = (sender, e) => { };

		#endregion

		#region Constructor

		public RelayCommand(Action action)
		{
			_mAction = action;
		}
		#endregion region

		#region Command Methods

		/// <summary>
		/// A relay command can always execute
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns>true</returns>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Executes the commands Action
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			_mAction();
		}

		#endregion
	}
}