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
			SpawnEnemy1(new SKPoint(Settings.General.xAxisLength, Helper.GetRandomFloat(0, Settings.General.yAxisLength - Settings.Enemy1.sizeY)));
		}

		//TODO Make Spawn Enemy1 prettier
		public static void SpawnEnemy1(SKPoint coords)
		{
			Enemy enemy = new Enemy(
				Settings.Enemy1.health, 
				MovementModuleFactory.CreateStandardHorizontalModule(
					coords, 
					new SKSize(Settings.Enemy1.sizeX,Settings.Enemy1.sizeY),
					Settings.Enemy1.speed
				),
				WeaponModuleFactory.CreateEnemyWeapon1(
					Settings.Enemy1.Weapon1.frequency,
					Settings.Enemy1.Weapon1.damage, 
					Settings.Enemy1.Weapon1.projSpeed
				),
				"JetPack.media.ship1.png"
			);
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
