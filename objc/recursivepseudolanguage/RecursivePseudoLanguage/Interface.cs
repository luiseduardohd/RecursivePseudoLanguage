using System;

namespace RecursivePseudoLanguage
{
	public class Interface: InnerElement
	{
		public Interface ()
		{
			innerPattern =@"(@interface(?:(?!@interface|@end).)*@end)";
		}
	}
}

