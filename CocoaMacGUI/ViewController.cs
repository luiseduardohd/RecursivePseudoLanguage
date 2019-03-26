using System;

using AppKit;
using Foundation;
using UnBabelerCode;


namespace CocoaMacGUI
{
	public partial class ViewController : NSViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view.


		}

		partial void convert (Foundation.NSObject sender)
		{

			//Console.WriteLine("Got doubleclick");
			var input = this.inputCode.TextStorage.Value;
			var output = UnBabeler.ObjectiveCToCSharp (input);
			this.outputCode.TextStorage.SetString( new NSAttributedString(output));
			//TODO: Agregar fragaria para que se vea bonito

			const string message =
				"Convertion Done";
			const string caption = "ObjC2CSharp";
//			MessageBox.Show(message, caption,
//				MessageBoxButtons.OK,
//				MessageBoxIcon.Question);
		}

		public override NSObject RepresentedObject {
			get {
				return base.RepresentedObject;
			}
			set {
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}
	}
}
