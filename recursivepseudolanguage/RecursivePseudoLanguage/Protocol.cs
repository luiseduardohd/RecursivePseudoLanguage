using System;

namespace RecursivePseudoLanguage
{
	public class Protocol: InnerElement
	{
		public Protocol ()
		{
			innerPattern =@"(@protocol(?:(?!@implementation|@end).)*@end)";
		}
	}
}

