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
using Android.Hardware;

namespace XamarinFormsCameraPreview.Droid.Renderers
{
	public class DelegatePictureCallback : Java.Lang.Object, Camera.IPictureCallback
	{
		public Action<byte[], Camera> PictureTaken { get; set; }

		#region IPictureCallback ƒƒ“ƒo[

		public void OnPictureTaken(byte[] data, Camera camera)
		{
			if (PictureTaken != null) {
				PictureTaken(data, camera);
			}
		}
		#endregion
	}
}