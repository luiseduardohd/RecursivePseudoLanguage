using System;
using System.Text.RegularExpressions;

namespace ParserSubstituciones
{
	public class FunctionCall : Sentence
	{
		public new static FunctionCall parse(string text)
		{
			FunctionCall result = null;
			string pattern = Parser.RegularExpressions.functionCall;
			RegexOptions options = RegexOptions.Singleline;
			Regex regex = new Regex (pattern,options);
			Match match = regex.Match (text);
			if (match.Success) {
				result = new FunctionCall ();
				result.caller = match.Groups [1].Value;
				result.parameters = match.Groups [2].Value;
			} else {
				throw new ArgumentException ( "Bad sintax" );
			}
			return result;
		}

		public string caller;
		public string parameters;
	}
}

