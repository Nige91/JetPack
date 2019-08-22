using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using JetPack.Movement;

namespace JetPack.Weapons
{
	//TODO overthink WeaponModule class structure
	class WeaponModule
	{
		public float speedModifier { get; private set; }
		public float strengthModifier { get; private set; }
		public bool active { get; private set; }
		public SKSize scale { get; private set; }
		public List<WeaponModuleUnit> weaponUnits { get; private set; }

		public WeaponModule()
		{
			weaponUnits = new List<WeaponModuleUnit>();
		}

		public void Loop(SKPoint coords)
		{
			foreach (var unit in weaponUnits)
			{
				unit.Loop(coords);
			}
		}

		public void DrawProjectiles(SKCanvas canvas)
		{
			foreach (var unit in weaponUnits)
			{
				unit.DrawProjectiles(canvas);
			}
		}

		public void AddWeaponUnit(WeaponModuleUnit unit)
		{
			weaponUnits.Add(unit);
		}
	}
}
