using System;

namespace CSharp
{
	public class Addition : BaseLanguage.Addition, IAddition
	{

		public Addition( )
		{
			this.left = "0";
			this.right = "0";
		}
		public Addition( BaseLanguage.IAddition Addition)
		{
			this.left = Addition.left;
			this.right = Addition.right;
		}
		public new static IAddition parse(string inputText)
		{
			IAddition Addition = new Addition();

			//Number.value = inputText;

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

