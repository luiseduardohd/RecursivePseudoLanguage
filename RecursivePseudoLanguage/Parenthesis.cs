using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SystemX;
using String = SystemX.String;

namespace RecursivePseudoLanguage
{
	public class Parenthesis : InnerElement
	{
		public Parenthesis ()
		{
			//TODO:revisar donde afecta el cambio de * por + en la espresión
			innerPattern =@"(\([^\(\)]*\))";//RegularExpressions.llamadaAFunción

			StartString	=	"(";
			EndString	=	")";
		}

		public static Parenthesis Parse( string inputText)
		{
			Parenthesis result = new Parenthesis ();

			//Pasarlo a expressiones regulares
			if (
				inputText.Substring (
					0,
					result.StartString.Length
				).Equals (result.StartString)
				&&
				inputText.Substring (
					inputText.Length - (  result.EndString.Length),
					result.EndString.Length
				).Equals (result.EndString)
			) {
				result.InnerText = inputText.Substring (result.StartString.Length, inputText.Length - (  result.StartString.Length + result.EndString.Length));
				RecursivePseudoLanguage r = RecursivePseudoLanguage.Parse(result.InnerText);
				result.elements = r.elements;
			} else {
				throw new ArgumentException ();
			}

			return result;
		}
	}
}

