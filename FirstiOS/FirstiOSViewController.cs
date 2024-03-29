﻿using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Core;

namespace FirstiOS
{
	public partial class FirstiOSViewController : UIViewController
	{
		public FirstiOSViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle
		List<string> phoneNumbers = new List<string>();

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			CallButton.Enabled = false;
			string translatedNumber = "";
			TranslateButton.TouchUpInside += (sender, e) => 
			{
				translatedNumber = PhonewordTranslator
					.ToNumber(PhoneNumberText.Text);
				if (translatedNumber != null) {
					CallButton.SetTitle("Call " + translatedNumber, 
						UIControlState.Normal);
					CallButton.Enabled = true;
				}
				else {
					CallButton.SetTitle("Call", UIControlState.Normal);
					CallButton.Enabled = false;
				}
			};

			PhoneNumberText.ShouldReturn += delegate { 
				PhoneNumberText.ResignFirstResponder();
				return true;
			};

			CallButton.TouchUpInside += delegate 
			{
				var alertPrompt = new UIAlertView("Dial Number?", 
					"Do you want to call " + translatedNumber + "?",
					null, "No", "Yes");
				alertPrompt.Dismissed += (sender, e) =>  {
					if (e.ButtonIndex >= alertPrompt.FirstOtherButtonIndex) {
						NSUrl url = new NSUrl("tel:" + translatedNumber);
						phoneNumbers.Add(translatedNumber);
						UIApplication.SharedApplication.OpenUrl(url);
					}
				};

				alertPrompt.Show();
			};
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			var chController = segue.DestinationViewController as CallHistoryController;
			if (chController != null) {
				chController.PhoneNumbers = phoneNumbers;
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion

	}
}

