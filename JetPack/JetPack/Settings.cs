﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JetPack
{
	static class Settings
	{
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

		public static class Player
		{
			public const float startPosX = 10;
			public const float startPosY = 10;
			public const float sizeX = 10;
			public const float sizeY = 10;
			public const double minSpeed = -5;
			public const double maxSpeed = 5;
			public const double gravity = 0.6;
			public const double jetPackStrength = 1.4;
			public const double maxHealth = 100;
			public const float explSizeX = 10;
			public const float explSizeY = 10;
			public const string explType = "03";
			public const int explAnimStepDuration = 10;

			public static class Weapon
			{
				public const float frequency = 2;
				public const float damage = 100;
				public const float projSpeed = 150;

				public const float yCoord = 5;
			}
		}

		public static class Enemy1
		{
			public const float health = 100;
			public const float sizeX = 15;
			public const float sizeY = 15;
			public const int normalAnimStepDuration = 100;
			public const float explSizeX = 20;
			public const float explSizeY = 20;
			public const string explType = "03";
			public const int explAnimStepDuration = 10;
			public const float speed = -8;

			public static class Weapon1
			{
				public const float frequency = 0.2f;
				public const float damage = 100;
				public const float projSpeed = -60;

				public const float yCoords1 = 0;
				public const float yCoords2 = 2;
				public const float yCoords3 = 13;
				public const float yCoords4 = 15;

				//percent
				public const float phaseShift1 = 0;
				public const float phaseShift2 = 10;
				public const float phaseShift3 = 20;
				public const float phaseShift4 = 30;
			}
		}

		public static class Weapon1
		{
			public const float projSizeX = 3;
			public const float projSizeY = 0.5f;
			public const int explDuration = 100;
			public const float explSizeX = 2;
			public const float explSizeY = 2;
		}
	}
}