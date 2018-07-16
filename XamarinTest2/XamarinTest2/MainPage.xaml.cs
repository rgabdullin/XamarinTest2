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
        CloudStorage cloudStorage;

        public MainPage()
		{
			InitializeComponent();
        }

        private async void DebugButton_Clicked(object sender, EventArgs e)
        {
            DebugLabel.BackgroundColor = Color.Blue;

            cloudStorage = new CloudStorage("studentlist-container",
                "ruslixag1",
                "GIs5ScFrm+cT1AvTnGZPtbWLoekrlMD7+SL1PRwh8U6wEeUJ82aKxfFVJTRTyy6gGtXNxxcn3avVX66YTikwIQ== ");

            DebugLabel.BackgroundColor = Color.Red;

            var text = await cloudStorage.DownloadBlob("1927cfeb-c44d-4c66-a461-a293c0a3ff7a");

            DebugLabel.Text = text;
            DebugLabel.BackgroundColor = Color.Green;/**/
        }
    }
}
