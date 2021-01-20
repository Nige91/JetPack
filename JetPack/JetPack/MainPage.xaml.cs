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
using JetPack.Pages;

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
			canvasView.PaintSurface += OnPaint;
			canvasView.Touch += OnTouch;
			Content = canvasView;
			backgroundBitmap = Helper.LoadBitmap("JetPack.media.starfield_purple.png");
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Send(this, "AllowLandscape");
			pageIsActive = true;
			//TODO move initialization
			InitializeGame();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Send(this, "PreventLandscape");
			pageIsActive = false;
		}

		private void OnPaint(object sender, SKPaintSurfaceEventArgs e)
		{
			float pixelCoordRatioX = (float)e.Info.Width/(float)Settings.General.xAxisLength ;
			float pixelCoordRatioY = (float)e.Info.Height / (float)Settings.General.yAxisLength;
			SKCanvas canvas = e.Surface.Canvas;
			canvas.Scale(pixelCoordRatioX, pixelCoordRatioY);
			try
			{
				DrawingLoop(canvas);
			}
			finally
			{
				canvas.Scale(1 / pixelCoordRatioX, 1 / pixelCoordRatioY);
			}
		}

		private void OnTouch(object sender, SKTouchEventArgs e)
		{
			if (e.Location.X < ((SKCanvasView)sender).CanvasSize.Width / 2)
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

		private void InitializeGame()
		{
			player = new Player();
			EnemyManager.Initialize();
			ProjectileManager.Initialize();
			EnemyManager.SpawnEnemy1();
			Loop();
		}

		//TODO Implement precise Timing
		async void Loop()
		{
			while (pageIsActive)
			{
				GameLoop();
				canvasView.InvalidateSurface();
				await Task.Delay(TimeSpan.FromSeconds(1.0 / Settings.General.fps));
			}
		}

		private void DrawingLoop(SKCanvas canvas)
		{
			canvas.Clear(SKColors.DarkBlue);
			canvas.DrawBitmap(backgroundBitmap, new SKRect(0, 0, Settings.General.xAxisLength, Settings.General.yAxisLength));
			player.Draw(canvas);
			EnemyManager.DrawEnemies(canvas);
			ProjectileManager.DrawProjectiles(canvas);
		}
		
		private void GameLoop()
		{
			player.Loop();
			EnemyManager.Loop();
			ProjectileManager.Loop(player, EnemyManager.enemyList);
			if (player.IsGameOver())
			{
				Navigation.PushAsync(new GameOverPage());
			}
		}
	}
}
