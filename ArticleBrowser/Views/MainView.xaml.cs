using System.Windows;
using WordAddIn1.Model.DataHandler;
using WordAddIn1.ViewModel;

namespace WordAddIn1.Views
{
	/// <summary>
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView
	{
		public MainView()
		{
			InitializeComponent();
			DataContext = new FileViewModel();
		}

		private void MainView_OnDrop(object sender, DragEventArgs e)
		{
			// Check that dropped item was file.
			if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

			var files = e.Data.GetData(DataFormats.FileDrop) as string[];
			IDataHandler handler = ((FileViewModel)DataContext).GetDataHandler();
			foreach (var file in files ?? new string[] { })
			{
				var view = new AddItemView(handler, file);
				view.Show();
			}
		}

		private void MainView_OnDragEnter(object sender, DragEventArgs e)
		{
			BringIntoView();
		}
	}
}
