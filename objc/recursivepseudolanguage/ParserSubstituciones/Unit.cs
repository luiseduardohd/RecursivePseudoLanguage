using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace ParserSubstituciones
{
	public class Unit
	{
		public Unit ( List<Sentence> sentences)
		{
			this.sentences = sentences;
		}

		public List<Sentence> sentences;

		public static Unit parse(string text)
		{
			string pattern = "";
			List<Match> matches = new List<Match> ();



			foreach (var sentencesPattern in Parser.RegularExpressions.sentencesPatterns) {
				RegexOptions options = RegexOptions.Singleline;
				Regex regex = new Regex (sentencesPattern,options);
				matches.AddRange (from Match m in regex.Matches (text) select m);
			}

			matches.Sort( 
				delegate (Match t1, Match t2) 
				{
					return (t1.Index - t2.Index);
				}
			);
			List<Sentence> sentences = new List<Sentence> ();
			foreach (var match in matches) {
				sentences.Add ( Sentence.parse( match.Value ) );
			}
			return new Unit( sentences );
		}
	}
}

