using UIKit;
using Foundation;
using ObjCRuntime;
using CoreAnimation;
using CoreGraphics;
using System.Collections.Generic;
using System;

public class Globals
{

	public interface DemoTableViewControllerInterface : PullRefreshTableViewControllerInterface {
		//NSMutableArray items { get; set;}
		List<object> items { get; set;}

	}



	////cImport("DemoTableViewController.h");


	public class DemoTableViewController : PullRefreshTableViewController ,  DemoTableViewControllerInterface{
		public List<object> items { get; set;}


		public override void ViewDidLoad() {
			base.ViewDidLoad();

			this.Title = "Pull to Refresh";

			this.items =new List<object>(new object[]{"What time is it?"});
		}		
		[Export ("numberOfSectionsInTableView:")]
		public int SectionsIn(UITableView tableView ){
			return 1;
		}
		[Export ("tableView:numberOfRowsInSection:")]
		public int NumberOfRowsInSection(UITableView tableView, /*numberOfRowsInSection*/ int section) {
			return items.Count;
		}
		[Export ("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell cellForRowAtIndexPath(UITableView tableView, /*cellForRowAtIndexPath*/ NSIndexPath indexPath) {

			/*static*/ string CellIdentifier = "CellIdentifier";
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			if (cell ==  null ) {
				cell = new UITableViewCell /*()WithStyle*/(UITableViewCellStyle.Default, /*reuseIdentifier*/ CellIdentifier /*autorelease*/);
			}

			cell.TextLabel.Text = (string)items[indexPath.Row];
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		public override void refresh() {
			this.PerformSelector(new Selector("addItem"),  null,  2.0f);
		}

		[Export("addItem")]
		public void addItem() {

			NSDateFormatter dateFormatter = new NSDateFormatter() /*alloc*/  /*autorelease*/;
			dateFormatter.TimeStyle = NSDateFormatterStyle.Medium;
			string now = dateFormatter.ToString(NSDate.Now);
			items.Add(string.Format("{0}", now));

			this.TableView.ReloadData();

			this.stopLoading();
		}

		public override void dealloc() {
//			items.release();
//			base.dealloc();
		}

	}


	////cImport("UIKit/UIKit.h");


	public interface PullRefreshTableViewControllerInterface  {
		UIView refreshHeaderView{ get; set;}
		UILabel refreshLabel{ get; set;}
		UIImageView refreshArrow{ get; set;}
		UIActivityIndicatorView refreshSpinner{ get; set;}
		bool  isDragging{ get; set;}
		bool  isLoading{ get; set;}
		string textPull{ get; set;}
		string textRelease{ get; set;}
		string textLoading{ get; set;}

		void setupStrings();
		void addPullToRefreshHeader();
		void startLoading();
		void stopLoading();
		void refresh();

	}


	//cImport("QuartzCore/QuartzCore.h");
	//cImport("PullRefreshTableViewController.h");


	public static float REFRESH_HEADER_HEIGHT = 52.0f;


	public class PullRefreshTableViewController : UITableViewController, PullRefreshTableViewControllerInterface  {

	//@synthesize textPull, textRelease, textLoading, refreshHeaderView, refreshLabel, refreshArrow, refreshSpinner;
		public UIView refreshHeaderView{ get; set;}
		public UILabel refreshLabel{ get; set;}
		public UIImageView refreshArrow{ get; set;}
		public UIActivityIndicatorView refreshSpinner{ get; set;}
		public bool  isDragging{ get; set;}
		public bool  isLoading{ get; set;}
		public string textPull{ get; set;}
		public string textRelease{ get; set;}
		public string textLoading{ get; set;}

	public PullRefreshTableViewController/*()WithStyle*/( UITableViewStyle style  ):base(style){
		//this =  base  /*()WithStyle*/(style);
		//if (this !=  null ) {
			this.setupStrings();
		//}
		//return this;
	}

	public  PullRefreshTableViewController(NSCoder aDecoder):base(aDecoder) {
		//this =  base  ()WithCoder(aDecoder;
		//if (this !=  null ) {
			this.setupStrings();
		//}
		//return this;
	}

	public PullRefreshTableViewController(string nibNameOr= null,  /*bundle*/NSBundle nibBundleOr = null ):base(nibNameOr,nibBundleOr) {
		//this =  base  ()WithNibName(nibNameOr null  bundle(nibBundleOr null ;
		//if (this !=  null ) {
			this.setupStrings();
		//}
		//return this;
	}

	public override void ViewDidLoad() {
		base.ViewDidLoad();
		this.addPullToRefreshHeader();
	}

	public void setupStrings(){
		textPull = "Pull down to refresh...";
		textRelease = "Release to refresh...";
		textLoading = "Loading...";
	}
	
	public void addPullToRefreshHeader () {
		refreshHeaderView = new UIView (new CGRect(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT));
		refreshHeaderView.BackgroundColor = UIColor.Clear;

		refreshLabel = new UILabel(new CGRect (0, 0, 320, REFRESH_HEADER_HEIGHT));
		refreshLabel.BackgroundColor = UIColor.Clear;
		refreshLabel.Font = UIFont.BoldSystemFontOfSize(12.0f);
		refreshLabel.TextAlignment = UITextAlignment.Center;

			refreshArrow = new UIImageView( UIImage.FromBundle("arrow.png"));
		refreshArrow.Frame = new CGRect(Math.Floor((REFRESH_HEADER_HEIGHT - 27) / 2),
			(Math.Floor(REFRESH_HEADER_HEIGHT - 44) / 2),
			27, 44);

		refreshSpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
		refreshSpinner.Frame = new CGRect(Math.Floor(Math.Floor(REFRESH_HEADER_HEIGHT - 20) / 2), Math.Floor((REFRESH_HEADER_HEIGHT - 20) / 2), 20, 20);
		refreshSpinner.HidesWhenStopped = true;

		refreshHeaderView.AddSubview(refreshLabel);
		refreshHeaderView.AddSubview(refreshArrow);
		refreshHeaderView.AddSubview(refreshSpinner);
		this.TableView.AddSubview(refreshHeaderView);
	}
	[Export ("scrollViewWillBeginDragging:")]
	public void WillBeginDragging(UIScrollView scrollView) {
		if (isLoading) return;
		isDragging = true;
	}
	[Export ("scrollViewDidScroll:")]
	public void DidScroll( UIScrollView scrollView) {
		if (isLoading) {

			if (scrollView.ContentOffset.Y > 0)
				this.TableView.ContentInset = UIEdgeInsets.Zero;
			else if (scrollView.ContentOffset.Y >= -REFRESH_HEADER_HEIGHT)
				this.TableView.ContentInset = new UIEdgeInsets(-scrollView.ContentOffset.Y, 0, 0, 0);
		} else if (isDragging && scrollView.ContentOffset.Y < 0) {

			UIView.Animate(0.25 , () => {
				if (scrollView.ContentOffset.Y < -REFRESH_HEADER_HEIGHT) {
					// User is scrolling above the header
					refreshLabel.Text = this.textRelease;
					refreshArrow.Layer.Transform = CATransform3D.MakeRotation((nfloat)Math.PI, (nfloat)0.0f, (nfloat)0.0f, (nfloat)1.0f);
				} else { 

					refreshLabel.Text = this.textPull;
					refreshArrow.Layer.Transform = CATransform3D.MakeRotation((nfloat)Math.PI * 2, (nfloat)0.0f, (nfloat)0.0f, (nfloat)1.0f);
				}
			});
		}
	}
	[Export ("scrollViewDidEndDragging:willDecelerate:")]
	public void DidEndDragging(UIScrollView scrollView,  bool decelerate) {
		if (isLoading) return;
		isDragging = false;
			if (scrollView.ContentOffset.Y <= -REFRESH_HEADER_HEIGHT) {

			this.startLoading();
		}
	}

	public void startLoading() {
		isLoading = true;


		UIView.Animate(0.3f ,() => {
			this.TableView.ContentInset = new UIEdgeInsets(REFRESH_HEADER_HEIGHT, 0, 0, 0);
			refreshLabel.Text = this.textLoading;
			refreshArrow.Hidden = true;
			refreshSpinner.StartAnimating();
		});


		this.refresh();
	}
	
	[Export("stopLoading")]
	public void stopLoading (){
		isLoading = false;


		UIView.Animate(0.3f , () => {
			this.TableView.ContentInset = UIEdgeInsets.Zero;
			refreshArrow.Layer.Transform = CATransform3D.MakeRotation((nfloat)Math.PI * 2, (nfloat)0, (nfloat)0, (nfloat)1);
			},
				() => {
				this.PerformSelector(new Selector("stopLoadingComplete"));
				});
	}
	[Export("stopLoadingComplete")]
	public void stopLoadingComplete() {

		refreshLabel.Text = this.textPull;
		refreshArrow.Hidden = false;
		refreshSpinner.StopAnimating();
	}

	public virtual void refresh() {
						
		this.PerformSelector(new Selector("stopLoading") ,null  ,2.0f);
	}

	public virtual void dealloc() {
//		refreshHeaderView.release;
//		refreshLabel.release;
//		refreshArrow.release;
//		refreshSpinner.release;
//		textPull.release;
//		textRelease.release;
//		textLoading.release;
//		base.dealloc();
	}

	}


	//cImport("UIKit/UIKit.h");
	
	/*public interface PullToRefreshAppDelegate : UIApplicationDelegate {
		UIWindow window;
		UINavigationController navigationController;
	}

	//@property (nonatomic, retain) UIWindow window;
	//@property (nonatomic, retain) UINavigationController navigationController;

	}**/



	//cImport("PullToRefreshAppDelegate.h");
	//cImport("DemoTableViewController.h");

	[Register ("PullToRefreshAppDelegate")]
	public class PullToRefreshAppDelegate : UIApplicationDelegate{

		public override UIWindow Window {
			get;
			set;
		}
		public UINavigationController NavigationController {
			get;
			set;
		}

		//@synthesize window;
		//@synthesize navigationController;


		public override bool FinishedLaunching( UIApplication application, NSDictionary launchOptions) {

			CGRect screenBounds = UIScreen.MainScreen.Bounds;
			this.Window = new UIWindow( screenBounds);

			DemoTableViewController demoTableViewController = new DemoTableViewController();
			NavigationController = new UINavigationController(demoTableViewController);
			this.Window.RootViewController = NavigationController;
			Window.AddSubview(NavigationController.View);
			Window.MakeKeyAndVisible();

			return true;
		}

		public void dealloc() {
//			navigationController release;
//			window release;
//			base  de/*alloc*/;
		}


	}

	public class Application
	{
		// This is the main entry point of the application.
		public static void Main (string []args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "PullToRefreshAppDelegate");
		}
	}
}
