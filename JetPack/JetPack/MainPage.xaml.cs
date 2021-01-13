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
using JetPack.Weapons;

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
				await Task.Delay(TimeSpan.FromSeconds(1.0 / Settings.General.fps));
			}
		}

		//TODO Move Stuff out of OnPaintSample
		private void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
		{
			GameLoop();
			float pixelCoordRatioX = (float)e.Info.Width/(float)Settings.General.xAxisLength ;
			float pixelCoordRatioY = (float)e.Info.Height / (float)Settings.General.yAxisLength;
			SKCanvas canvas = e.Surface.Canvas;
			try
			{
				canvas.Clear(SKColors.DarkBlue);
				canvas.Scale(pixelCoordRatioX, pixelCoordRatioY);
				canvas.DrawBitmap(backgroundBitmap, new SKRect(0, 0, Settings.General.xAxisLength, Settings.General.yAxisLength));
				player.Draw(canvas);
				EnemyManager.DrawEnemies(canvas);
				ProjectileManager.DrawProjectiles(canvas);
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
			ProjectileManager.MoveProjectiles();
			ProjectileManager.CollideProjectiles(player, EnemyManager.enemyList);
		}

		private void OnTouch(object sender, SKTouchEventArgs e)
		{
			if(e.Location.X < ((SKCanvasView)sender).CanvasSize.Width / 2)
			{
				if (e.ActionType == SKTouchAction.Pressed)
				{
					player.TouchLeft();
				}
				else if (e.ActionType == SKTouchAction.Released)
				{
					player.ReleaseLeft();
				}
			}
			else 
			{
				if (e.ActionType == SKTouchAction.Pressed)
				{
					player.TouchRight();
				}
				else if (e.ActionType == SKTouchAction.Released)
				{
					player.ReleaseRight();
				}
			}
			e.Handled = true;
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
}
