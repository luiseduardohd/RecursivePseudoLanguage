using System;
using System.Text.RegularExpressions;

namespace ParserSubstituciones
{
	public class FunctionImplementation : Sentence
	{

		public static FunctionImplementation parse(string text)
		{
			FunctionImplementation result = null;
			string pattern = Parser.RegularExpressions.functionImplementation;
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

