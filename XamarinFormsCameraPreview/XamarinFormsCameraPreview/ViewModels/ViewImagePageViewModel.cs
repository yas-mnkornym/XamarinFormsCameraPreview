using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace XamarinFormsCameraPreview.ViewModels
{
	public class ViewImagePageViewModel : INotifyPropertyChanged
	{
		#region Bindings

		#region Image
		ImageSource image_ = null;

		public ImageSource Image
		{
			get
			{
				return image_;
			}
			set
			{
				if (image_ != value) {
					image_ = value;
					NotifyPropertyChanged();
				}
			}
		}
		#endregion

		#endregion // Bindings


		#region INotifyPropertyChanged メンバー
		void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
