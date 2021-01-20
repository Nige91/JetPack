using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JetPack.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameOverPage : ContentPage
	{
		public GameOverPage()
		{
			InitializeComponent();
		}

		private void OnRestartButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new MainPage());
		}

		private void OnExitButtonClicked(object sender, EventArgs e)
		{

		}
	}
}