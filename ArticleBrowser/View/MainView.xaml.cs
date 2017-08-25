using WordAddIn1.ViewModel;

namespace WordAddIn1.View
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
