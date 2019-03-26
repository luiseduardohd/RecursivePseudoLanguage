using System;

namespace ObjectiveC
{
	public class EvaluableExpression : BaseLanguage.EvaluableExpression , IEvaluableExpression
	{
		public EvaluableExpression ()
		{
		}

		//		public new static Unit parse2(string inputText)
		//		{
		//			//Revisar como se puede hacer para encontrar todos los tipos derivados
		//			//TODO:revisar que se este parseando bien y salgan bien las excepciones, si no despues se parsea como otro tipo
		//			#pragma warning disable 168
		//			try{return Unit.convert(Number.parse( inputText ));}catch(Exception e){}
		//			#pragma warning restore 168
		//			return null;
		//		}

		public new static IEvaluableExpression parse(string inputText)
		{
			IEvaluableExpression IEvaluableExpression = null;

			//IEvaluableExpression = EvaluableExpression.create ( BaseLanguage.EvaluableExpression.parse (inputText));//??

			#pragma warning disable 168
			try{ 
				IEvaluableExpression = Addition.parse( inputText );
			}catch(Exception e){}
			try{ IEvaluableExpression = Number.parse( inputText );}catch(Exception e){}
			#pragma warning restore 168

			return IEvaluableExpression;
		}
	}
	public interface IEvaluableExpression : BaseLanguage.IEvaluableExpression
	{
	}
}

