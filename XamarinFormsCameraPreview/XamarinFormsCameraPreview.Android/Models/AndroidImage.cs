using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;

using XamarinFormsCameraPreview.Models;

[assembly: Dependency(typeof(XamarinFormsCameraPreview.Droid.Models.AndroidImage))]

namespace XamarinFormsCameraPreview.Droid.Models
{
	public class AndroidImage : IImage
	{
		public ImageSource ImageSource { get; set; }

		#region IImage ÉÅÉìÉoÅ[

		public ImageSource AsImageSource()
		{
			return ImageSource;
		}

		#endregion
	}
}