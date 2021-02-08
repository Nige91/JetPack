using SkiaSharp;
using System.Collections.Generic;

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
			friendly = true;
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
			unit.friendly = friendly;
			weaponUnits.Add(unit);
		}
	}
}
