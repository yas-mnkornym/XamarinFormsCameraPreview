using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFormsCameraPreview.Models
{
	public interface IImage
	{
		ImageSource AsImageSource();
	}
}
