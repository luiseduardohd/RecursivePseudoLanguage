using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using SystemX;
using String = SystemX.String;

namespace RecursivePseudoLanguage
{
	// Esta clase solo deberá contener el texto parseado con parentesis corchetes y llaves, pero sin parseo de ningun otro tipo.
	//TODO:Cambiar de nombre la clase, este no va a ser un parser, solamente va a hacer substituciones de texto
	public class RecursivePseudoLanguage
	{
		public RecursivePseudoLanguage ( )
		{
		}

		public static ReplaceableString Parse(string text)
		{
			ReplaceableString result = new ReplaceableString();

			ReplaceableString substitution = new ReplaceableString (text);

			substitution = new Bracket().replace ( substitution );

			substitution = new Parenthesis().replace ( substitution );

			substitution = new Brace().replace ( substitution );

//			substitution = new Protocol().replace ( substitution );
//
//			substitution = new Implementation().replace ( substitution );
//
//			substitution = new Interface().replace ( substitution );

			result = substitution;
			return result;
		}

	}
}

