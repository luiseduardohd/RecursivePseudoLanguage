using System;
using RecursivePseudoLanguage;

namespace ObjectiveC
{
	//Cambio el nombre pro statement para que este mas acorde a la teoria
	//http://en.wikipedia.org/wiki/Statement_(computer_science)
	public class Statement : Element
	{
		public static Statement parse(string text)
		{
			//Revisar como se puede hacer para encontrar todos los tipos derivados
			//TODO:revisar que se este parseando bien y salgan bien las excepciones, si no despues se parsea como otro tipo
			#pragma warning disable 168
			try{return Interface.parse( text );}catch(Exception e){}
			try{return Implementation.parse( text );}catch(Exception e){}
			try{return FunctionImplementation.parse( text );}catch(Exception e){}
			try{return FunctionCall.parse( text );}catch(Exception e){}
			#pragma warning restore 168
			return null;
		}
	}
}

