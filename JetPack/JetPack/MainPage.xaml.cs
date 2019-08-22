using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using JetPack.Enemies;

namespace JetPack
{
	public partial class MainPage : ContentPage
	{
		bool pageIsActive;

		Player player;

		SKBitmap backgroundBitmap;

		public MainPage()
		{
			InitializeComponent();
			//TODO Remove magic numbers
			this.player = new Player(10, 10, 10, 10);
			EnemyManager.SpawnEnemy1();
			LoadBackground("JetPack.media.starfield_purple.png");
			
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
				await Task.Delay(TimeSpan.FromSeconds(1.0 / Globals.fps));
			}
		}

		//TODO Move Stuff out of OnPaintSample
		private void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
		{
			GameLoop();
			float pixelCoordRatioX = (float)e.Info.Width/(float)Globals.xAxisLength ;
			float pixelCoordRatioY = (float)e.Info.Height / (float)Globals.yAxisLength;
			SKCanvas canvas = e.Surface.Canvas;
			try
			{
				canvas.Clear(SKColors.DarkBlue);
				canvas.Scale(pixelCoordRatioX, pixelCoordRatioY);
				canvas.DrawBitmap(backgroundBitmap, new SKRect(0, 0, Globals.xAxisLength, Globals.yAxisLength));
				player.Draw(canvas);
				EnemyManager.DrawEnemies(canvas);
				EnemyManager.DrawProjectiles(canvas);
			}
			finally
			{
				canvas.Scale(1 / pixelCoordRatioX, 1 / pixelCoordRatioY);
			}
		}
		
		private void GameLoop()
		{
			player.Loop();
			EnemyManager.Loop();
		}

		private void OnTouch(object sender, SKTouchEventArgs e)
		{
			if(e.Location.X < ((SKCanvasView)sender).CanvasSize.Width / 2)
			{
				player.TouchLeft();
			}
			else
			{
				player.TouchRight();
			}
		}

		private void LoadBackground(string resourceId)
		{
			Assembly assembly = GetType().GetTypeInfo().Assembly;
			using (Stream stream = assembly.GetManifestResourceStream(resourceId))
			{
				this.backgroundBitmap = SKBitmap.Decode(stream);
			}
		}
	}

	public static class Globals
	{
		public const int xAxisLength = 160;
		public const int yAxisLength = 90;
		public const int fps = 30;
		public const int normalTimeUnitInMs = 1000;
	}
}
