using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace ohno
{
	public class Bump
	{
		public double segments = 15.0;

		public double X = 80.0, Y = 50.0;
		public double VX, VY;
		public double AX, AY;

		public void Update() {
			X += (VX += AX);
			Y += (VY += AY);

			if (X > 160) { 
				VX = -Math.Abs (VX);
				X = 160;
			}
			if (Y > 100) { 
				VY = -Math.Abs (VY);
				Y = 100;
			}
			if (X < 0) { 
				VX = Math.Abs (VX);
				X = 0;
			}
			if (Y < 0) {
				VY = Math.Abs (VY);
				Y = 0;
			}
		}

		public void Render() {
			GL.Begin(BeginMode.Polygon);

			GL.Color3(Color.White);

			double a;
			double x, y;

			for (double i = 0; i < segments; i++) {
				a = Math.PI * (i / segments) * 2;

				x = this.X + Math.Cos (a) * 5.0;
				y = this.Y + Math.Sin (a) * 5.0;

				GL.Vertex2 (x, y);
			}

			GL.End();
		}
	}
}

