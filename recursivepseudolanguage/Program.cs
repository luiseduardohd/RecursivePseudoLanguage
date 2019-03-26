
using System;
using System.Diagnostics;
using ParserSubstituciones;

namespace RecursivePseudoLanguage.Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("ReplaceableString.Test");
			test4 ();
		}

		public static bool test4 ()
		{

			//			foreach (var sentencePattern in ParserSubstituciones.Parser.RegularExpressions.sentencesPatterns) {
			//				Console.WriteLine ("sentencePattern:" + sentencePattern);
			//			}
			string text  = @"
objcImport _Parentesis_ ;
objcImport _Parentesis_ ;

@interface DemoTableViewController  @end


objcImport _Parentesis_ ;


@implementation DemoTableViewController @end


objcImport _Parentesis_ ;


@interface PullRefreshTableViewController @end

objcImport _Parentesis_ ;
objcImport _Parentesis_ ;

float  REFRESH_HEADER_HEIGHT ;


@implementation PullRefreshTableViewController @end

objcImport _Parentesis_ ;

@interface PullToRefreshAppDelegate @end

objcImport _Parentesis_ ;
objcImport _Parentesis_ ;


@implementation PullToRefreshAppDelegate @end



objcImport _Parentesis_ ;

int main _Parentesis_ _Cuerpo_
";
			//			ParserSubstituciones.Unit unit = ParserSubstituciones.Unit.parse (new ParserSubstituciones.Text(text));
			ParserSubstituciones.Unit unit = ParserSubstituciones.Unit.parse (text);
			return true;
		}

		public static bool test3 ()
		{
			string text = "";
			SystemX.ReplaceableString replaceableString = null;


			text = @"
@implementation PullRefreshTableViewController

@synthesize textPull, textRelease, textLoading, refreshHeaderView, refreshLabel, refreshArrow, refreshSpinner;

- (id)initWithStyle:(UITableViewStyle)style {
  self = [super initWithStyle:style];
  if (self != nil) {
    [self setupStrings];
  }
  return self;
}

- (id)initWithCoder:(NSCoder *)aDecoder {
  self = [super initWithCoder:aDecoder];
  if (self != nil) {
    [self setupStrings];
  }
  return self;
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
  self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
  if (self != nil) {
    [self setupStrings];
  }
  return self;
}

- (void)viewDidLoad {
  [super viewDidLoad];
  [self addPullToRefreshHeader];
}

- (void)setupStrings{
  textPull = [[NSString alloc] initWithString:@""Pull down to refresh...""];
  textRelease = [[NSString alloc] initWithString:@""Release to refresh...""];
  textLoading = [[NSString alloc] initWithString:@""Loading...""];
}

- (void)addPullToRefreshHeader {
    refreshHeaderView = [[UIView alloc] initWithFrame:CGRectMake(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)];
    refreshHeaderView.backgroundColor = [UIColor clearColor];

    refreshLabel = [[UILabel alloc] initWithFrame:CGRectMake(0, 0, 320, REFRESH_HEADER_HEIGHT)];
    refreshLabel.backgroundColor = [UIColor clearColor];
    refreshLabel.font = [UIFont boldSystemFontOfSize:12.0];
    refreshLabel.textAlignment = NSTextAlignmentCenter;

    refreshArrow = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@""arrow.png""]];
    refreshArrow.frame = CGRectMake(floorf((REFRESH_HEADER_HEIGHT - 27) / 2),
                                    (floorf(REFRESH_HEADER_HEIGHT - 44) / 2),
                                    27, 44);

    refreshSpinner = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleGray];
    refreshSpinner.frame = CGRectMake(floorf(floorf(REFRESH_HEADER_HEIGHT - 20) / 2), floorf((REFRESH_HEADER_HEIGHT - 20) / 2), 20, 20);
    refreshSpinner.hidesWhenStopped = YES;

    [refreshHeaderView addSubview:refreshLabel];
    [refreshHeaderView addSubview:refreshArrow];
    [refreshHeaderView addSubview:refreshSpinner];
    [self.tableView addSubview:refreshHeaderView];
}

- (void)scrollViewWillBeginDragging:(UIScrollView *)scrollView {
    if (isLoading) return;
    isDragging = YES;
}

- (void)scrollViewDidScroll:(UIScrollView *)scrollView {
    if (isLoading) {
        // Update the content inset, good for section headers
        if (scrollView.contentOffset.y > 0)
            self.tableView.contentInset = UIEdgeInsetsZero;
        else if (scrollView.contentOffset.y >= -REFRESH_HEADER_HEIGHT)
            self.tableView.contentInset = UIEdgeInsetsMake(-scrollView.contentOffset.y, 0, 0, 0);
    } else if (isDragging && scrollView.contentOffset.y < 0) {
        // Update the arrow direction and label
        [UIView animateWithDuration:0.25 animations:^{
            if (scrollView.contentOffset.y < -REFRESH_HEADER_HEIGHT) {
                // User is scrolling above the header
                refreshLabel.text = self.textRelease;
                [refreshArrow layer].transform = CATransform3DMakeRotation(M_PI, 0, 0, 1);
            } else { 
                // User is scrolling somewhere within the header
                refreshLabel.text = self.textPull;
                [refreshArrow layer].transform = CATransform3DMakeRotation(M_PI * 2, 0, 0, 1);
            }
        }];
    }
}

- (void)scrollViewDidEndDragging:(UIScrollView *)scrollView willDecelerate:(BOOL)decelerate {
    if (isLoading) return;
    isDragging = NO;
    if (scrollView.contentOffset.y <= -REFRESH_HEADER_HEIGHT) {
        // Released above the header
        [self startLoading];
    }
}

- (void)startLoading {
    isLoading = YES;
    
    // Show the header
    [UIView animateWithDuration:0.3 animations:^{
        self.tableView.contentInset = UIEdgeInsetsMake(REFRESH_HEADER_HEIGHT, 0, 0, 0);
        refreshLabel.text = self.textLoading;
        refreshArrow.hidden = YES;
        [refreshSpinner startAnimating];
    }];
    
    // Refresh action!
    [self refresh];
}

- (void)stopLoading {
    isLoading = NO;
    
    // Hide the header
    [UIView animateWithDuration:0.3 animations:^{
        self.tableView.contentInset = UIEdgeInsetsZero;
        [refreshArrow layer].transform = CATransform3DMakeRotation(M_PI * 2, 0, 0, 1);
    } 
                     completion:^(BOOL finished) {
                         [self performSelector:@selector(stopLoadingComplete)];
                     }];
}

- (void)stopLoadingComplete {
    // Reset the header
    refreshLabel.text = self.textPull;
    refreshArrow.hidden = NO;
    [refreshSpinner stopAnimating];
}

- (void)refresh {
    // This is just a demo. Override this method with your custom reload action.
    // Don't forget to call stopLoading at the end.
    [self performSelector:@selector(stopLoading) withObject:nil afterDelay:2.0];
}

- (void)dealloc {
    [refreshHeaderView release];
    [refreshLabel release];
    [refreshArrow release];
    [refreshSpinner release];
    [textPull release];
    [textRelease release];
    [textLoading release];
    [super dealloc];
}

@end
";//TODO: revisar los errores, son pocos , pero si hay.
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());

			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			return true;
		}

		public static bool test ()
		{
			string text = "";
			SystemX.ReplaceableString replaceableString = null;

			text = "";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "()";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "[]";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "{}";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");


			text = "[]()";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "(()())";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "[[][]]";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "{{}{}}";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

//			text = "(()[])";
//			replaceableString = RecursivePseudoLanguaje.Parse ( text );
//			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
//			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
//			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");


			text = "[[]()]";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "a (( b ) c )";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "REFRESH_HEADER_HEIGHT - 27";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "(REFRESH_HEADER_HEIGHT - 27)";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "(REFRESH_HEADER_HEIGHT - 27) / 2";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "floorf((REFRESH_HEADER_HEIGHT - 27) / 2)";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "CGRectMake(floorf((REFRESH_HEADER_HEIGHT - 27) / 2),(floorf(REFRESH_HEADER_HEIGHT - 44) / 2),27, 44)";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "{REFRESH_HEADER_HEIGHT - 27}";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "floorf[{REFRESH_HEADER_HEIGHT - 27} / 2]";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "(( in1 ) ),( ( in2 ) )";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "( [ in1 ] )";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "( [ in1 ] ),( [ in2 ] )";//Error
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");


			text = "[{}]({})";//Error
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");


			text = "[{ in1 } ],( { in2 } )";//Error
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "CGRectMake{floorf[{REFRESH_HEADER_HEIGHT - 27} / 2],(floorf{REFRESH_HEADER_HEIGHT - 44} / 2),27, 44}";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "refreshArrow.frame = CGRectMake(floorf((REFRESH_HEADER_HEIGHT - 27) / 2),(floorf(REFRESH_HEADER_HEIGHT - 44) / 2),27, 44);";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "refreshArrow.frame = CGRectMake(floorf((REFRESH_HEADER_HEIGHT - 27) / 2),\n                                    (floorf(REFRESH_HEADER_HEIGHT - 44) / 2),\n                                    27, 44);";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = @"
	refreshArrow = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@""arrow.png""]];
    refreshSpinner = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleGray];
    refreshSpinner.frame = CGRectMake(floorf(floorf(REFRESH_HEADER_HEIGHT - 20) / 2), floorf((REFRESH_HEADER_HEIGHT - 20) / 2), 20, 20);
    refreshSpinner.hidesWhenStopped = YES;";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = @"- (void)addPullToRefreshHeader {
    refreshHeaderView = [[UIView alloc] initWithFrame:CGRectMake(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)];
    refreshHeaderView.backgroundColor = [UIColor clearColor];

    refreshLabel = [[UILabel alloc] initWithFrame:CGRectMake(0, 0, 320, REFRESH_HEADER_HEIGHT)];
    refreshLabel.backgroundColor = [UIColor clearColor];
    refreshLabel.font = [UIFont boldSystemFontOfSize:12.0];
    refreshLabel.textAlignment = NSTextAlignmentCenter;

    refreshArrow = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@""arrow.png""]];
    refreshArrow.frame = CGRectMake(floorf((REFRESH_HEADER_HEIGHT - 27) / 2),
                                    (floorf(REFRESH_HEADER_HEIGHT - 44) / 2),
                                    27, 44);

    refreshSpinner = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleGray];
    refreshSpinner.frame = CGRectMake(floorf(floorf(REFRESH_HEADER_HEIGHT - 20) / 2), floorf((REFRESH_HEADER_HEIGHT - 20) / 2), 20, 20);
    refreshSpinner.hidesWhenStopped = YES;

    [refreshHeaderView addSubview:refreshLabel];
    [refreshHeaderView addSubview:refreshArrow];
    [refreshHeaderView addSubview:refreshSpinner];
    [self.tableView addSubview:refreshHeaderView];
}";
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = @"
@implementation PullRefreshTableViewController

@synthesize textPull, textRelease, textLoading, refreshHeaderView, refreshLabel, refreshArrow, refreshSpinner;

- (id)initWithStyle:(UITableViewStyle)style {
  self = [super initWithStyle:style];
  if (self != nil) {
    [self setupStrings];
  }
  return self;
}

- (id)initWithCoder:(NSCoder *)aDecoder {
  self = [super initWithCoder:aDecoder];
  if (self != nil) {
    [self setupStrings];
  }
  return self;
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
  self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
  if (self != nil) {
    [self setupStrings];
  }
  return self;
}

- (void)viewDidLoad {
  [super viewDidLoad];
  [self addPullToRefreshHeader];
}

- (void)setupStrings{
  textPull = [[NSString alloc] initWithString:@""Pull down to refresh...""];
  textRelease = [[NSString alloc] initWithString:@""Release to refresh...""];
  textLoading = [[NSString alloc] initWithString:@""Loading...""];
}

- (void)addPullToRefreshHeader {
    refreshHeaderView = [[UIView alloc] initWithFrame:CGRectMake(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)];
    refreshHeaderView.backgroundColor = [UIColor clearColor];

    refreshLabel = [[UILabel alloc] initWithFrame:CGRectMake(0, 0, 320, REFRESH_HEADER_HEIGHT)];
    refreshLabel.backgroundColor = [UIColor clearColor];
    refreshLabel.font = [UIFont boldSystemFontOfSize:12.0];
    refreshLabel.textAlignment = NSTextAlignmentCenter;

    refreshArrow = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@""arrow.png""]];
    refreshArrow.frame = CGRectMake(floorf((REFRESH_HEADER_HEIGHT - 27) / 2),
                                    (floorf(REFRESH_HEADER_HEIGHT - 44) / 2),
                                    27, 44);

    refreshSpinner = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleGray];
    refreshSpinner.frame = CGRectMake(floorf(floorf(REFRESH_HEADER_HEIGHT - 20) / 2), floorf((REFRESH_HEADER_HEIGHT - 20) / 2), 20, 20);
    refreshSpinner.hidesWhenStopped = YES;

    [refreshHeaderView addSubview:refreshLabel];
    [refreshHeaderView addSubview:refreshArrow];
    [refreshHeaderView addSubview:refreshSpinner];
    [self.tableView addSubview:refreshHeaderView];
}

- (void)scrollViewWillBeginDragging:(UIScrollView *)scrollView {
    if (isLoading) return;
    isDragging = YES;
}

- (void)scrollViewDidScroll:(UIScrollView *)scrollView {
    if (isLoading) {
        // Update the content inset, good for section headers
        if (scrollView.contentOffset.y > 0)
            self.tableView.contentInset = UIEdgeInsetsZero;
        else if (scrollView.contentOffset.y >= -REFRESH_HEADER_HEIGHT)
            self.tableView.contentInset = UIEdgeInsetsMake(-scrollView.contentOffset.y, 0, 0, 0);
    } else if (isDragging && scrollView.contentOffset.y < 0) {
        // Update the arrow direction and label
        [UIView animateWithDuration:0.25 animations:^{
            if (scrollView.contentOffset.y < -REFRESH_HEADER_HEIGHT) {
                // User is scrolling above the header
                refreshLabel.text = self.textRelease;
                [refreshArrow layer].transform = CATransform3DMakeRotation(M_PI, 0, 0, 1);
            } else { 
                // User is scrolling somewhere within the header
                refreshLabel.text = self.textPull;
                [refreshArrow layer].transform = CATransform3DMakeRotation(M_PI * 2, 0, 0, 1);
            }
        }];
    }
}

- (void)scrollViewDidEndDragging:(UIScrollView *)scrollView willDecelerate:(BOOL)decelerate {
    if (isLoading) return;
    isDragging = NO;
    if (scrollView.contentOffset.y <= -REFRESH_HEADER_HEIGHT) {
        // Released above the header
        [self startLoading];
    }
}

- (void)startLoading {
    isLoading = YES;
    
    // Show the header
    [UIView animateWithDuration:0.3 animations:^{
        self.tableView.contentInset = UIEdgeInsetsMake(REFRESH_HEADER_HEIGHT, 0, 0, 0);
        refreshLabel.text = self.textLoading;
        refreshArrow.hidden = YES;
        [refreshSpinner startAnimating];
    }];
    
    // Refresh action!
    [self refresh];
}

- (void)stopLoading {
    isLoading = NO;
    
    // Hide the header
    [UIView animateWithDuration:0.3 animations:^{
        self.tableView.contentInset = UIEdgeInsetsZero;
        [refreshArrow layer].transform = CATransform3DMakeRotation(M_PI * 2, 0, 0, 1);
    } 
                     completion:^(BOOL finished) {
                         [self performSelector:@selector(stopLoadingComplete)];
                     }];
}

- (void)stopLoadingComplete {
    // Reset the header
    refreshLabel.text = self.textPull;
    refreshArrow.hidden = NO;
    [refreshSpinner stopAnimating];
}

- (void)refresh {
    // This is just a demo. Override this method with your custom reload action.
    // Don't forget to call stopLoading at the end.
    [self performSelector:@selector(stopLoading) withObject:nil afterDelay:2.0];
}

- (void)dealloc {
    [refreshHeaderView release];
    [refreshLabel release];
    [refreshArrow release];
    [refreshSpinner release];
    [textPull release];
    [textRelease release];
    [textLoading release];
    [super dealloc];
}

@end
";//TODO: revisar los errores, son pocos , pero si hay.
			replaceableString = RecursivePseudoLanguage.Parse ( text );
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());

			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			return true;
		}


		public static bool test2 ()
		{
			string text = "";
			SystemX.ReplaceableString replaceableString = null;

			text = "";
			replaceableString = new SystemX.ReplaceableString ( text );
			Console.WriteLine ("replaceableString:" + replaceableString);
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "1";
			replaceableString = new SystemX.ReplaceableString ( text );
			//replaceableString = replaceableString.Replace (0, 1, "2");
			replaceableString.Replace (0, 1, "2");
			Console.WriteLine ("replaceableString:" + replaceableString);
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "123";
			replaceableString = new SystemX.ReplaceableString ( text );
			//replaceableString = replaceableString.Replace (0, 1, "2");
			replaceableString.Replace (1, 1, "4");
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "123";
			replaceableString = new SystemX.ReplaceableString ( text );
			//replaceableString = replaceableString.Replace (0, 1, "2");
			replaceableString.Replace (1, 1, "4");
			replaceableString.Replace (0, 3, "5");
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "123";
			replaceableString = new SystemX.ReplaceableString ( text );
			//replaceableString = replaceableString.Replace (0, 1, "2");
			replaceableString.Replace (1, 1, "46");
			replaceableString.Replace (0, 3, "57");
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			text = "(()())";
			replaceableString = new SystemX.ReplaceableString ( text );
			//replaceableString = replaceableString.Replace (0, 1, "2");
			replaceableString.Replace (1, 2, " _a1_ ");
			replaceableString.Replace (7, 2, " _a2_ ");
			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());
			Debug.Assert(text.Equals( replaceableString.ToString() ), "Error");

			return true;
		}
	}
}
