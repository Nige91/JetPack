using JetPack.Enemies;
using SkiaSharp;
using System.Collections.Generic;

namespace JetPack.Weapons
{
	sealed class ProjectileManager
	{
		private static readonly ProjectileManager instance = new ProjectileManager();

		public List<Projectile> projectileList { get; private set; }
		public List<Projectile> explodedList { get; private set; }

		static ProjectileManager()
		{

		}

		private ProjectileManager()
		{

		}

		public static ProjectileManager GetInstance()
		{
			return instance;
		}

		public void Initialize()
		{
			projectileList = new List<Projectile>();
			explodedList = new List<Projectile>();
		}

		public void AddProjectile(Projectile projectile)
		{
			projectileList.Add(projectile);
		}

		public void DrawProjectiles(SKCanvas canvas)
		{
			foreach (var projectile in projectileList)
			{
				projectile.Draw(canvas);
			}
			foreach (var projectile in explodedList)
			{
				projectile.Draw(canvas);
			}
		}

		public void Loop(Player player, List<Enemy> enemies)
		{
			MoveProjectiles();
			CollideProjectiles(player, enemies);
			RemoveExplodedProjectiles();
			RemoveOutOfBoundProjectiles();
		}

		private void MoveProjectiles()
		{
			foreach (var projectile in projectileList)
			{
				projectile.Move();
			}
		}

		private void CollideProjectiles(Player player, List<Enemy> enemies)
		{
			List<Projectile> projectilesToExplode = new List<Projectile>();
			foreach (var projectile in projectileList)
			{
				if (projectile.friendly)
				{
					foreach (var enemy in enemies)
					{
						if (projectile.movementModule.GetRect().IntersectsWith(
							enemy.movementModule.GetRect()
						))
						{
							enemy.SufferDamage(projectile.damage);
							projectilesToExplode.Add(projectile);
						}
					}
				}
				else
				{
					if (projectile.movementModule.GetRect().IntersectsWith(player.GetRect()))
					{
						player.SufferDamage(projectile.damage);
						projectilesToExplode.Add(projectile);
					}
				}
			}
			foreach (var projectile in projectilesToExplode)
			{
				ExplodeProjectile(projectile);
			}
		}

		private void ExplodeProjectile(Projectile projectile)
		{
			projectile.Explode();
			projectileList.Remove(projectile);
			explodedList.Add(projectile);
		}

		private void RemoveOutOfBoundProjectiles()
		{
			List<Projectile> projectilesToRemove = new List<Projectile>();
			foreach (var projectile in projectileList)
			{
				if (projectile.IsOutOfBounds())
				{
					projectilesToRemove.Add(projectile);
				}
			}
			foreach (var projectile in projectilesToRemove)
			{
				projectileList.Remove(projectile);
			}
		}

		private void RemoveExplodedProjectiles()
		{
			List<Projectile> projectilesToRemove = new List<Projectile>();
			foreach (var projectile in explodedList)
			{
				if (projectile.ExplosionFinished())
				{
					projectilesToRemove.Add(projectile);
				}
			}
			foreach (var projectile in projectilesToRemove)
			{
				explodedList.Remove(projectile);
			}
		}
	}
}
