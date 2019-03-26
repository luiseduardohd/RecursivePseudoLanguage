using System;

namespace ObjectiveC
{
	public class Number : BaseLanguage.Number , INumber
	{
		public Number ()
		{
		}
		public Number( BaseLanguage.Number Number)
		{
			this.value = Number.value;
		}
//		public override string ToString()
//		{
//			return "" + value;
//		}

		public new static INumber parse(string inputText)
		{
			INumber Number = new Number();

			//TODO:cambiarlo por una mas general
			Number.value = "" +  Int32.Parse(inputText);

			return Number;
		}
	}
	public interface INumber : BaseLanguage.INumber , IEvaluableExpression
	{
	}
}

