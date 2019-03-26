// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CocoaMacGUI
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButtonCell ConvertButton { get; set; }

		[Outlet]
		AppKit.NSTextView inputCode { get; set; }

		[Outlet]
		AppKit.NSTextView outputCode { get; set; }

		[Action ("convert:")]
		partial void convert (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (outputCode != null) {
				outputCode.Dispose ();
				outputCode = null;
			}

			if (inputCode != null) {
				inputCode.Dispose ();
				inputCode = null;
			}

			if (ConvertButton != null) {
				ConvertButton.Dispose ();
				ConvertButton = null;
			}
		}
	}
}
