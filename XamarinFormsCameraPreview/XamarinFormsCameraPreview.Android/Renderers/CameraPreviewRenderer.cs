using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using XamarinFormsCameraPreview.Views;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(XamarinFormsCameraPreview.Droid.Renderers.CameraPreviewRenderer))]

namespace XamarinFormsCameraPreview.Droid.Renderers
{
	// To use generic version of the ViewRenderer, Xamarin.Forms have to be updated to 1.2.2.*
	public class CameraPreviewRenderer : ViewRenderer<CameraPreview, SurfaceView>, ISurfaceHolderCallback
	{
		Camera camera_ = null;

		protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null) {
				// get CamerPreview object and set event handler
				var preview = e.NewElement;
				preview.PictureRequired += preview_PictureRequired;

				// create and set surface view
				var surfaceView = new SurfaceView(Context);
				surfaceView.Holder.AddCallback(this);
				SetNativeControl(surfaceView);
			}
		}

		/// <summary>
		/// called when the picture is required
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void preview_PictureRequired(object sender, EventArgs e)
		{
			CameraPreview preview = sender as CameraPreview;
			if (camera_ != null && preview != null) {
				camera_.TakePicture(null, null, new DelegatePictureCallback {
					PictureTaken = (data, camera) => {
						// write jpeg data into a memory stream
						MemoryStream ms = null;
						try {
							ms = new MemoryStream(data.Length);
							ms.Write(data, 0, data.Length);
							ms.Flush();
							ms.Seek(0, SeekOrigin.Begin);

							// load image source from stream
							preview.OnPictureTaken(new Models.AndroidImage {
								ImageSource = ImageSource.FromStream(() => ms)
							});

							// NOTE: Do not dispose memory stream if it succeeded.
							// ImageSource is loaded in background so ms should not be disposed immediately.
						}
						catch {
							if (ms != null) {
								ms.Dispose();
							}
							throw;
						}
					}
				});
			}
		}

		#region ISurfaceHolderCallback ÉÅÉìÉoÅ[

		public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
		{
			if (camera_ != null) {
				camera_.StopPreview();
				camera_.SetPreviewDisplay(holder);
				camera_.StartPreview();
			}
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			try {
				camera_ = Camera.Open();
			}
			catch {
				camera_ = null;
			}

			if (camera_ != null) {
				camera_.SetPreviewDisplay(holder);
				camera_.StartPreview();
			}
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			if (camera_ != null) {
				camera_.StopPreview();
				camera_.Release();
				camera_.Dispose();
				camera_ = null;
			}
		}

		#endregion
	}
}