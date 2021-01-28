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
	sealed class EnemyManager
	{
		private static readonly EnemyManager instance = new EnemyManager();

		public List<Enemy> enemyList { get; private set; }
		private List<Enemy> explodedList;
		private EnemyFactory enemyFactory;

		static EnemyManager()
		{
			
		}

		private EnemyManager()
		{
			Initialize();
		}

		private void Initialize()
		{
			enemyList = new List<Enemy>();
			explodedList = new List<Enemy>();
			enemyFactory = EnemyFactory.GetInstance();
		}

		public static EnemyManager GetInstance()
		{
			return instance;
		}

		public void DrawEnemies(SKCanvas canvas)
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

		public void Loop()
		{
			LoopEnemies();
			ExplodeDeadEnemies();
			RemoveExplodedEnemies();
		}

		//TODO prevent spawning inside each other.
		public void SpawnEnemy1()
		{
			var enemy = enemyFactory.CreateEnemy1(new SKPoint(
				Settings.General.xAxisLength,
				Helper.GetRandomFloat(0, Settings.General.yAxisLength - Settings.Enemy1.sizeY)
			));
			enemyList.Add(enemy);
		}

		public void SpawnEnemy2()
		{
			var enemy = enemyFactory.CreateEnemy2(
				new SKPoint(
					Settings.General.xAxisLength,
					Helper.GetRandomFloat(0, Settings.General.yAxisLength - Settings.Enemy1.sizeY)
				)
			);
			enemyList.Add(enemy);
		}

		private void LoopEnemies()
		{
			foreach (var enemy in enemyList)
			{
				enemy.Loop();
			}
		}

		private void ExplodeDeadEnemies()
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

		private void RemoveExplodedEnemies()
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
