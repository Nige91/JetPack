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
using JetPack.Movement;

namespace JetPack
{
	public partial class MainPage : ContentPage
	{
		bool pageIsActive;

		Player player;
		EnemyManager enemyManager;
		ProjectileManager projectileManager;
		LoopTimer loopTimer;
		TimeLogger timeLogger;

		SKBitmap backgroundBitmap;

		public MainPage()
		{
			InitializeComponent();
			skglView.PaintSurface += OnPaint;
			skglView.Touch += OnTouch;
			Content = skglView;
			backgroundBitmap = Helper.LoadBitmap("JetPack.media.starfield_purple.png");
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Send(this, "AllowLandscape");
			pageIsActive = true;
			InitializeGame();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Send(this, "PreventLandscape");
			pageIsActive = false;
		}

		private void OnPaint(object sender, SKPaintGLSurfaceEventArgs e)
		{
			float pixelCoordRatioX = 
				(float)e.BackendRenderTarget.Width/(float)Settings.General.xAxisLength;
			float pixelCoordRatioY = 
				(float)e.BackendRenderTarget.Height / (float)Settings.General.yAxisLength;
			SKCanvas canvas = e.Surface.Canvas;
			canvas.Scale(pixelCoordRatioX, pixelCoordRatioY);
			try
			{
				timeLogger.StartLog("GameLoop");
				GameLoop();
				timeLogger.FinishLog("GameLoop");
				timeLogger.StartLog("DrawingLoop");
				DrawingLoop(canvas);
				timeLogger.FinishLog("DrawingLoop");
				timeLogger.CalculateMeans();
			}
			finally
			{
				canvas.Scale(1 / pixelCoordRatioX, 1 / pixelCoordRatioY);
			}
		}

		private void OnTouch(object sender, SKTouchEventArgs e)
		{
			if (e.Location.X < ((SKGLView)sender).CanvasSize.Width / 2)
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
			enemyManager = EnemyManager.GetInstance();
			enemyManager.Initialize();
			projectileManager = ProjectileManager.GetInstance();
			projectileManager.Initialize();
			loopTimer = LoopTimer.GetInstance();
			timeLogger = TimeLogger.GetInstance();
			Loop();
		}

		//TODO Implement precise Timing
		async void Loop()
		{
			while (pageIsActive)
			{
				skglView.InvalidateSurface();
				await Task.Delay(TimeSpan.FromSeconds(1.0 / Settings.General.fps));
			}
		}

		private void DrawingLoop(SKCanvas canvas)
		{
			canvas.Clear(SKColors.DarkBlue);
			canvas.DrawBitmap(
				backgroundBitmap, 
				new SKRect(
					0, 
					0, 
					Settings.General.xAxisLength, 
					Settings.General.yAxisLength
				)
			);
			player.Draw(canvas);
			enemyManager.DrawEnemies(canvas);
			projectileManager.DrawProjectiles(canvas);
			GraphicalUserInterface.DrawScore(canvas, enemyManager.score);
			GraphicalUserInterface.DrawFPS(canvas, (int)loopTimer.GetFPS());
		}
		
		private void GameLoop()
		{
			timeLogger.StartLog("loopTimer");
			loopTimer.MeasureTime();
			timeLogger.FinishLog("loopTimer");
			timeLogger.StartLog("player");
			player.Loop();
			timeLogger.FinishLog("player");
			timeLogger.StartLog("enemyManager");
			enemyManager.Loop();
			timeLogger.FinishLog("enemyManager");
			timeLogger.StartLog("projectileManager");
			projectileManager.Loop(player, enemyManager.enemyList);
			timeLogger.FinishLog("projectileManager");
			if (player.IsGameOver())
			{
				Navigation.PushAsync(new GameOverPage());
			}
		}
	}
}
