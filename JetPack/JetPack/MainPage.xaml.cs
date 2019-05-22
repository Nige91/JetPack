using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JetPack
{
	public partial class MainPage : ContentPage
	{
		int xMax = 160;
		int yMax = 90;

		bool pageIsActive;
		int fps = 30;

		Player player;

		public MainPage()
		{
			InitializeComponent();
			this.player = new Player(10, 10, 10, 10);

			canvasView.PaintSurface += OnPaintSample;
			canvasView.Touch += OnTouch;
			Content = canvasView;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Send(this, "AllowLandscape");
			pageIsActive = true;
			AnimationLoop();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Send(this, "PreventLandscape");
			pageIsActive = false;
		}

		async Task AnimationLoop()
		{
			while (pageIsActive)
			{
				canvasView.InvalidateSurface();
				await Task.Delay(TimeSpan.FromSeconds(1.0 / fps));
			}
		}

		private void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
		{
			float pixelCoordRatioX = (float)e.Info.Width/(float)Globals.xMax ;
			float pixelCoordRatioY = (float)e.Info.Height / (float)Globals.yMax;
			SKCanvas canvas = e.Surface.Canvas;
			try
			{
				canvas.Clear(SKColors.DarkBlue);
				canvas.Scale(pixelCoordRatioX, pixelCoordRatioY);
				player.Draw(canvas);
			}
			catch(Exception error)
			{
				int fdjkvn = 90;
			}
			finally
			{
				canvas.Scale(1 / pixelCoordRatioX, 1 / pixelCoordRatioY);
			}
		}

		private void OnTouch(object sender, SKTouchEventArgs e)
		{
			player.Touch(sender, e);
		}
	}

	public static class Globals
	{
		public const int xMax = 160;
		public const int yMax = 90;
	}
}
