using System;
using Gtk;
using Gdk;

public partial class MainWindow: Gtk.Window
{	

	public byte[] pixels;
	public int width;
	public int height;
	public bool alpha = false;


	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnDrawingarea1ExposeEvent (object o, ExposeEventArgs args)
	{

		int idx =0;
		for (int h=0;h<height;h++)
			for (int w=0;w<width;w++)

		{
		
			byte a=255;
			if (alpha)
				a = pixels[idx++];

			byte c = pixels[idx++];
			//byte c = pixels[h*width+w];

			//Console.WriteLine("color  is {0:x}",c);
			int r = ((c >> 6) & 0x3)<<6;
			/*float rf = (float)r;
			rf = 256*(rf/7);
			r = (int)rf;*/

			int g = ((c >> 3) & 07)<<5;
			/*float rg = (float)g;
			rg = 256*(rg/7);
			g = (int)rg;*/

			int b = (c  & 0x7)<<5;
			/*float rb = (float)b;
			rb = 256*(rb/3);
			b = (int)rb;*/

			byte br = (byte)(Math.Min(255,Math.Max(0,r)));
			byte bg = (byte)(Math.Min(255,Math.Max(0,g)));
			byte bb = (byte)(Math.Min(255,Math.Max(0,b)));

			/*Console.WriteLine("r  is {0}",br);
			Console.WriteLine("g  is {0}",bg);
			Console.WriteLine("b  is {0}",bb);*/

			Gdk.GC gc=new Gdk.GC((Drawable)base.GdkWindow);
			gc.RgbFgColor=new Gdk.Color(br,bg,bb);
			gc.RgbBgColor=new Gdk.Color(br,bg,bb);
			if (a > 128)
				drawingarea1.GdkWindow.DrawPoint(gc,w,h);
		}

		//drawingarea1.GdkWindow.DrawLine(drawingarea1.Style.BaseGC(StateType.Normal), 0, 0, 400, 300);

	}
}
