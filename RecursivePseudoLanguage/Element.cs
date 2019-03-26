using System;
using System.Reflection;
using System.Collections.Generic;

namespace RecursivePseudoLanguage
{
	public class Element
	{
		//public static Assembly Assembly; 
		public static List<Type> Types; 


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

//		public static List<object> getInheritedClasses(  object o )
//		{
//			getInheritedClasses(   o , Types ?? new List<Type>());
//		}

//		public static List<object> getInheritedClasses(  object o , List<Type> Types)
//		{
//			List<object> classes = new List<object>() ;
//
//			foreach (Type type in Types)
//			{
//				classes.Add(Activator.CreateInstance(type, new object[]{}));
//			}
//			classes.Sort();
//			return classes;
//		}
	}
}

