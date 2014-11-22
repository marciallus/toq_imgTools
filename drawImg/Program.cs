using System;
using Gtk;
using System.IO;

namespace drawImg
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();

			if (args.Length >0)
			{
				if (File.Exists(args[0]))
				{
					int idx = 0;
					byte [] bs = File.ReadAllBytes(args[0]);
					Console.WriteLine("M is {0:x}",bs[idx++]);
					Console.WriteLine("S is {0:x}",bs[idx++]);
					Console.WriteLine("O is {0:x}",bs[idx++]);
					Console.WriteLine("L is {0:x}",bs[idx++]);
					Console.WriteLine("  is {0:x}",bs[idx++]);
					Console.WriteLine("  is {0:x}",bs[idx++]);
					Console.WriteLine("Alpha is {0}",win.alpha = (bs[idx++]!=0));
					Console.WriteLine("8  is {0:x}",bs[idx++]);
					int w1 = (int)bs[idx++];
					Console.WriteLine("w1  is {0:x}",w1);
					int w2 = (int)bs[idx++];
					Console.WriteLine("w2  is {0:x}",w2);
					Console.WriteLine("Width is {0}",win.width = (w2*256+w1));
					int h1 = (int)bs[idx++];
					Console.WriteLine("h1  is {0:x}",h1);
					int h2 = (int)bs[idx++];
					Console.WriteLine("h2  is {0:x}",h2);
					Console.WriteLine("Height is {0}",win.height = (h2*16+h1));
					Console.WriteLine("0 is {0:x}",bs[idx++]);
					Console.WriteLine("0 is {0:x}",bs[idx++]);
					Console.WriteLine("0 is {0:x}",bs[idx++]);
					Console.WriteLine("0 is {0:x}",bs[idx++]);

					int idx2=0;
					int alpha=1;
					if (win.alpha)
						alpha=2;
					win.pixels = new byte[win.width*win.height*alpha];
					for (int w = 0;w<win.width*alpha;w++)
						for (int h = 0;h<win.height;h++)
					{
						win.pixels[idx2++] = bs[idx++];
					}

				}
			}



			win.Show ();
			Application.Run ();
		}
	}
}
