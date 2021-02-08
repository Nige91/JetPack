using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	sealed class WeaponModuleFactory
	{
		private static readonly WeaponModuleFactory instance =
			new WeaponModuleFactory();

		private MovementModuleFactory movementModuleFactory;

		static WeaponModuleFactory()
		{

		}

		private WeaponModuleFactory()
		{
			movementModuleFactory = MovementModuleFactory.GetInstance();
		}

		public static WeaponModuleFactory GetInstance()
		{
			return instance;
		}

		public WeaponModule CreateEnemyWeaponFanShot(
			float frequency,
			float damage,
			float projectileSpeed,
			float rotAngleMin,
			float rotAngleMax,
			float rotCycleDuration
		)
		{
			WeaponModule module = new WeaponModule();
			WeaponModuleUnit unit = CreateWeaponModuleUnitHorizontal(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy2.Weapon.yCoords),
				new SKSize(
						Settings.WeaponFanShot.projSizeX,
						Settings.WeaponFanShot.projSizeY
					),
					new SKSize(
						Settings.WeaponFanShot.explSizeX,
						Settings.WeaponFanShot.explSizeY
					)
			);
			unit.SetRotating(rotAngleMin, rotAngleMax, rotCycleDuration);
			module.AddWeaponUnit(unit);
			return module;
		}

		public WeaponModule CreateEnemyWeaponFourShot(
			float frequency,
			float damage,
			float projectileSpeed
		)
		{
			SKSize projSize = new SKSize(
				Settings.WeaponFourShot.projSizeX,
				Settings.WeaponFourShot.projSizeY
			);
			SKSize explSize = new SKSize(
				Settings.WeaponFourShot.explSizeX,
				Settings.WeaponFourShot.explSizeY
			);
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(CreateWeaponModuleUnitHorizontal(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon.yCoords1),
				projSize,
				explSize,
				Settings.Enemy1.Weapon.phaseShift1
			));
			module.AddWeaponUnit(CreateWeaponModuleUnitHorizontal(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon.yCoords2),
				projSize,
				explSize,
				Settings.Enemy1.Weapon.phaseShift2
			));
			module.AddWeaponUnit(CreateWeaponModuleUnitHorizontal(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon.yCoords3),
				projSize,
				explSize,
				Settings.Enemy1.Weapon.phaseShift3
			));
			module.AddWeaponUnit(CreateWeaponModuleUnitHorizontal(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon.yCoords4),
				projSize,
				explSize,
				Settings.Enemy1.Weapon.phaseShift4
			));
			return module;
		}

		public WeaponModule CreatePlayerWeapon(
			float frequency,
			float damage,
			float projectileSpeed
		)
		{
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(
				CreateWeaponModuleUnitHorizontal(
					frequency,
					damage,
					projectileSpeed,
					new SKPoint(0, Settings.Player.Weapon.yCoord),
					new SKSize(
						Settings.WeaponPlayer.projSizeX,
						Settings.WeaponPlayer.projSizeY
					),
					new SKSize(
						Settings.WeaponPlayer.explSizeX,
						Settings.WeaponPlayer.explSizeY
					)
				)
			);
			return module;
		}

		//TODO Remove magic numbers
		private WeaponModuleUnit CreateWeaponModuleUnitHorizontal(
			float frequency,
			float damage,
			float projectileSpeed,
			SKPoint coords,
			SKSize projSize,
			SKSize explSize,
			float cooldownPhaseShiftPercent = 0
		)
		{
			MovementModule module =
				movementModuleFactory.CreateStandardHorizontalModule(
					coords,
					projSize,
					explSize,
					projectileSpeed
				);
			WeaponModuleUnit unit = new WeaponModuleUnit(
				frequency,
				damage,
				module,
				"JetPack.media.projectile1.png",
				"JetPack.media.explosions.explosion1_",
				1,
				Settings.WeaponFourShot.explAnimStepDuration
			);
			unit.SetCooldownPhaseShiftPercent(cooldownPhaseShiftPercent);
			return unit;
		}
	}
}
