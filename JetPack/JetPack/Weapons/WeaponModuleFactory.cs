using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	static class WeaponModuleFactory
	{
		static public WeaponModule CreateEnemyWeapon1(float frequency, float damage, float projectileSpeed)
		{
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(CreateWeaponModuleUnit1(
				frequency, 
				damage, 
				projectileSpeed, 
				new SKPoint(0, Settings.Enemy1.Weapon1.yCoords1),
				Settings.Enemy1.Weapon1.phaseShift1
			));
			module.AddWeaponUnit(CreateWeaponModuleUnit1(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon1.yCoords2),
				Settings.Enemy1.Weapon1.phaseShift2
			));
			module.AddWeaponUnit(CreateWeaponModuleUnit1(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon1.yCoords3),
				Settings.Enemy1.Weapon1.phaseShift3
			));
			module.AddWeaponUnit(CreateWeaponModuleUnit1(
				frequency,
				damage,
				projectileSpeed,
				new SKPoint(0, Settings.Enemy1.Weapon1.yCoords4),
				Settings.Enemy1.Weapon1.phaseShift4
			));
			return module;
		}

		static public WeaponModule CreatePlayerWeapon1(float frequency, float damage, float projectileSpeed)
		{
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, damage, projectileSpeed, new SKPoint(0, Settings.Player.Weapon.yCoord)));
			return module;
		}

		//TODO Remove magic numbers
		static private WeaponModuleUnit CreateWeaponModuleUnit1(float frequency, float damage, float projectileSpeed, SKPoint coords, float cooldownPhaseShiftPercent = 0)
		{
			MovementModule module = MovementModuleFactory.CreateStandardHorizontalModule(
				coords, 
				new SKSize(Settings.Weapon1.projSizeX, Settings.Weapon1.projSizeY), 
				projectileSpeed
			);
			WeaponModuleUnit unit = new WeaponModuleUnit(frequency, damage, module, "JetPack.media.projectile1.png");
			unit.SetCooldownPhaseShiftPercent(cooldownPhaseShiftPercent);
			return unit;
		}
	}
}
