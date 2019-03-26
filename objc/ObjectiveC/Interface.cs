using System;
using System.Text.RegularExpressions;

namespace ObjectiveC
{
	public class Interface : Statement
	{
		public static string @interface = "@interface"+ catchAnyExcept("@interface","@end") +"@end";
		public new static Interface parse(string text)
		{
			Interface result = null;
			string pattern = @interface;
			RegexOptions options = RegexOptions.Singleline;
			Regex regex = new Regex (pattern,options);
			Match match = regex.Match (text);
			if (match.Success) {
				result = new Interface ();
				result.body = match.Groups [1].Value;
			} else {
				throw new ArgumentException ( "Bad sintax" );
			}
			return result;
		}
		public string body;
	}
}

