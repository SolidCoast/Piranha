using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;

namespace Piranha.Drawing
{
	/// <summary>
	/// Image utils for scaling and cropping images.
	/// </summary>
	public class ImageUtils
	{
		/// <summary>
		/// Resizes the given image to the given size. If the size is larger than the original the
		/// image is returned unchanged.
		/// </summary>
		/// <param name="img">The image to resize</param>
		/// <param name="width">The desired width</param>
		/// <returns>The resized image</returns>
		public static Image Resize(Image img, int width) {
			if (width < img.Width) {
				int height = Convert.ToInt32(((double)width / img.Width) * img.Height) ;

				using (Bitmap bmp = new Bitmap(width, height)) {
					Graphics grp = Graphics.FromImage(bmp) ;

					grp.SmoothingMode = SmoothingMode.HighQuality ;
					grp.CompositingQuality = CompositingQuality.HighQuality ;
					grp.InterpolationMode = InterpolationMode.High ;

					// Resize and crop image
					Rectangle dst = new Rectangle(0, 0, bmp.Width, bmp.Height) ;
					grp.DrawImage(img, dst, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel) ;

					// Save new image to memory
					MemoryStream mem = new MemoryStream() ;
					bmp.Save(mem, img.RawFormat) ;
					mem.Position = 0;

					return Image.FromStream(mem) ;
				}
			}
			return img ;
		}

		/// <summary>
		/// Resizes and crops the image to the given dimensions
		/// </summary>
		/// <param name="img">The image</param>
		/// <param name="width">The desired width</param>
		/// <param name="height">The desired height</param>
		/// <returns>The resized image</returns>
		public static Image Resize(Image img, int width, int height) {
			var xRatio = width / (double)img.Width ;
			var yRatio = height / (double)img.Height ;

			if (img.Height * xRatio < height)
				img = Resize(img, Convert.ToInt32(img.Width * yRatio)) ;
			else img = Resize(img, Convert.ToInt32(width)) ;

			var newRect = new Rectangle(((width - img.Width) / 2) * -1, ((height - img.Height) / 2) * -1, width, height) ;
			var orgBmp = new Bitmap(img) ;
			var crpBmp = orgBmp.Clone(newRect, img.PixelFormat) ;

			return (Image)crpBmp ;
		}
	}
}
