using System;
using System.Text.RegularExpressions;

namespace ParserSubstituciones
{
	public class Implementation : Sentence
	{
		public static Implementation parse(string text)
		{

			Implementation result = null;
			string pattern = Parser.RegularExpressions.implementation;
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
	}
}

