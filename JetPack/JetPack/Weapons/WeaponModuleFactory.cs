using System;
using System.Collections.Generic;
using System.Text;
using JetPack.Movement;
using SkiaSharp;

namespace JetPack.Weapons
{
	static class WeaponModuleFactory
	{
		static public WeaponModule CreateWeapon1(float frequency, float strength, float projectileSpeed)
		{
			WeaponModule module = new WeaponModule();
			module.AddWeaponUnit(CreateWeaponModuleUnit1(frequency, strength, projectileSpeed));
			return module;
		}

		//TODO Remove magic numbers
		static private WeaponModuleUnit CreateWeaponModuleUnit1(float frequency, float strength, float projectileSpeed)
		{
			MovementModule module = MovementModuleFactory.CreateStandardHorizontalModule(new SKPoint(0, 0), new SKSize(3, 0.5f), projectileSpeed);
			WeaponModuleUnit unit = new WeaponModuleUnit(frequency, strength, module, "JetPack.media.projectile1.png");
			return unit;
		}
	}
}
