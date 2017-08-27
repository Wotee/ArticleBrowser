using System.Windows;
using WordAddIn1.Model.DataHandler;
using WordAddIn1.ViewModel;

namespace WordAddIn1.Views
{
	/// <summary>
	/// Interaction logic for AddItemView.xaml
	/// </summary>
	public partial class AddItemView
	{
		public AddItemView(IDataHandler handler, string filePath)
		{
			InitializeComponent();
			DataContext = new AddItemViewModel(handler, filePath);
		}

		private void OnButtonOKClick(object sender, RoutedEventArgs e)
		{
			((AddItemViewModel)DataContext).AddCommand.Execute();
			this.Close();
		}

		private void OnButtonCancelClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
