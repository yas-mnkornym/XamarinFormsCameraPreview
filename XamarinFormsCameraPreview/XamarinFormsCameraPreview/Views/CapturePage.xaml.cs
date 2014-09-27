using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFormsCameraPreview.Views
{
	public partial class CapturePage
	{
		CameraPreview preview_ = null;

		public CapturePage()
		{
			InitializeComponent();

			// The CameraPreview have to be created in the code behind.
			// The instance of camera preview uses platform-specified modules and it causes crash if it is defined in XAML.
			preview_ = new CameraPreview();
			preview_.PictureTaken += preview__PictureTaken;
			gridCameraPreview_.Children.Add(preview_);
		}

		void preview__PictureTaken(object sender, PictureTakenEventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () => {
				// create view image page
				var page = new ViewImagePage();
				var vm = new ViewModels.ViewImagePageViewModel {
					Image = e.Image.AsImageSource()
				};
				page.BindingContext = vm;

				// push view image page
				await Navigation.PushAsync(page);
			});
		}

		void OnCaptureButtonClicked(object sender, EventArgs e)
		{
			preview_.TakePicture();
		}
	}
}
