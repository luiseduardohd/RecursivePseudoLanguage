using System;

namespace ParserSubstituciones
{
	public class Sentence
	{
		public static Sentence parse(string text)
		{
			//Revisar como se puede hacer para encontrar todos los tipos derivados
			#pragma warning disable 168
			try{return FunctionCall.parse( text );}catch(Exception e){}
			try{return FunctionImplementation.parse( text );}catch(Exception e){}
			try{return Implementation.parse( text );}catch(Exception e){}
			try{return Interface.parse( text );}catch(Exception e){}
			#pragma warning restore 168
			return null;
		}
	}
}

