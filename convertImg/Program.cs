using System;
using System.Drawing;
using System.IO;
using Cairo;

namespace convertImg
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			if (args.Length > 0)
			{
				using (var bitmap = new ImageSurface( args[0] ))
				{
					int width = bitmap.Width;
					int height = bitmap.Height;
					string output = System.IO.Path.ChangeExtension(args[0],".img");

					FileStream img = File.Create(output.Replace(".img","_ND.img"));
					FileStream img_inv = File.Create(output.Replace(".img","_inverse_ND.img"));
					img.Write(new byte[]{077, 083, 079, 076, 032, 032, 000, 008},0,8);
					img_inv.Write(new byte[]{077, 083, 079, 076, 032, 032, 000, 008},0,8);
					int w1 = width & 0x000000FF;
					int w2 = (width >> 8) & 0x000000FF;
					int h1 = height & 0x000000FF;
					int h2 = (height >> 8) & 0x000000FF;
					byte bw1 = (byte)(Math.Min(255,Math.Max(0,w1))); 
					byte bw2 = (byte)(Math.Min(255,Math.Max(0,w2))); 
					byte bh1 = (byte)(Math.Min(255,Math.Max(0,h1))); 
					byte bh2 = (byte)(Math.Min(255,Math.Max(0,h2))); 
					img.Write(new byte[]{bw1, bw2, bh1, bh2,0,0,0,0},0,8);
					img_inv.Write(new byte[]{bw1, bw2, bh1, bh2,0,0,0,0},0,8);
					byte [] pix = bitmap.Data;
					int idx = 0;
					for (int h= 0;h<height;h++)
						for (int w=0;w<width;w++)
					{
						if (bitmap.Format == Format.Argb32)
						{

							byte b = pix[idx++];
							byte g = pix[idx++];
							byte r = pix[idx++];
							byte a = pix[idx++];
					
							int bc = (b>>5) & 0x7;
							bc = bc | (((g>>5) & 0x7) << 3);
							bc = bc | (((r>>5) & 0x7) << 5);
							byte final = (byte)Math.Min(255,Math.Max(0,bc));
							img.Write(new byte[]{final},0,1);
							int f = (int)final;
							int inv = ~f;
							inv = inv & 0x000000FF;


							img_inv.Write(new byte[]{(byte)inv},0,1);
						}

					}
					img.Close();
					img_inv.Close();
				}
			}
		}
	}
}
