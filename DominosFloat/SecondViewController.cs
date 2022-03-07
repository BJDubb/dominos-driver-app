using System;
using System.Linq;
using CoreLocation;
using Foundation;
using UIKit;

namespace DominosFloat
{
    public partial class SecondViewController : UIViewController, ICLLocationManagerDelegate
    {
        public SecondViewController(IntPtr handle) : base(handle)
        {
        }
        CLLocationManager locationManager;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();

            if (CLLocationManager.LocationServicesEnabled)
            {
                locationManager.Delegate = this;
                locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
                locationManager.StartUpdatingLocation();
            }
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        [Export("locationManager:didUpdateLocations:")]
        public void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            var currentSpeed = locations.ToArray().First().Speed;

            CalculateSpeed(currentSpeed);
        }

        private void CalculateSpeed(double currentSpeed)
        {
            speedLabel.Text = (currentSpeed * 3.6).ToString("0");
        }
    }
}

