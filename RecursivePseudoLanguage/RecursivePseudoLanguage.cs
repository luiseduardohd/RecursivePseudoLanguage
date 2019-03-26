using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using SystemX;
using String = SystemX.String;

namespace RecursivePseudoLanguage
{
	// Esta clase solo deberá contener el texto parseado con parentesis corchetes y llaves, pero sin parseo de ningun otro tipo.
	//TODO:Cambiar de nombre la clase, este no va a ser un parser, solamente va a hacer substituciones de texto
	public class RecursivePseudoLanguage
	{
		public List <RecursivePseudoLanguageElement> elements;
		public RecursivePseudoLanguage ( )
		{
		}
		//TODO: este no debe regresar un ReplaceableString sino un objeto de si mismo ya parseado.
		//TODO: buscar diferentes formas de hacer este parseo.
		public static ReplaceableString ToReplaceableString(string text)
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
		//TODO: esta muy revuelto el parseo, separar un poco la funcionalidad de convertir de texto a ReplaceableString_
		//y la de parsear ya desde texto o desde ReplaceableString.
		//Buscar una forma de meterle cadenas de inicio y final dinámicas, para no tener que hacer clases 
		//y hacerlo más general para otros lenguajes
		//TODO:Agregarte objectiveCString, CString, Interface, implementation, protocol, etc, 
		//y tratar de sacar esto de la clase.
		public static RecursivePseudoLanguage Parse(string text)
		{
			RecursivePseudoLanguage result = new RecursivePseudoLanguage();

			ReplaceableString substitution = new ReplaceableString (text);

			substitution = new Bracket().replace ( substitution );

			substitution = new Parenthesis().replace ( substitution );

			substitution = new Brace().replace ( substitution );

			result = ToReplaceableString (substitution);

			return result;
		}

		//Obtener ya un objeto parseado con una estructura recursiva.
		public static RecursivePseudoLanguage ToReplaceableString(ReplaceableString ReplaceableString)
		{
			RecursivePseudoLanguage result = new RecursivePseudoLanguage();

			//List <String> substituciones = ReplaceableString.getAllSubstitutions (0,ReplaceableString.sustitutionLength);
			List <String> substituciones = ReplaceableString.substitutions;
			List <object> orderedSubstitutions = new List<object>();
			var text = "";

			//int currentTextPosition = ((ReplaceableString)substituciones[0]).parentIndex;
			int currentTextPosition = 0;
			//Me debe regresar las substituciones con el texto, separados en una lista.
			//Esto tal vez lo debería hacer la clase replaceableSubstitutions.
			foreach (String substitution  in substituciones) {

				//Console.
				Debug.WriteLine(""+substitution.text);
//				try
//				{
				//TODO:revisar por que lo dejé así, si en verdad si se ocupa.
				ReplaceableString replaceableString = (ReplaceableString) substitution;
				text = ReplaceableString.text.Substring ( currentTextPosition, replaceableString.parentIndex - currentTextPosition );
				orderedSubstitutions.Add( text );
				currentTextPosition = replaceableString.parentIndex + replaceableString.sustitutionLength;
//				}catch(Exception e)
//				{
//				}
				orderedSubstitutions.Add(substitution);
			}
			//Agrego lo Que halla quedado al final.
			text = ReplaceableString.text.Substring (currentTextPosition, ReplaceableString.text.Length -  currentTextPosition);
			orderedSubstitutions.Add(text);
			result.elements = ToReplaceableString( orderedSubstitutions);
			return result;
		}

		public static List <RecursivePseudoLanguageElement> ToReplaceableString(List <object> orderedSubstitutions)
		{
			List <RecursivePseudoLanguageElement> result = new List<RecursivePseudoLanguageElement> ();

			foreach (object substitution  in orderedSubstitutions) {
				if (substitution is string) {
					result.Add (new RecursivePseudoLanguageElement((string)substitution));
				}
				//aqui tengo que crear un objeto de cada tipo.
				try{
					var o = Brace.Parse(substitution.ToString());
					result.Add (new RecursivePseudoLanguageElement(o));
				}catch(Exception e){
				}
				try{
					var o = Bracket.Parse(substitution.ToString());
					result.Add (new RecursivePseudoLanguageElement(o));
				}catch(Exception e){
				}
				try{
					var o = Parenthesis.Parse(substitution.ToString());
					result.Add (new RecursivePseudoLanguageElement(o));
				}catch(Exception e){
				}
					
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

			return text;
		}

	}
}

