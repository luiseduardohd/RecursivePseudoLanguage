using System;
using System.Collections.Generic;

namespace ParserSubstituciones
{
	public class Parser
	{
		public Parser ()
		{
		}

//		public static Unit parseWithSubstitutions( string text)
//		{
//			return  Unit.parse (text);
//		}
		public class RegularExpressions
		{
			//public static string identifier = @"[a-zA-Z_][a-zA-Z0-9_]*";
			public static string identifier = @"[a-záéíóúA-ZÁÉÍÓÚ_][a-záéíóúA-ZZÁÉÍÓÚ0-9_]*";
			public static string space = @"[\s]+";
			public static string maybeSpace = @"[\s]*";
			public static string anyExceptSpace = @"[^\s]+";

			public static string @catch(  string text )
			{
				return "(" + text + ")";
			}

//			public static string any(  string text )
//			{
//				return  text + "+";
//			}
//
//			public static string except(  string text )
//			{
//				return  "[^"+text+"]";
//			}

			public static string anyExcept(params string[] values)
			{
				string joined = string.Join ("|", values);
				return "(?!"+joined+").";
			}
			//(?:(?!@implementation|@end).)

			public static string catchAnyExcept(params string[] values)
			{
				string joined = string.Join ("|", values);
				return "(?:(?!"+joined+").)*";
			}

			//public string functionCall = @"([a-zA-Z_][a-zA-Z0-9_]*)[\s]+([a-zA-Z_][a-zA-Z0-9_]*)[\s]*;";
			public static string functionCall = @catch( identifier ) + space + @catch( identifier ) + maybeSpace+ ";";
			//public static string @interface = @"@interface[\s]+([^\s]+)[\s]+@end";
			public static string @interface = "@interface"+ catchAnyExcept("@interface","@end") +"@end";
			//public static string implementation = @"@implementation[\s]+([^\s]+)[\s]+@end";
			public static string implementation = "@implementation"+  catchAnyExcept("@implementation","@end") +"@end";
			//public static string functionImplementation = @"([a-zA-Z_][a-zA-Z0-9_]*)[\s]+([a-zA-Z_][a-zA-Z0-9_]*)[\s]+([a-zA-Z_][a-zA-Z0-9_]*)[\s]+([a-zA-Z_][a-zA-Z0-9_]*)";
			public static string functionImplementation = @catch( identifier ) + space + @catch( identifier ) + space + @catch( identifier ) + space + @catch( identifier );

			public static List<string> sentencesPatterns = new List<string> (){
				functionCall,
				@interface,
				implementation,
				functionImplementation
			};
		}
	}
}

