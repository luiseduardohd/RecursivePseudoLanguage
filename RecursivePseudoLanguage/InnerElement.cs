using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SystemX;
using String = SystemX.String;

namespace RecursivePseudoLanguage
{
	public class InnerElement:Element
	{

		protected string innerPattern = "";
		public string InnerPattern{
			get {
				return innerPattern;
			}
		}

		public string InnerText { get; set;}
		//TODO:hacer el cambio de innerText a elements
		public List <RecursivePseudoLanguageElement> elements;


		public string StartString { get; set;}
		public string EndString { get; set;}

		public string CatchPattern { get; set;}

		public  ReplaceableString replace( ReplaceableString substitution )
		{
			ReplaceableString result = new ReplaceableString();

			int count = 0;
			//Mientras existan paréntesis
			//Encontrar los paréntesis mas adentro y aplicar una substitución, así hasta ya no encontrar
			//Si alguna substitucion abarca otras substituciones entonces esas substituciones irán dentro de esa substitución.
			while ( thereAreElements( substitution.text ) ) {

				substitution = replaceInnerElements ( substitution, count++ );

			}

			result = substitution;

			return result;
		}
		public  bool thereAreElements( string text )
		{
			bool result = false;

			Match match = findInner (text);
			result = match != null && match.Success;

			return result;
		}

		public virtual string substitutionText()
		{
			return " _"+ this.GetType().Name.Substring(0,1) + this.GetType().Name.Substring(this.GetType().Name.Length-1,1) +  "_ " ;
		}

		public  ReplaceableString replaceInnerElements	( ReplaceableString replaceableString, int nestedCount )
		{
			ReplaceableString result = new ReplaceableString();

			//StringWithSubstitution stringWithSubstitution = new StringWithSubstitution( substitution );

			Match match = findInner ( replaceableString.text);

//			int count = 0;
			int diferencia = 0;
			while ( match != null && match.Success ) {

				//string matchValue = match.Value;

				//string newText =  " _"+ this.GetType().Name + "_" + nestedCount + "_" + count++ + "_ " ; // Ejem: "_InnerParenthesis_0_0_" //TODO: cambiarlo por un random
				string newText =  substitutionText();//TODO:revisar, pareciera que entre mas corto fallla menos // Ejem: "_InnerParenthesis_0_0_" //TODO: cambiarlo por un random

				//stringWithSubstitution.Replace( match.Index + diferencia, match.Length, newText);
				//replaceableString.Replace( match.Index + diferencia, match.Length,newText);

//				//TODO buscar corchetes y llaves
				ReplaceableString tempSubstitution = new ReplaceableString (match.Value);

				if (!(this is Bracket) ) {
					tempSubstitution = new Bracket ().replace (tempSubstitution);
				}
				if (!(this is Parenthesis)	) {
					tempSubstitution = new Parenthesis ().replace (tempSubstitution);
				}
				if (!(this is Brace) ) {
					tempSubstitution = new Brace ().replace (tempSubstitution);
				}
//				if (!(this is Interface) ) {
//					tempSubstitution = new Interface ().replace (tempSubstitution);
//				}
//				if (!(this is Implementation) ) {
//					tempSubstitution = new Implementation ().replace (tempSubstitution);
//				}
//				if (!(this is Protocol) ) {
//					tempSubstitution = new Protocol ().replace (tempSubstitution);
//				}

				replaceableString.Replace (match.Index + diferencia, match.Length, newText,   tempSubstitution);

				diferencia -= match.Length;
				diferencia += newText.Length;

				match = match.NextMatch();
			}

			result = replaceableString;
			return result;
		}



		//TODO: pasar esto a un método estático.
		public Match findInner( string text )
		{
			Match result = null;

			string pattern = InnerPattern;
			RegexOptions options = RegexOptions.Singleline;
			Regex regex = new Regex (pattern,options);
			Match match = regex.Match (text);

			if (match.Success) {
				result =  match;
			}

			return result;
		}

		public override string ToString()
		{
			//return innerPattern;
			var text = "";

			foreach (RecursivePseudoLanguageElement r in elements) {
				text += r.ToString ();
			}

			return StartString+text+EndString;
		}
	}
}

