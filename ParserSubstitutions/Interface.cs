using System;
using System.Text.RegularExpressions;

namespace ParserSubstituciones
{
	public class Interface : Sentence
	{
		public new static Interface parse(string text)
		{
			Interface result = null;
			string pattern = Parser.RegularExpressions.@interface;
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

