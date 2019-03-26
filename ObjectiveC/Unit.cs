using System;
using SystemX;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RecursivePseudoLanguage;
using System.Linq;
using BaseLanguage;

namespace ObjectiveC
{
	public class Unit  : Statement
	{
//		public string aaaa ="";
		public static string functionCall = @catch( identifier ) + space + @catch( identifier ) + maybeSpace+ ";";
		public static string @interface = "@interface"+ catchAnyExcept("@interface","@end") +"@end";
		public static string implementation = "@implementation"+  catchAnyExcept("@implementation","@end") +"@end";
//		public static string @interface = @catch( identifier ) + maybeSpace+ ";";
//		public static string implementation = @catch( identifier ) + maybeSpace+ ";";
		public static string functionImplementation = @catch( identifier ) + space + @catch( identifier ) + space + @catch( identifier ) + space + @catch( identifier );
		public static List<string> sentencesPatterns = new List<string> (){
			functionCall,
			@interface,
			implementation,
			functionImplementation
		};

		public Unit ( List<Statement> sentences)
		{
			this.sentences = sentences;
		}

		public List<Statement> sentences;

		public Unit ()
		{
		}

		public new static Unit parse(string text)
		{
			SystemX.ReplaceableString replaceableString = null;
			replaceableString = RecursivePseudoLanguage.RecursivePseudoLanguage.ToReplaceableString (text);
			return parse (replaceableString);
		}

//		public new static Unit parse2(string inputText)
//		{
//			//Revisar como se puede hacer para encontrar todos los tipos derivados
//			//TODO:revisar que se este parseando bien y salgan bien las excepciones, si no despues se parsea como otro tipo
//			#pragma warning disable 168
//			try{return Unit.convert(Number.parse( inputText ));}catch(Exception e){}
//			#pragma warning restore 168
//			return null;
//		}

		public static Unit parse(ReplaceableString replaceableString)
		{
//			string pattern = "";
			List<Match> matches = new List<Match> ();

//			IEnumerable<Unit> iEnumerableUnit= ReflectiveEnumerator.GetEnumerableOfType<Unit>(new object[]{});
//
//			var sentencesPatter = new List<Unit>

//			var type = typeof(Unit);
//			var types = AppDomain.CurrentDomain.GetAssemblies()
//				.SelectMany(s => s.GetTypes())
//				.Where(p => type.IsAssignableFrom(p));
//			var sentencesPatterns = from t in types select type.me

			foreach (var sentencesPattern in sentencesPatterns) {
				RegexOptions options = RegexOptions.Singleline;
				Regex regex = new Regex (sentencesPattern,options);
				matches.AddRange (from Match m in regex.Matches (replaceableString.text) select m);
			}

			matches.Sort( 
				delegate (Match t1, Match t2) 
				{
					return (t1.Index - t2.Index);
				}
			);

			List<Match> newMatches = new List<Match> ();

			foreach( Match match in matches )
			{
				if ( ! isInsideAnother( match , matches ) )
					newMatches.Add( match );
			}



			List<Statement> sentences = new List<Statement> ();
			foreach (var match in newMatches) {
//				var substitutions = replaceableString.getAllSubstitutions (match.Index, match.Length);
//				if (substitutions.Count >= 1) {
//					sentences.Add (Sentence.parse (substitutions[0]));
//				} else {
					sentences.Add (Statement.parse (match.Value));
//				}
			}

			return new Unit( sentences );
		}

		public static bool isInsideAnother( Match match, List<Match> matches )
		{
			foreach (Match matchOutside in matches) {
				bool result = intersect (match,matchOutside);
				if ( result ) {
					return true;
				}
			}
			return false;
		}

		public static bool intersect( Match matchInside , Match matchOutside )
		{
			if (matchOutside.Index <= matchInside.Index && (matchInside.Index + matchInside.Length) < (matchOutside.Index + matchOutside.Length) && matchInside != matchOutside) {
				return true;
			}

			return false;
		}
		public override string ToString(){
			return this.GetType().ToString();
		}


		public static Unit convert( BaseLanguage.Unit Unit)
		{
//			if (Unit is BaseLanguage.Number)
//				return new ObjectiveC.Number ((BaseLanguage.Number)Unit);
			return null;
		}
	}
}

