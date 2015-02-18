// This code was written for the OpenTK library and has been released
// to the Public Domain.
// It is provided "as is" without express or implied warranty of any kind.

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace ohno
{
	/// <summary>
	/// Demonstrates the GameWindow class.
	/// </summary>
	public class SimpleWindow : GameWindow
	{
		public SimpleWindow() : base(800, 600, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4))
		{
			Keyboard.KeyDown += Keyboard_KeyDown;
		}

		#region Keyboard_KeyDown

		/// <summary>
		/// Occurs when a key is pressed.
		/// </summary>
		/// <param name="sender">The KeyboardDevice which generated this event.</param>
		/// <param name="e">The key that was pressed.</param>
		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				this.Exit();

			if (e.Key == Key.F11)
				if (this.WindowState == WindowState.Fullscreen)
					this.WindowState = WindowState.Normal;
			else
				this.WindowState = WindowState.Fullscreen;

			if (e.Key == Key.A)
				r = !r;
		}

		#endregion

		#region OnLoad

		/// <summary>
		/// Setup OpenGL and load resources here.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			GL.Enable (EnableCap.Multisample);
		}

		#endregion

		#region OnResize

		/// <summary>
		/// Respond to resize events here.
		/// </summary>
		/// <param name="e">Contains information on the new GameWindow size.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, 160, 100, 0, 0.0, 4.0);
		}

		#endregion

		#region OnUpdateFrame
		
		Bump b = new Bump();
		Accm a = new Accm ();
		bool r = false;

		/// <summary>
		/// Add your game logic here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			b.AX = a.Data.X / 5000.0;
			b.AY = a.Data.Z / 5000.0;

			if (r) b.Update ();
		}

		#endregion

		#region OnRenderFrame

		/// <summary>
		/// Add your game rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			b.Render ();

			this.SwapBuffers();
		}

		#endregion

		#region public static void Main()

		/// <summary>
		/// Entry point of this example.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			using (SimpleWindow example = new SimpleWindow())
			{

				example.Run(30.0, 30.0);
			}
		}

		#endregion
	}
}
