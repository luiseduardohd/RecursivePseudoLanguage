using System;

namespace BaseLanguage
{
	public class Element
	{
		public Element ()
		{
		}

		//TODO:tratar de sacar esto de esta función.
		public static string identifier = @"[a-záéíóúA-ZÁÉÍÓÚ_][a-záéíóúA-ZZÁÉÍÓÚ0-9_]*";
		public static string space = @"[\s]+";
		public static string maybeSpace = @"[\s]*";
		public static string anyExceptSpace = @"[^\s]+";

		public static string @catch(  string text )
		{
			return "(" + text + ")";
		}

		public static string anyExcept(string value)
		{
			return anyExcept(new string[] {value});
		}

		public static string anyExcept(params string[] values)
		{
			string joined = string.Join ("|", values);
			return "(?!"+joined+").";
		}

		public static string catchAnyExcept( string value)
		{
			return "(?:(?!"+value+").)*";
		}

		public static string catchAnyExcept(params string[] values)
		{
			string joined = string.Join ("|", values);
			return "(?:(?!"+joined+").)*";
		}

		//public abstract static Element parse (string inputText);
	}
//	public class IElement
//	{
//		static Element parse(string inputText)
//		{
//		}
//	}
}

