using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnBabelerCode
{
	public class UnBabeler
	{
		//		public UnbabelCode ()
		//		{
		//		}
		public class TupleList<T1, T2> : List<Tuple<T1, T2>>
		{
			public void Add( T1 item, T2 item2 )
			{
				Add( new Tuple<T1, T2>( item, item2 ) );
			}
		}

		public static string ObjectiveCToCSharp(string inputCode)
		{
			/*
			string result = "test";
			ObjectiveC.Unit objectiveCUnit = ObjectiveC.Unit.parse (inputCode);

			CSharp.Unit cSharpUnit = ObjectiveCToCSharp (objectiveCUnit);

			//result = cSharpUnit.ToString ();
			return result;
			*/
			string result = "";

			result = inputCode;

			//Substituciones de llamadas con corchetes en objective-C
			string prevText = "";
			do {
				prevText = result;
				//Cuando tienen parámetros
				var pattern = "\\[([^\\s\\[\\]]+)\\s([^\\s\\[\\]\\:]+)\\:([^\\[\\]]+)\\]";
				var replacement = "$1.$2($3)";
				result = Regex.Replace(result, pattern,replacement ,RegexOptions.Singleline);
				//Cuando no tienen parámetros
				//No me estaba funcionando cuando no tenían parametros
				pattern = "\\[([^\\[\\]]+)\\s([^\\[\\]]+)\\s*]";
				replacement = "$1.$2()";
				result = Regex.Replace(result, pattern,replacement ,RegexOptions.Singleline);
			} while (!result.Equals (prevText)) ;

			//Modificar constructores de tipos nativos que se van a convertir
			//Ejemplo:
			//NSString.stringWithFormat(@"%@", now)
			// new string(@"%@", now)
			//NS([^\.]+)\.([^\.]+)With([^\.]+)\(([^\(\)]+)\)
			//String.Format("{0}",now)

			//			TupleList<string,string> substituciones = new TupleList<string, string> ();
			//			var pattern = "";
			//			var replacement = "";
			//			substituciones.Add (new Tuple<string, string> ("",""));

			//Estas substituciones son antes de tener codigo c# compilable

			//http://stackoverflow.com/questions/6005609/replace-only-some-groups-with-regex
			TupleList<string,string> substituciones = new TupleList<string, string> ()
			{
				//init
				{ "-\\s*\\(\\s*id\\s*\\)\\s*init\\s*{", "public init( ){" },
				//initWith
				{ "-\\s*\\(\\s*id\\s*\\)\\s*initWith([^\\{\\}]+)\\s*{", "public init( $1 ){" },
				//ObjectiveC Method with parameters
				{ "-\\s*\\(([^\\)]+)\\)\\s*([^\\s\\:]+)\\:([^\\{]*){", "public $1 $2($3){" },
				//ObjectiveC Method without parameters
				{ "-\\s*\\(([^\\)]+)\\)\\s*([^\\s\\:]+)\\s*{", "public $1 $2(){" },
				//Objective c chef if self != null
				{ "(if\\s*\\(\\s*self\\s*!=\\s*nil\\s*\\)\\s*){([^\\{\\}]*)}", @"/*$1{*/
$2//}" },
				//Implementation
				{ "@implementation\\s*([^\\s\\n\\r]*)", "public class $1{" },
				//Interface
				//TODO:cambiar para hacer match con interface e implementation con named groups
				//http://stackoverflow.com/questions/10386656/check-equality-of-two-named-matching-groups-using-regular-expression
				{ "@interface\\s*([^\\s\\n\\r]*)", "public interface $1{" },
				//Method declaration on interface
				{ "-\\s*\\(([^\\(\\)]+)\\)([^\\;]+);", "public $1 $2();" },
				//init with a new
				{ "([^\\s\\.]*)\\.alloc\\(\\)\\.initWith([^\\(\\)])\\s*\\(", "new $1( $2: " },
				//init a new
				{ "([^\\s\\.]*)\\.alloc\\(\\)\\.init", "new $1" },
				//init a new
				{ "([^\\s\\.]*)\\.alloc\\(\\)\\.init", "new $1" },

				{ Regex.Escape("NSString.stringWithFormat"), "String.Format" },
				{ Regex.Escape("@\"%@\""), "\"{0}\"" },
				{ Regex.Escape(".autorelease()"), "" },
				{ Regex.Escape("@synthesize"), "//@synthesize" },
				{ Regex.Escape("UIWindow *window;"), "public override UIWindow Window{get;set;}" },
				{ Regex.Escape("UINavigationController *navigationController;"), "public override UINavigationController NavigationController{get;set;}" },
				{ Regex.Escape("int main(int argc, char *argv[]) {"), "public static void Main (string []args)" },
				//int main(int argc, char * argv[]) {
				{ "int\\s+main\\s*\\(\\s*int\\s+argc\\s*,\\s*char\\s*\\*\\s*argv\\s*\\[\\s*\\]\\s*\\)\\s*{", "public static void Main (string []args){" },
				//
				{ "(@autoreleasepool {)([^\\{\\}]*)(})", "//$1\n$2//$3" },
				//return UIApplicationMain(argc, argv, nil, NSStringFromClass(AppDelegate.class()));
				{ "return\\s*UIApplicationMain\\s*\\(\\s*argc\\s*,\\s*argv\\s*,\\s*nil\\s*,\\s*NSStringFromClass\\s*\\(\\s*([^\\s\\.]*)\\s*\\.\\s*class\\s*\\(\\s*\\)\\s*\\)\\s*\\)\\s*;", "UIApplication.Main (args, null, typeof($1).Name);" },
				{ Regex.Escape("NSAutoreleasePool * pool = [[NSAutoreleasePool alloc] init];"), "//NSAutoreleasePool * pool = [[NSAutoreleasePool alloc] init];" },
				{ Regex.Escape("int retVal = UIApplicationMain(argc, argv, nil, @\"PullToRefreshAppDelegate\");"), "UIApplication.Main (args, null, \"PullToRefreshAppDelegate\");" },
				{ Regex.Escape("[pool release];"), "//[pool release];" },
				{ Regex.Escape("return retVal;"), "//return retVal;" },
				{ Regex.Escape("(NSString *)"), "string " },
				{ Regex.Escape("(NSBundle *)"), "NSBundle " },
				{ Regex.Escape("(NSMutableArray *)"), "List<object> " },
				{ Regex.Escape("(NSCoder *)"), "NSCoder " },
				{ Regex.Escape("(NSDictionary *)"), "NSCoder " },
				{ Regex.Escape("(NSIndexPath *)"), "NSIndexPath " },
				{ Regex.Escape("(UITableView *)"), "UITableView " },
				{ Regex.Escape("(UIApplication *)"), "UIApplication " },
				{ Regex.Escape("(NSInteger)"), " nint " },
				{ Regex.Escape("UIView *"), "UIView " },
				{ Regex.Escape("UILabel *"), "UILabel " },
				{ Regex.Escape("UIImageView *"), "UIImageView " },
				{ Regex.Escape("UIActivityIndicatorView *"), "UIActivityIndicatorView " },
				{ Regex.Escape("NSString *"), "NSString " },
				{ Regex.Escape("UIActivityIndicatorView *"), "UIActivityIndicatorView " },
				{ Regex.Escape("UIActivityIndicatorView *"), "UIActivityIndicatorView " },
				{ Regex.Escape("(UITableViewStyle)"), "UITableViewStyle " },
				{ Regex.Escape("cImport"), "//cImport" },
				{ Regex.Escape("#import"), "//#import" },
				{ Regex.Escape("@end"), "}" },
				{ Regex.Escape("NSInteger"), "nint" },
				{ Regex.Escape("@\""), "\"" },
				{ Regex.Escape("self"), "this" },
				{ Regex.Escape("backgroundColor"), "BackgroundColor" },
				{ Regex.Escape("frame"), "Frame" },
				{ Regex.Escape("hidesWhenStopped"), "HidesWhenStopped" },
				{ Regex.Escape("tableView"), "TableView" },
				{ Regex.Escape("font"), "Font" },
				{ Regex.Escape("textAlignment"), "TextAlignment" },
				{ Regex.Escape("rootViewController"), "RootViewController" },
				{ Regex.Escape("super"), "base" },
				{ Regex.Escape("hidden"), "Hidden" },
				{ Regex.Escape("contentInset"), "ContentInset" },
				{ Regex.Escape("contentOffset"), "ContentOffset" },
				{ Regex.Escape("NSString"), "string" },
				{ Regex.Escape("UITableViewCellStyleDefault"), "UITableViewCellStyle.Default" },
				//{ Regex.Escape("init"), "Init" },
				{ Regex.Escape("viewDidLoad"), "ViewDidLoad" },
				{ Regex.Escape("nil"), "null" },
				{ Regex.Escape("UITableViewCellSelectionStyleNone"), "UITableViewCellSelectionStyle.None" },
				//{ Regex.Escape("WithFormat"), ".Format" },
				{ "NSObject <UIApplicationDelegate>", "UIApplicationDelegate" },
				{ Regex.Escape("WithFrame"), "" },
				{ Regex.Escape("boldSystemFontOfSize"), "BoldSystemFontOfSize" },
				{ Regex.Escape("UIActivityIndicatorViewStyleGray"), "UIActivityIndicatorViewStyle.Gray" },
				{ Regex.Escape("addSubview"), "AddSubview" },
				{ Regex.Escape("animateWithDuration"), "Animate" },
				{ Regex.Escape("^{"), "() => {" },
				{ "@selector\\(([^\\)]+)\\)", "new Selector(\"$1\")" },
				{ Regex.Escape("performSelector"), "PerformSelector" },
				{ "setTimeStyle\\(([^\\)]+)\\);", "TimeStyle = $1;" },
				{ Regex.Escape("NSDateFormatterMediumStyle"), "NSDateFormatterStyle.Medium" },
				{ Regex.Escape("stringFromDate"), "ToString" },
				{ Regex.Escape("NSDate.date"), "NSDate.Now" },
				{ Regex.Escape("CATransform3DMakeRotation"), "CATransform3D.MakeRotation" },
				{ "^\\(([^\\)]+)\\)", "(\"$1\") => " },
				{ Regex.Escape("animateWithDuration"), "Animate" },
				{ Regex.Escape("animateWithDuration"), "Animate" },
				//{ "@synthesize ([^,;)]+)", "public object \"$1\"{ get; set;} @synthesize " },
				{ Regex.Escape("title"), "Title" },
				//{ Regex.Escape("textLabel"), "Text" },
				{ Regex.Escape("selectionStyle"), "SelectionStyle" },
				{ Regex.Escape("count"), "Count" },
				{ Regex.Escape("NSTextAlignmentCenter"), "NSTextAlignment.Center" },
				//{ Regex.Escape("text"), "Text" },
				{ Regex.Escape("floorf"), "Math.Floor" },
				{ Regex.Escape("YES"), "true" },
				{ Regex.Escape("NO"), "false" },
				{ Regex.Escape("contentOffset"), "ContentOffset" },
				{ Regex.Escape("startAnimating"), "StartAnimating" },
				{ Regex.Escape("UIEdgeInsetsZero"), "UIEdgeInsets.Zero" },
				{ Regex.Escape("M_PI"), "Math.PI" },
				{ Regex.Escape("string args"), "string []args" },
				{ Regex.Escape("makeKeyAndVisible"), "MakeKeyAndVisible" },
				{ Regex.Escape("NSTextAlignmentCenter"), "UITextAlignment.Center" },
				{ Regex.Escape("Insert"), "Add" },
				{ Regex.Escape("InsertObject"), "Insert" },
				{ Regex.Escape("MainScreen"), "Bounds" },
				{ Regex.Escape("MainScreen"), "Bounds" },
				{ Regex.Escape("applicationDidFinishedLaunching"), "FinishedLaunching" },
				{ Regex.Escape("UIWindow window"), "UIWindow Window" },
				{ Regex.Escape(".window"), ".Window" },
				//{ Regex.Escape("alloc"), "Alloc" },
				//{ Regex.Escape("init"), "Init" },
				{ Regex.Escape("@property (nonatomic, retain)"), "//@property (nonatomic, retain)" },
				{ Regex.Escape("@property (nonatomic, copy)"), "//@property (nonatomic, copy)" },
				//{ Regex.Escape("- (void)"), "public void " },
				//{ Regex.Escape("- (id)"), "public void " },
				{ Regex.Escape("(UIScrollView *)"), "UIScrollView " },
				{ Regex.Escape("(BOOL)"), "bool " },
				{ Regex.Escape("BOOL"), "bool " },
				{ Regex.Escape("Now()"), "Now" },
				{ Regex.Escape("NSDateFormatter *"), "NSDateFormatter " },
				{ Regex.Escape("%@"), "{}" },
				//http://stackoverflow.com/questions/14482341/c-net-and-sprintf-syntax
				//int i=0;input=Regex.Replace(input,"%.",m=>("{"+ ++i +"}"));
				//{ Regex.Escape("Make"), ".Make" },
				{ Regex.Escape("CGRectMake"), "new CGRect" },
				{ Regex.Escape("UIEdgeInsetsMake"), "new UIEdgeInsets" },
				{ Regex.Escape("(:"), "(" },
				{ Regex.Escape("ContentOffset.y"), "ContentOffset.Y" },
				{ Regex.Escape("ContentOffset.x"), "ContentOffset.Y" },
				{ "[\\s\\(\\)]([^\\s\\:\\[\\]\\(\\)]+)\\:", " ,/*$1*/ " },
				{ "\\sid\\s", " object," },
				{ "\\(\\s*,", "(" },
				//    navigationController.release();
				{ "([^\\s\\.]+)\\.release\\(\\);", "/*$1.release();*/" },
				//    base.dealloc();
				{ "([^\\s\\.]+)\\.dealloc\\(\\);", "/*$1.dealloc();*/" },
				//Constructor with call to base class
				{ "{[\\s\\n\\r]*this = base\\.initWith([^\\;\\(\\)]+)\\(([^\\(\\)]+)\\)", @":base( /*$1:*/ $2){" },
				{ Regex.Escape(".mainScreen()"), ".MainScreen" },
				{ Regex.Escape(".bounds()"), ".Bounds" },
				{ Regex.Escape("imageNamed"), "FromBundle" },
				{ Regex.Escape("clearColor()"), "Clear" },
				{ Regex.Escape("layer()"), "Layer" },
				{ Regex.Escape("transform"), "Transform" },

				//{ Regex.Escape("static"), "/*static*/" },
				//Falla con public static void main

				//    Numeros decimales agregarle el f
				{ "([0-9](?:[.,][0-9]{1,3})?)", "$1f" },
				{ Regex.Escape("NSAutoreleasePool * pool = new NSAutoreleasePool();"), "//NSAutoreleasePool * pool = new NSAutoreleasePool();" },
			};
			//Agregar sprintf y regexp match action
			//Hay que cambiar los tipos NSString, NSMutableArray por tipos nativos:string y list
			//Checar si algo se requiere poner lazy o greedy
			//http://stackoverflow.com/questions/2301285/what-do-lazy-and-greedy-mean-in-the-context-of-regular-expressions

			foreach( var tuple in substituciones )
			{
				// pattern, replacement
				var pattern = tuple.Item1;
				var replacement = tuple.Item2;
				result = Regex.Replace( result, pattern, replacement, RegexOptions.Singleline );
				//Single line para que tome el . como cualquier caracter y no ignorar el salto de línea
				//Ver si se usa:
				//http://omegacoder.com/?p=147
			}

			//TODO: aqui se deben de hacer substituciones de cuando ya se tiene un codigo c# compilable

			//			TupleList<string,string> substituciones = new TupleList<string, string> ()
			//			{
			//			};

			//			foreach( var tuple in substituciones )
			//			{
			//				// pattern, replacement
			//				var pattern = tuple.Item1;
			//				var replacement = tuple.Item2;
			//				result = Regex.Replace( result, pattern, replacement, RegexOptions.Singleline );
			//				//Single line para que tome el . como cualquier caracter y no ignorar el salto de línea
			//				//Ver si se usa:
			//				//http://omegacoder.com/?p=147
			//			}

			result = @"
using UIKit;
using Foundation;
using ObjCRuntime;
using CoreAnimation;
using CoreGraphics;
using System.Collections.Generic;
using System;

public partial class Globals
{
"+result+@"
}
";

			return result;
		}

		public static CSharp.Unit ObjectiveCToCSharp(ObjectiveC.Unit objectiveCUnit)
		{
			return new CSharp.Unit ();
		}

	}
}