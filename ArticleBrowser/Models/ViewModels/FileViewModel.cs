using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ArticleBrowserAddIn.Models.Data;
using ArticleBrowserAddIn.Models.Factories;
using ArticleBrowserAddIn.Models.ViewModels.Base;

namespace ArticleBrowserAddIn.Models.ViewModels
{
	/// <summary>
	/// A view model for files
	/// </summary>
	public class FileViewModel : BaseViewModel
	{
		#region Public properties
		public ObservableCollection<Item> Items { get; }
		#endregion

		#region Public Commands

		private ICommand OpenCommand { get; }
		#endregion

		internal FileViewModel(ProdStatus status = ProdStatus.Production)
		{
			IDataRetriever retriever = DataRetrieverFactory.GetNewDataRetriever(status);
			Items = new ObservableCollection<Item>(retriever.GetItems());
			OpenCommand = new RelayCommand(Open);
		}

		private void Open()
		{
			throw new NotImplementedException("Toimiiks tää ees");
		}
	}
}