#region Copyright & License Information
/*
 * Copyright 2007-2011 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made 
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using OpenRA.Graphics;
using OpenRA.Mods.RA.Buildings;
using OpenRA.Mods.RA.Effects;
using OpenRA.Traits;

namespace OpenRA.Mods.RA.Render
{
	public class SoundOnDamageTransitionInfo : ITraitInfo
	{
		public readonly string DamagedSound;
		public readonly string DestroyedSound;
		
		public object Create(ActorInitializer init) { return new SoundOnDamageTransition(this);}
	}

	public class SoundOnDamageTransition : INotifyDamage
	{
		readonly SoundOnDamageTransitionInfo Info;
		public SoundOnDamageTransition( SoundOnDamageTransitionInfo info )
		{
			Info = info;
		}

		public virtual void Damaged(Actor self, AttackInfo e)
		{
			if (!e.DamageStateChanged)
				return;
			
			if (e.DamageState == DamageState.Dead)
				Sound.Play(Info.DestroyedSound, self.CenterLocation);
			else if (e.DamageState >= DamageState.Heavy && e.PreviousDamageState < DamageState.Heavy)
				Sound.Play(Info.DamagedSound, self.CenterLocation);
		}
	}
}
