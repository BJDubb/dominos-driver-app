// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DominosFloat
{
	[Register ("ThirdViewController")]
	partial class ThirdViewController
	{
		[Outlet]
		UIKit.UIButton callBtn { get; set; }

		[Outlet]
		UIKit.UIButton completeDeliveryBtn { get; set; }

		[Outlet]
		UIKit.UIButton mapBtn { get; set; }

		[Outlet]
		MapKit.MKMapView mapView { get; set; }

		[Outlet]
		UIKit.UILabel nameLabel { get; set; }

		[Outlet]
		UIKit.UIButton openCameraBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (callBtn != null) {
				callBtn.Dispose ();
				callBtn = null;
			}

			if (mapBtn != null) {
				mapBtn.Dispose ();
				mapBtn = null;
			}

			if (mapView != null) {
				mapView.Dispose ();
				mapView = null;
			}

			if (nameLabel != null) {
				nameLabel.Dispose ();
				nameLabel = null;
			}

			if (openCameraBtn != null) {
				openCameraBtn.Dispose ();
				openCameraBtn = null;
			}

			if (completeDeliveryBtn != null) {
				completeDeliveryBtn.Dispose ();
				completeDeliveryBtn = null;
			}
		}
	}
}
