using System;

namespace CSharp
{
	public class Number : BaseLanguage.Number , IEvaluableExpression
	{
		public Number( BaseLanguage.INumber Number):base(Number)//
		{
			//this.value = Number.value;
		}
	}
}

