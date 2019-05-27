using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using JetPack.Movement;

namespace JetPack.Enemies
{
	static class EnemyManager
	{
		private static List<Enemy> enemyList = new List<Enemy>();

		//TODO Remove Magic Numbers, prevent spawning inside each other.
		public static void SpawnEnemy1()
		{
			SpawnEnemy1(new SKPoint(Globals.xAxisLength, Helper.GetRandomFloat(0, Globals.yAxisLength - 15)));
		}

		//TODO Make Spawn Enemy1 prettier
		public static void SpawnEnemy1(SKPoint coords)
		{
			Enemy enemy = new Enemy(100, MovementModuleFactory.CreateStandardHorizontalModule(coords, new SKSize(15, 15), - 15), "JetPack.media.ship1.png");
			enemyList.Add(enemy);
		}

		public static void DrawEnemies(SKCanvas canvas)
		{
			foreach (var enemy in enemyList)
			{
				enemy.Draw(canvas);
			}
		}
		
		public static void Loop()
		{
			LoopEnemies();
			RemoveDeadEnemies();
		}

		private static void LoopEnemies()
		{
			foreach (var enemy in enemyList)
			{
				enemy.Loop();
			}
		}

		private static void RemoveDeadEnemies()
		{
			foreach (var enemy in enemyList)
			{
				if (enemy.isDead())
				{
					enemyList.Remove(enemy);
				}
			}
		}
	}
}
