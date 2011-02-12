﻿#region Copyright & License Information
/*
 * Copyright 2007-2011 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made 
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System.Drawing;
using OpenRA.Traits;
using System.Collections.Generic;
using OpenRA.FileFormats;

namespace OpenRA.Mods.RA
{
	class WaterPaletteRotationInfo : TraitInfo<WaterPaletteRotation> {}
	class WaterPaletteRotation : ITick, IPaletteModifier
	{
		float t = 0;
		public void Tick(Actor self)
		{
			t += .25f;
		}

		public void AdjustPalette(Dictionary<string,Palette> palettes)
		{
			var excludePalettes = new List<string>(){"cursor", "chrome", "colorpicker"};
			foreach (var pal in palettes)
			{
				if (excludePalettes.Contains(pal.Key))
					continue;
				
				var copy = (uint[])pal.Value.Values.Clone();
				var rotate = (int)t % 7;
				for (int i = 0; i < 7; i++)
					pal.Value.SetColor(0x60 + (rotate + i) % 7, copy[0x60 + i]);
			}
		}
	}
}
