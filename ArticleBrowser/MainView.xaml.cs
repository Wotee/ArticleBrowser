using System.Windows.Controls;
using ArticleBrowserAddIn.Models.ViewModels;

namespace ArticleBrowserAddIn
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
	}
}
