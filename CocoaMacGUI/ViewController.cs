using System;
using System.IO;
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


			string fileName = @"testObjectiveC1";
			//string fileName = @"testObjectiveC1.m";
			string mainBundleDir = NSBundle.MainBundle.PathForResource(fileName, "m");
			string currentDirectory = Directory.GetCurrentDirectory();
			//string filePath = Path.Combine(mainBundleDir, fileName);
			string filePath = mainBundleDir;
			//			string target = @"c:\temp";
			Console.WriteLine("The current directory is {0}", filePath);
			if (File.Exists(filePath))
			{
                inputCode.TextStorage.SetString( new NSAttributedString( File.ReadAllText(filePath) ) );
			}

			//inputCode.TextStorage.Value = 
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
