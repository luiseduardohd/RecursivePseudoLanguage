using System;

namespace SystemX
{
	//I dont like sealed classes
	public class String
	{
		public String ( )
		{
		}
		public String ( string text)
		{
			this.text = text;
		}
		public string text = "";
//		public int Length {
//			get{
//				return text.Length;
//			}
//		}
	}
}

