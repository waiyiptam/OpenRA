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
using System.Drawing;
using OpenRA.Graphics;

namespace OpenRA.Widgets
{
	public class PasswordFieldWidget : TextFieldWidget
	{
		public PasswordFieldWidget() : base() {}
		protected PasswordFieldWidget(PasswordFieldWidget widget) : base(widget) {}
		
		public override void DrawInner()
		{
			DrawWithString(new string('*', Text.Length));
		}
		public override Widget Clone() { return new PasswordFieldWidget(this); }
	}
}