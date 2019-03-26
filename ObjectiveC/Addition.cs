using System;
using System.Text.RegularExpressions;
using CSharpVerbalExpressions;

namespace ObjectiveC

{

	public class Addition : BaseLanguage.Addition, IAddition
	{
		//private static string pattern = catchAnyExcept(new string[]{"\\+"})  + "\\+" +  catchAnyExcept(new string[]{"\\+"}) ;
		//private static string pattern= "((?:(?!\\+).)*)\\+((?:(?!\\+).)*)";
		/*
		 * "
		  	(
		  		(?:
		  			(?!
		  				\\+
		  			)
		  			.
		  		)
				*
			)
			\\+
			(
				(?:
					(?!
						\\+
					)
					.
				)
				*
			)"
		 * */
		private static string pattern =	"((?:(?!\\+).)*)\\+((?:(?!\\+).)*)";
//			VerbalExpressions.DefaultExpression
//				.BeginCapture ()
//				.AnythingBut ("+")
//				.EndCapture ()
//				.Then ("+")
//				.BeginCapture ()
//				.AnythingBut ("+")
//				.EndCapture ()
//				.ToString ();
		
		public Addition( )
		{
			this.left = "0";
			this.right = "0";
			pattern =	CSharpVerbalExpressions.VerbalExpressions.DefaultExpression
							.AnythingBut ("+")//Groups [1]
							.Then ("+")//Groups [2]
							.AnythingBut ("+")//Groups [3]
							.ToString ();
		}
		public Addition( BaseLanguage.IAddition Addition):base()
		{
			this.left = Addition.left;
			this.right = Addition.right;
		}
		public new static IAddition parse(string inputText)
		{
			IAddition Addition = new Addition();

			//Number.value = inputText;

			//Match result = null;

			//string pattern = pattern;
			RegexOptions options = RegexOptions.Singleline;
			Regex regex = new Regex (pattern,options);
			Match match = regex.Match (inputText);


			if (match.Success) {
				//result =  match;
				Addition.left = match.Groups [1].Value.Trim();
				Addition.right = match.Groups [3].Value.Trim();
			} else {
				throw new ArgumentException ( "bad format" );
			}

			return Addition;
		}
		public override string ToString()
		{
			return "" + left + " + " + right;
		}
	}
	public interface IAddition : BaseLanguage.IAddition, IEvaluableExpression
	{
	}
}

