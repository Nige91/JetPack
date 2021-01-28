using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using JetPack.Movement;
using JetPack.Weapons;

namespace JetPack.Enemies
{
	sealed class EnemyFactory
	{
		private static readonly EnemyFactory instance = new EnemyFactory();

		static EnemyFactory()
		{

		}

		private EnemyFactory()
		{

		}

		public static EnemyFactory GetInstance()
		{
			return instance;
		}

		//TODO Make Create Enemy prettier
		public Enemy CreateEnemy1(SKPoint coords)
		{
			Enemy enemy = new Enemy(
				Settings.Enemy1.health,
				MovementModuleFactory.CreateStandardHorizontalModule(
					coords,
					new SKSize(Settings.Enemy1.sizeX, Settings.Enemy1.sizeY),
					new SKSize(Settings.Enemy1.explSizeX, Settings.Enemy1.explSizeY),
					Settings.Enemy1.speed
				),
				WeaponModuleFactory.CreateEnemyWeapon1(
					Settings.Enemy1.Weapon1.frequency,
					Settings.Enemy1.Weapon1.damage,
					Settings.Enemy1.Weapon1.projSpeed
				),
				"JetPack.media.ufos.green1_",
				4,
				Settings.Enemy1.normalAnimStepDuration,
				"JetPack.media.explosions.explosion1_",
				1,
				Settings.Enemy1.explAnimStepDuration
			);
			return enemy;
		}

		public Enemy CreateEnemy2(SKPoint coords)
		{
			Enemy enemy = new Enemy(
				Settings.Enemy2.health,
				MovementModuleFactory.CreateStandardHorizontalModule(
					coords,
					new SKSize(Settings.Enemy2.sizeX, Settings.Enemy2.sizeY),
					new SKSize(Settings.Enemy2.explSizeX, Settings.Enemy2.explSizeY),
					Settings.Enemy2.speed
				),
				WeaponModuleFactory.CreateEnemyWeapon1(
					Settings.Enemy2.Weapon1.frequency,
					Settings.Enemy2.Weapon1.damage,
					Settings.Enemy2.Weapon1.projSpeed
				),
				"JetPack.media.ufos.red1_",
				4,
				Settings.Enemy2.normalAnimStepDuration,
				"JetPack.media.explosions.explosion1_",
				1,
				Settings.Enemy2.explAnimStepDuration
			);
			return enemy;
		}
	}
}
