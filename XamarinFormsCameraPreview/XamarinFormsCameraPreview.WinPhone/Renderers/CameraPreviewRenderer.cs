using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Microsoft.Devices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

using XamarinFormsCameraPreview.Views;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(XamarinFormsCameraPreview.WinPhone.Renderers.CameraPreviewRenderer))]

namespace XamarinFormsCameraPreview.WinPhone.Renderers
{

	public class CameraPreviewRenderer : ViewRenderer<CameraPreview, ContentControl>
	{
		Canvas canvas_ = null;
		CameraPreview cameraPreview_ = null;
		PhotoCamera camera_ = null;

		public CameraPreviewRenderer()
		{
			// set control's event handler
			// these two will be called after the OnElementChanged with e.OldElement == null
			Loaded += CameraPreviewRenderer_Loaded;
			Unloaded += CameraPreviewRenderer_Unloaded;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
		{
			base.OnElementChanged(e);

			// execute only at the first time
			if (e.OldElement == null) {
				cameraPreview_ = e.NewElement; // NewElement should be an CameraPreview obejct
				
				// create a ContentControl
				var contentControl = new ContentControl();
				
				// create a ViewBox
				var viewBox = new Viewbox() {
					HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
					VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
					Stretch = System.Windows.Media.Stretch.Uniform
				};

				// create a canvas
				canvas_ = new Canvas();

				// make hierarchy
				viewBox.Child = canvas_;
				contentControl.Content = viewBox;

				// set the contentControl as the root native control
				SetNativeControl(contentControl);

				// register event handler of CameraPreview
				cameraPreview_.PictureRequired += cameraPreview_PictureRequired;
			}
		}

		/// <summary>
		/// Called when the picture is requred
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void cameraPreview_PictureRequired(object sender, EventArgs e)
		{
			if (camera_ != null) {
				camera_.CaptureImage();
			}
		}

		/// <summary>
		/// Called when the control is loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CameraPreviewRenderer_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			// initialize the camera
			try {
				camera_ = new PhotoCamera(CameraType.Primary); // the method throws exception when the initialization failed
			}
			catch {
				camera_ = null;
			}

			if (camera_ != null) {
				// set preview brush
				var previewResolution = camera_.PreviewResolution;
				canvas_.Width = previewResolution.Width;
				canvas_.Height = previewResolution.Height;

				var videoBrush = new System.Windows.Media.VideoBrush();
				videoBrush.SetSource(camera_);
				canvas_.Background = videoBrush;

				camera_.CaptureImageAvailable += camera__CaptureImageAvailable;
			}
		}

		/// <summary>
		/// Called when the control is unloaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CameraPreviewRenderer_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
			// cleanup the camera
			if (camera_ != null) {
				var camera = camera_;
				camera_ = null;
				camera.Dispose();
			}
		}

		/// <summary>
		/// Called just after the picture is taken
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void camera__CaptureImageAvailable(object sender, ContentReadyEventArgs e)
		{
			var image = new Models.WPImage {
				ImageSource = ImageSource.FromStream(() => e.ImageStream)
			};
			cameraPreview_.OnPictureTaken(image);
		}
	}
}
