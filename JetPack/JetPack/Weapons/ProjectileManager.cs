using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using JetPack.Enemies;

namespace JetPack.Weapons
{
	static class ProjectileManager
	{
		public static List<Projectile> projectileList { get; private set; }

		public static List<Projectile> explodedList { get; private set; }

		static ProjectileManager()
		{
			projectileList = new List<Projectile>();
			explodedList = new List<Projectile>();
		}

		public static void AddProjectile(Projectile projectile)
		{
			projectileList.Add(projectile);
		}

		public static void DrawProjectiles(SKCanvas canvas)
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

		public static void MoveProjectiles()
		{
			foreach (var projectile in projectileList)
			{
				projectile.Move();
			}
		}

		public static void CollideProjectiles(Player player, List<Enemy> enemies)
		{
			List<Projectile> projectilesToExplode = new List<Projectile>();
			foreach (var projectile in projectileList)
			{
				if (projectile.friendly)
				{
					foreach (var enemy in enemies)
					{
						if (projectile.movementModule.GetRect().IntersectsWith(enemy.movementModule.GetRect()))
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

		public static void ExplodeProjectile(Projectile projectile)
		{
			projectile.Explode();
			projectileList.Remove(projectile);
			explodedList.Add(projectile);
		}

		public static void RemoveProjectiles()
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
