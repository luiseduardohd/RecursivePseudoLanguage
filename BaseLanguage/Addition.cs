using System;

namespace BaseLanguage

{
	public class Addition : EvaluableExpression
	{
		public string left { get; set;}
		public string right { get; set;}

		public Addition ()
		{
		}
	}
	public interface IAddition : IEvaluableExpression
	{
		string left { get; set;}
		string right { get; set;}
	}
}

