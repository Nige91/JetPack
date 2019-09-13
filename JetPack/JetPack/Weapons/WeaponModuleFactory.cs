using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	static class WeaponModuleFactory
	{
		static public WeaponModule CreateEnemyWeapon1(float frequency, float strength, float projectileSpeed)
		{
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, strength, projectileSpeed, new SKPoint(0, 0)));
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, strength, projectileSpeed, new SKPoint(0, 2), 10));
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, strength, projectileSpeed, new SKPoint(0, 13), 20));
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, strength, projectileSpeed, new SKPoint(0, 15), 30));
			return module;
		}

		static public WeaponModule CreatePlayerWeapon1(float frequency, float strength, float projectileSpeed)
		{
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, strength, projectileSpeed, new SKPoint(0, 5)));
			return module;
		}

		//TODO Remove magic numbers
		static private WeaponModuleUnit CreateWeaponModuleUnit1(float frequency, float strength, float projectileSpeed, SKPoint coords, float cooldownPhaseShiftPercent = 0)
		{
			MovementModule module = MovementModuleFactory.CreateStandardHorizontalModule(coords, new SKSize(3, 0.5f), projectileSpeed);
			WeaponModuleUnit unit = new WeaponModuleUnit(frequency, strength, module, "JetPack.media.projectile1.png");
			unit.SetCooldownPhaseShiftPercent(cooldownPhaseShiftPercent);
			return unit;
		}
	}
}
