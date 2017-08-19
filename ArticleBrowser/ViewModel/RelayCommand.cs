using System;
using System.Windows.Input;

namespace WordAddIn1.ViewModel
{
	/// <summary>
	/// A basic command that uns an Action
	/// </summary>
	public class RelayCommand : ICommand
	{
		#region Private Members

		private readonly Action<object> _actionWithParameters;
		private readonly Action _actionWithOutParameters;

		#endregion

		#region Public Events

		/// <summary>
		/// The event that's fired when the <see cref="CanExecute(object)"/> value has changed
		/// </summary>
		public event EventHandler CanExecuteChanged = (sender, e) => { };

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for Command with parameters.
		/// </summary>
		/// <param name="action"></param>
		public RelayCommand(Action<object> action)
		{
			_actionWithParameters = action;
		}

		/// <summary>
		/// Constructor for parameterless Command
		/// </summary>
		/// <param name="action"></param>
		public RelayCommand(Action action)
		{
			_actionWithOutParameters = action;
		}
		#endregion region

		#region Command Methods

		/// <summary>
		/// A relay command can always execute
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns>true</returns>
		public bool CanExecute(object parameter = null) => true;

		/// <summary>
		/// Executes the commands Action without parameters
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute()
		{
			_actionWithOutParameters();
		}

		/// <summary>
		/// Executes the command Action with parameters
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			_actionWithParameters(parameter);
		}

		#endregion
	}
}