using SkiaSharp;
using System.Collections.Generic;

namespace JetPack.Enemies
{
	//TODO overthink Enemy class structure
	sealed class EnemyManager
	{
		private static readonly EnemyManager instance = new EnemyManager();

		public List<Enemy> enemyList { get; private set; }
		public int score { get; private set; }
		private List<Enemy> explodedList;
		private EnemyFactory enemyFactory;


		static EnemyManager()
		{

		}

		private EnemyManager()
		{

		}

		public void Initialize()
		{
			enemyList = new List<Enemy>();
			explodedList = new List<Enemy>();
			enemyFactory = EnemyFactory.GetInstance();
			score = 0;
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
			SpawnLoop();
		}

		private void SpawnLoop()
		{
			if (enemyList.Count < 1)
			{
				//SpawnEnemy2();
			}
		}

		//TODO prevent spawning inside each other.
		public void SpawnEnemy1()
		{
			var enemy = enemyFactory.CreateEnemy1(
				new SKPoint(
					Settings.General.xAxisLength - Settings.Enemy1.sizeX,
					Helper.GetRandomFloat(
						0,
						Settings.General.yAxisLength - Settings.Enemy1.sizeY
					)
				)
			);
			enemyList.Add(enemy);
		}

		public void SpawnEnemy2()
		{
			var enemy = enemyFactory.CreateEnemy2(
				new SKPoint(
					Settings.General.xAxisLength - Settings.Enemy1.sizeX,
					Helper.GetRandomFloat(
						0,
						Settings.General.yAxisLength - Settings.Enemy1.sizeY
					)
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
					IncreaseScore(1);
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

		private void IncreaseScore(int score)
		{
			this.score += score;
		}
	}
}
