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
		}
	}
}

