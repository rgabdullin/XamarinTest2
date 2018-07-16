using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinTest2
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
		{
			InitializeComponent();
        }

        private async void DebugButton_Clicked(object sender, EventArgs e)
        {
            DebugLabel.BackgroundColor = Color.Blue;

            var text = await CloudStorage.DownloadBlob("testblob");

            DebugLabel.Text = text;
            DebugLabel.BackgroundColor = Color.Green;
        }
    }
}
