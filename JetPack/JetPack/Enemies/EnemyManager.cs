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
		public static List<Enemy> explodedList { get; private set; }

		static EnemyManager()
		{
			enemyList = new List<Enemy>();
			explodedList = new List<Enemy>();
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
					new SKSize(Settings.Enemy1.explSizeX, Settings.Enemy1.explSizeY),
					Settings.Enemy1.speed
				),
				WeaponModuleFactory.CreateEnemyWeapon1(
					Settings.Enemy1.Weapon1.frequency,
					Settings.Enemy1.Weapon1.damage, 
					Settings.Enemy1.Weapon1.projSpeed
				),
				"JetPack.media.ship1.png",
				"JetPack.media.explosion1.png",
				Settings.Enemy1.explDuration
			);
			enemyList.Add(enemy);
		}

		public static void DrawEnemies(SKCanvas canvas)
		{
			foreach (var enemy in enemyList)
			{
				enemy.Draw(canvas);
			}
			foreach (var enemy in explodedList)
			{
				enemy.Draw(canvas);
			}
		}
		
		public static void Loop()
		{
			LoopEnemies();
			ExplodeDeadEnemies();
			RemoveExplodedEnemies();
		}

		private static void LoopEnemies()
		{
			foreach (var enemy in enemyList)
			{
				enemy.Loop();
			}
		}

		private static void ExplodeDeadEnemies()
		{
			List<Enemy> enemiesToRemove = new List<Enemy>();
			foreach (var enemy in enemyList)
			{
				if (enemy.IsDead())
				{
					enemiesToRemove.Add(enemy);
					enemy.Explode();
				}
			}
			foreach (var enemy in enemiesToRemove)
			{
				enemyList.Remove(enemy);
				explodedList.Add(enemy);
			}
		}

		private static void RemoveExplodedEnemies()
		{
			List<Enemy> enemiesToRemove = new List<Enemy>();
			foreach (var enemy in explodedList)
			{
				if (enemy.ExplosionFinished())
				{
					enemiesToRemove.Add(enemy);
				}
			}
			foreach (var enemy in enemiesToRemove)
			{
				explodedList.Remove(enemy);
			}
		}
	}
}
