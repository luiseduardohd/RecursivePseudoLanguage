using System;
using BaseLanguage;

namespace BaseLanguage
{
	public class Number  : Unit, INumber
	{
		public Number (  )
		{
			this.value = "0";
		}
//		public Number ( string inputText )
//		{
//			//Checar que sea un numero
//			this.value = inputText;
//		}
		public Number( BaseLanguage.INumber Number)
		{
			this.value = Number.value;
		}

//		public new static Unit parse(string inputText)
//		{
//			Unit Unit = new Number(inputText);
//
//			return Unit;
//		}

		public new static INumber parse(string inputText)
		{
			INumber Number = new Number();
			Number.value = inputText;
			return Number;
		}
		
		public string value { get; set;}

		public override string ToString()
		{
			return "" + value;
		}
	}
	public interface INumber :   IEvaluableExpression
	{
		string value { get; set;}
	}
}

