using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using JetPack.Movement;
using JetPack.Weapons;

namespace JetPack.Enemies
{
	//TODO overthink Enemy class structure
	static class EnemyManager
	{
		public static List<Enemy> enemyList { get; private set; }

		static EnemyManager()
		{
			enemyList = new List<Enemy>();
		}

		//TODO Remove Magic Numbers, prevent spawning inside each other.
		public static void SpawnEnemy1()
		{
			SpawnEnemy1(new SKPoint(Globals.xAxisLength, Helper.GetRandomFloat(0, Globals.yAxisLength - 15)));
		}

		//TODO Make Spawn Enemy1 prettier
		public static void SpawnEnemy1(SKPoint coords)
		{
			//TODO Remove Magic Numbers
			Enemy enemy = new Enemy(100, 
				MovementModuleFactory.CreateStandardHorizontalModule(coords, new SKSize(15, 15), - 3),
				WeaponModuleFactory.CreateEnemyWeapon1(2f, 10, - 60),
				"JetPack.media.ship1.png");
			enemyList.Add(enemy);
		}

		public static void DrawEnemies(SKCanvas canvas)
		{
			foreach (var enemy in enemyList)
			{
				enemy.DrawEnemy(canvas);
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
			List<Enemy> enemiesToRemove = new List<Enemy>();
			foreach (var enemy in enemyList)
			{
				if (enemy.isDead())
				{
					enemiesToRemove.Add(enemy);
				}
			}
			foreach (var enemy in enemiesToRemove)
			{
				enemyList.Remove(enemy);
			}
		}
	}
}
