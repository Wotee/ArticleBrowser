using System;
using WordAddIn1.Misc;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;

namespace ArticleBrowserAddIn
{
	public partial class ArticleBrowserAddIn
	{
		private CustomTaskPane _myTaskPane;
		private ControlHost _host;

		private void ArticleBrowserAddIn_Startup(object sender, EventArgs e)
		{
			// Load the WPF ControlHost to be the design surface for WPF UI
			_host = new ControlHost();
			_host.Form1_Load(this, EventArgs.Empty);
			// Add the name of the plugin
			_myTaskPane = CustomTaskPanes.Add(_host, "ArticleBrowser");
			_myTaskPane.Visible = true;
		}

		private void ArticleBrowserAddIn_Shutdown(object sender, EventArgs e)
		{
		}

		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
		{
			Startup += ArticleBrowserAddIn_Startup;
			Shutdown += ArticleBrowserAddIn_Shutdown;
		}

		#endregion
	}
}
