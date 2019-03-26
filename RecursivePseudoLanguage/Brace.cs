using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SystemX;
//using CSharpVerbalExpressions;
using String = SystemX.String;

namespace RecursivePseudoLanguage
{
	public class Brace: InnerElement
	{
		public Brace ()
		{
			innerPattern =@"(\{[^\{\}]*\})";

			InnerText =@"";
			StartString	=	"{";
			EndString	=	"}";
			//VerbalExpressions d = VerbalExpressions.DefaultExpression;

			//No sirve muy bien verbal espression
			//TODO:Hacer un RegularExpressionBuilder
			//https://msdn.microsoft.com/en-us/library/bs2twtah(v=vs.110).aspx#noncapturing_group
//			CatchPattern	=	VerbalExpressions.DefaultExpression
//				.StartOfLine()
//				.Then(StartString)
//				.AnythingBut( "[^" + d.Sanitize(StartString) +d.Sanitize(EndString)+"]",false)
//				.Then(EndString);
			//Ejemplo:@"(@interface(?:(?!@interface|@end).)*@end)"
//			CatchPattern	=	
//			
//				@""+
//				d.Sanitize(StartString)+
//				@"((?!"+
//				d.Sanitize(StartString)+
//				@"|"+
//				d.Sanitize(EndString)+
//				@").)*"+
//				EndString+
//				@"";
		}
		//TODO:En lugar de innerText, debo meterlos en elements (checarlo)
		public static Brace Parse( string inputText)
		{
			Brace result = new Brace ();

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

