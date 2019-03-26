using System;

namespace RecursivePseudoLanguage
{
	public class Element
	{
//		public static string identifier = @"[a-záéíóúA-ZÁÉÍÓÚ_][a-záéíóúA-ZZÁÉÍÓÚ0-9_]*";
//		public static string space = @"[\s]+";
//		public static string maybeSpace = @"[\s]*";
//		public static string anyExceptSpace = @"[^\s]+";
//
//		public static string @catch(  string text )
//		{
//			return "(" + text + ")";
//		}

		public static string identifier = @"[a-záéíóúA-ZÁÉÍÓÚ_][a-záéíóúA-ZZÁÉÍÓÚ0-9_]*";
		public static string space = @"[\s]+";
		public static string maybeSpace = @"[\s]*";
		public static string anyExceptSpace = @"[^\s]+";

		public static string @catch(  string text )
		{
			return "(" + text + ")";
		}

		public static string anyExcept(params string[] values)
		{
			string joined = string.Join ("|", values);
			return "(?!"+joined+").";
		}

		public static string catchAnyExcept(params string[] values)
		{
			string joined = string.Join ("|", values);
			return "(?:(?!"+joined+").)*";
		}
		public Element ()
		{
		}
	}
}

