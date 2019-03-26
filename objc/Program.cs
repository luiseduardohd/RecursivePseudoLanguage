using System;
using System.IO;
//using RecursivePseudoLanguage;
//using ParserSubstituciones;
//using RecursivePseudoLanguage =  ReecursivePseudoLanguage.RecursivePseudoLanguage;

namespace ObjectiveC
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			test2 ();
			Console.ReadLine();
		}

		public static void test2()
		{
			Console.WriteLine ("test2");
			string text = "";
			SystemX.ReplaceableString replaceableString = null;
			Unit unit = null;

			text = "items";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "*items";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "NSMutableArray *items";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "NSMutableArray *items;";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "super viewDidLoad";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[super viewDidLoad]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[super viewDidLoad];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "@\"Pull to Refresh\"";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "self.title";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "self.title = @\"Pull to Refresh\";";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "NSMutableArray";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "NSMutableArray alloc";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[NSMutableArray alloc]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[NSMutableArray alloc] initWithObjects:@\"What time is it?\", nil";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[[NSMutableArray alloc] initWithObjects:@\"What time is it?\", nil]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[[NSMutableArray alloc] initWithObjects:@\"What time is it?\", nil];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "items = [[NSMutableArray alloc] initWithObjects:@\"What time is it?\", nil];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "1";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "return 1";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "return 1;";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "items count";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[items count]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "return [items count]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "return [items count];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "static NSString *CellIdentifier";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "static NSString *CellIdentifier = @\"CellIdentifier\";";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "UITableViewCell *cell";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "tableView dequeueReusableCellWithIdentifier:CellIdentifier";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[tableView dequeueReusableCellWithIdentifier:CellIdentifier]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "nil";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "cell == nil";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "cell == nil";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "if (cell == nil){}";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "cell.textLabel.text = [items objectAtIndex:indexPath.row];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "cell.textLabel.text = [items objectAtIndex:indexPath.row];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "(addItem)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "@selector(addItem)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "self performSelector:@selector(addItem) withObject:nil afterDelay:2.0";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "NSDateFormatter *dateFormatter = [[[NSDateFormatter alloc] init] autorelease];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "NSString *now = [dateFormatter stringFromDate:[NSDate date]];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[items insertObject:[NSString stringWithFormat:@\"%@\", now] atIndex:0];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "REFRESH_HEADER_HEIGHT = 52.0f;";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "float REFRESH_HEADER_HEIGHT = 52.0f;";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "0 - REFRESH_HEADER_HEIGHT";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "CGRectMake(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[UIView alloc] initWithFrame:CGRectMake(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[[UIView alloc] initWithFrame:CGRectMake(0, 0 - REFRESH_HEADER_HEIGHT, 320, REFRESH_HEADER_HEIGHT)]";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "(REFRESH_HEADER_HEIGHT - 27)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "((REFRESH_HEADER_HEIGHT - 27) / 2)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "((REFRESH_HEADER_HEIGHT - 27) / 2)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "floorf((REFRESH_HEADER_HEIGHT - 27) / 2)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "refreshSpinner.hidesWhenStopped = YES;";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[refreshArrow layer].transform = CATransform3DMakeRotation(M_PI * 2, 0, 0, 1)";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "scrollView.contentOffset.y >= -REFRESH_HEADER_HEIGHT";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "isDragging && scrollView.contentOffset.y < 0";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "^{}";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[UIView animateWithDuration:0.25 animations:^{}];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "^(BOOL finished) {[self performSelector:@selector(stopLoadingComplete)];}";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "[self performSelector:@selector(stopLoading) withObject:nil afterDelay:2.0];";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

			text = "UINavigationController *navigationController;";
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );
			unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());

		}

		public static void test1()
		{
			Console.WriteLine ("Hello World!");

			//			string path = @"test1.m";
			string fileName = @"testObjectiveC1.m";
			string filePath = Path.Combine( Directory.GetCurrentDirectory(),fileName);
			//			string target = @"c:\temp";
			Console.WriteLine("The current directory is {0}", filePath);
			string text = "";
			if (File.Exists( filePath))
			{
				text = File.ReadAllText(filePath);
			}
			//			Console.WriteLine ("text:"+text);

			SystemX.ReplaceableString replaceableString = null;
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( text );

			Console.WriteLine ("replaceableString.text:" + replaceableString.text);
			Console.WriteLine ("replaceableString.ToString():" + replaceableString.ToString());

			//			Unit unit = Unit.parse( text );
			Unit unit = Unit.parse( replaceableString );
			Console.WriteLine (unit.ToString());
			//			var unit = new Unit (text);

		}
	}
}
