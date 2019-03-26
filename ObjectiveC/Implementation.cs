using System;
using System.Text.RegularExpressions;

namespace ObjectiveC
{
	public class Implementation : Statement
	{
		public static string implementation = "@implementation"+  catchAnyExcept("@implementation","@end") +"@end";
		public new static Implementation parse(string text)
		{

			Implementation result = null;
			string pattern = implementation;
			RegexOptions options = RegexOptions.Singleline;
			Regex regex = new Regex (pattern,options);
			Match match = regex.Match (text);
			if (match.Success) {
				result = new Implementation ();
				result.body = match.Groups [1].Value;
			} else {
				throw new ArgumentException ( "Bad sintax" );
			}
			return result;
		}
		public string body;
		public static void parseBody(string text)
		{
		}
	}
}

