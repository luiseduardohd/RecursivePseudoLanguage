using System;

namespace BaseLanguage
{
	public class EvaluableExpression : BaseLanguage.Element, IEvaluableExpression
	{
		public EvaluableExpression ()
		{
		}

		public static IEvaluableExpression parse(string inputText)
		{
			IEvaluableExpression IEvaluableExpression = null;

			#pragma warning disable 168
			try{ IEvaluableExpression = Addition.parse( inputText );}catch(Exception e){}
			try{ IEvaluableExpression = Number.parse( inputText );}catch(Exception e){}
			#pragma warning restore 168

			return IEvaluableExpression;
		}
	}
	public interface IEvaluableExpression
	{
	}
}

