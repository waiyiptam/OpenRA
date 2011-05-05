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
using OpenRA.FileFormats.Graphics;

namespace OpenRA.Graphics
{
	public class LineRenderer : Renderer.IBatchRenderer
	{
		Renderer renderer;

		Vertex[] vertices = new Vertex[ Renderer.TempBufferSize ];
		int nv = 0;

		public LineRenderer( Renderer renderer )
		{
			this.renderer = renderer;
		}

		public void Flush()
		{
			if( nv > 0 )
			{
				renderer.LineShader.Render( () =>
				{
					var vb = renderer.GetTempVertexBuffer();
					vb.SetData( vertices, nv );
					renderer.DrawBatch( vb,	0, nv, PrimitiveType.LineList );
				} );

				nv = 0;
			}
		}

		public void DrawLine( float2 start, float2 end, Color startColor, Color endColor )
		{
			Renderer.CurrentBatchRenderer = this;

			if( nv + 2 > Renderer.TempBufferSize )
				Flush();

			vertices[ nv++ ] = new Vertex( start,
				new float2( startColor.R / 255.0f, startColor.G / 255.0f ),
				new float2( startColor.B / 255.0f, startColor.A / 255.0f ) );

			vertices[ nv++ ] = new Vertex( end,
				new float2( endColor.R / 255.0f, endColor.G / 255.0f ),
				new float2( endColor.B / 255.0f, endColor.A / 255.0f ) );
		}
		
		public void FillRect( RectangleF r, Color color )
		{
			for (float y = r.Top; y < r.Bottom; y++)
				DrawLine(new float2(r.Left, y), new float2(r.Right, y), color, color);
		}
	}
}
