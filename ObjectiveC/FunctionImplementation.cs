using System;
using System.Text.RegularExpressions;

namespace ObjectiveC
{
	public class FunctionImplementation : Statement
	{
		public static string functionImplementation = @catch( identifier ) + space + @catch( identifier ) + space + @catch( identifier ) + space + @catch( identifier );
		public new static FunctionImplementation parse(string text)
		{
			FunctionImplementation result = null;
			string pattern = functionImplementation;
			RegexOptions options = RegexOptions.Singleline;
			Regex regex = new Regex (pattern,options);
			Match match = regex.Match (text);
			if (match.Success) {
				result = new FunctionImplementation ();
				result.type = match.Groups [1].Value;
				result.name = match.Groups [2].Value;
				result.body = match.Groups [3].Value;
			} else {
				throw new ArgumentException ( "Bad sintax" );
			}
			return result;
		}

		public string type;
		public string name;
		public string body;
	}
}

