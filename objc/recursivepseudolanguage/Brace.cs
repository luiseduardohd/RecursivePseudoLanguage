using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SystemX;
using String = SystemX.String;

namespace RecursivePseudoLanguage
{
	public class Brace: InnerElement
	{
		public Brace ()
		{
			innerPattern =@"(\{[^\{\}]*\})";
		}
	}
}

