using System;
using System.Collections.Generic;
using System.Text;

namespace JetPack
{
	static class Settings
	{
		public static class Player
		{
			public const double minSpeed = -5;
			public const double maxSpeed = 5;
			public const double gravity = 0.6;
			public const double jetPackStrength = 1.4;
			public const double maxHealth = 100;
		}

		public static class General
		{
			public const int xAxisLength = 160;
			public const int yAxisLength = 90;
			public const int fps = 30;
			public const int normalTimeUnitInMs = 1000;
		}

		public static class Interface
		{
			public const float healthBarPosX = 0;
			public const float healthBarPosY = -5;
			public const float healthBarWidth = 10;
			public const float healthBarHeight = 1;
		}

		public static class Enemy1
		{
			public const float health = 100;
			public const float sizeX = 15;
			public const float sizeY = 15;
			public const float speed = -3;

			public static class Weapon1
			{
				public const float frequency = 2;
				public const float damage = 10;
				public const float projSpeed = -60;
			}
		}
	}
}
