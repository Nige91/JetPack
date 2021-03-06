﻿using JetPack.Movement;
using JetPack.Weapons;
using JetPack.Drawing;
using SkiaSharp;

namespace JetPack.Enemies
{
	sealed class EnemyFactory
	{
		private static readonly EnemyFactory instance = new EnemyFactory();

		private MovementModuleFactory movementModuleFactory;
		private WeaponModuleFactory weaponModuleFactory;

		private Animator enemy1NormalAnim;
		private Animator enemy1ExplAnim;
		private Animator enemy2NormalAnim;
		private Animator enemy2ExplAnim;

		static EnemyFactory()
		{

		}

		private EnemyFactory()
		{
			movementModuleFactory = MovementModuleFactory.GetInstance();
			weaponModuleFactory = WeaponModuleFactory.GetInstance();

			enemy1NormalAnim = new Animator(
				"JetPack.media.ufos.green1_",
				4,
				Settings.Enemy1.normalAnimStepDuration
			);
			enemy2NormalAnim = new Animator(
				"JetPack.media.ufos.red1_",
				4,
				Settings.Enemy2.normalAnimStepDuration
			);
			enemy1ExplAnim = new Animator(
				"JetPack.media.explosions.explosion1_",
				1,
				Settings.Enemy1.explAnimStepDuration
			);
			enemy2ExplAnim = new Animator(
				"JetPack.media.explosions.explosion1_",
				1,
				Settings.Enemy2.explAnimStepDuration
			);
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
				movementModuleFactory.CreateHorizontalModule(
					coords,
					new SKSize(Settings.Enemy1.sizeX, Settings.Enemy1.sizeY),
					new SKSize(Settings.Enemy1.explSizeX, Settings.Enemy1.explSizeY),
					Settings.Enemy1.speed,
					Settings.Enemy2.xCoordLimit
				),
				weaponModuleFactory.CreateEnemyWeaponFourShot(
					Settings.Enemy1.Weapon.frequency,
					Settings.Enemy1.Weapon.damage,
					Settings.Enemy1.Weapon.projSpeed
				),
				enemy1NormalAnim,
				enemy1ExplAnim
			);
			return enemy;
		}

		public Enemy CreateEnemy2(SKPoint coords)
		{
			Enemy enemy = new Enemy(
				Settings.Enemy2.health,
				movementModuleFactory.CreateCircularModule(
					coords,
					new SKSize(Settings.Enemy2.sizeX, Settings.Enemy2.sizeY),
					new SKSize(Settings.Enemy2.explSizeX, Settings.Enemy2.explSizeY),
					Settings.Enemy2.xSpeed,
					Settings.Enemy2.radius,
					Settings.Enemy2.cycleDuration,
					Settings.Enemy2.xCoordLimit
				),
				weaponModuleFactory.CreateEnemyWeaponFanShot(
					Settings.Enemy2.Weapon.frequency,
					Settings.Enemy2.Weapon.damage,
					Settings.Enemy2.Weapon.projSpeed,
					Settings.Enemy2.Weapon.rotAngleMin,
					Settings.Enemy2.Weapon.rotAngleMax,
					Settings.Enemy2.Weapon.rotCycleDuration
				),
				enemy2NormalAnim,
				enemy2ExplAnim
			);
			return enemy;
		}
	}
}
