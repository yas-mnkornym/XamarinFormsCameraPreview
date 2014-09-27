using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

[assembly: Dependency(typeof(XamarinFormsCameraPreview.WinPhone.Models.WPImage))]

namespace XamarinFormsCameraPreview.WinPhone.Models
{
	/// <summary>
	/// An IImage implementation of WP8.
	/// NOTE: Should handle Bitmap object or like that if it is needed.
	/// </summary>
	public class WPImage : XamarinFormsCameraPreview.Models.IImage
	{
		public ImageSource ImageSource { get; set; }

		#region IImage メンバー

		Xamarin.Forms.ImageSource XamarinFormsCameraPreview.Models.IImage.AsImageSource()
		{
			return ImageSource;
		}

		#endregion
	}
}
