using System;

namespace RecursivePseudoLanguage
{
	public class RecursivePseudoLanguageElement
	{
		public Brace Brace { get; set; }

		public Bracket Bracket { get; set; }

		public Parenthesis Parenthesis { get; set; }
		//		public Interface Interface { get; set;}
		//		public Implementation Implementation { get; set;}
		//		public Protocol Protocol { get; set;}

		public string Text { get; set; }

		public RecursivePseudoLanguageElement (string Text)
		{
			this.Text = Text;
		}

		public RecursivePseudoLanguageElement (Brace Brace)
		{
			this.Brace = Brace;
		}

		public RecursivePseudoLanguageElement (Bracket Bracket)
		{
			this.Bracket = Bracket;
		}

		public RecursivePseudoLanguageElement (Parenthesis Parenthesis)
		{
			this.Parenthesis = Parenthesis;
		}

		public object getValue ()
		{
			if (Brace != null)
				return Brace;
			if (Bracket != null)
				return Bracket;
			if (Parenthesis != null)
				return Parenthesis;
			if (Text != null)
				return Text;
			
			return null;
		}

		public override string ToString()
		{
			return this.getValue ().ToString();
		}
	}
}

