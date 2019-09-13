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
		public bool active { get; set; }
		public bool friendly { get; set; }
		public SKSize scale { get; private set; }
		public List<WeaponModuleUnit> weaponUnits { get; private set; }

		public WeaponModule()
		{
			active = true;
			friendly = false;
			weaponUnits = new List<WeaponModuleUnit>();
		}

		public void SetFriendly()
		{
			this.friendly = true;
			foreach (var unit in weaponUnits)
			{
				unit.friendly = true;
			}
		}

		public void Loop(SKPoint coords)
		{
			foreach (var unit in weaponUnits)
			{
				unit.Loop(coords, active);
			}
		}

		public void AddWeaponUnit(WeaponModuleUnit unit)
		{
			unit.friendly = this.friendly;
			weaponUnits.Add(unit);
		}
	}
}
