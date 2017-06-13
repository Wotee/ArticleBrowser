using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ArticleBrowserAddIn
{
	/// <summary>
	/// ControlHost to wrap the WPF controls, since word does not
	/// support WPF
	/// </summary>
	public partial class ControlHost : UserControl
	{
		public ControlHost()
		{
			InitializeComponent();
		}

		public void Form1_Load(object sender, EventArgs e)
		{
			ElementHost host = new ElementHost { Dock = DockStyle.Fill };
			Controls.Add(host);
		}
	}
}
