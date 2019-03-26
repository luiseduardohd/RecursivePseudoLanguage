using System;
using ObjectiveC;

namespace LanguageConverter
{
	public static class LanguageConverter
	{
		
		public static string ObjectiveCToCSharp(string inputCode)
		{
			string result = "";
			//ObjectiveC.Unit objectiveCUnit = ObjectiveC.Unit.parse2 (inputCode);
			//ObjectiveC.IEvaluableExpression ObjectiveCEvaluableExpression = ObjectiveC.EvaluableExpression.parse (inputCode);
			RecursivePseudoLanguage.RecursivePseudoLanguage recursivePseudoLanguage = null;
			recursivePseudoLanguage =RecursivePseudoLanguage.RecursivePseudoLanguage.Parse ( inputCode );
			//ObjectiveC.IEvaluableExpression ObjectiveCEvaluableExpression = ObjectiveC.EvaluableExpression.parse (inputCode);
			ObjectiveC.IEvaluableExpression ObjectiveCEvaluableExpression = RecursivePseudoLanguageToObjectiveC (recursivePseudoLanguage);

			//CSharp.Unit cSharpUnit = ObjectiveCToCSharp (EvaluableExpression);

			CSharp.IEvaluableExpression CSharpEvaluableExpression = ObjectiveCToCSharp (ObjectiveCEvaluableExpression);

			result = CSharpEvaluableExpression.ToString ();
			return result;
		}

		//TODO:aqui debe de ser de RecursivePseudoLanguageToBaseLanguage y despúes ya de RecursivePseudoLanguageToObjectiveC
		//No se debe de hacer así, pero por tiempo y facilidad así se hace.
		public static ObjectiveC.IEvaluableExpression RecursivePseudoLanguageToObjectiveC(RecursivePseudoLanguage.RecursivePseudoLanguage RecursivePseudoLanguage)
		{
			ObjectiveC.IEvaluableExpression result = null;

			if (RecursivePseudoLanguage.elements.Count == 1 && RecursivePseudoLanguage.elements[0].getValue() is string ) {
				result = EvaluableExpression.parse ((string)RecursivePseudoLanguage.elements[0].getValue());
			}

			return result;
		}
		//TODO:hacer este primero antes del de objectiveC
		//No, si tiene que ser a objective-C por que es así como está el codigo en texto.
		//ya después de eso se convienrte a base language para la conversion a otro lenguage.s
		public static BaseLanguage.IEvaluableExpression RecursivePseudoLanguageToBaseLanguage(RecursivePseudoLanguage.RecursivePseudoLanguage RecursivePseudoLanguage)
		{
			BaseLanguage.IEvaluableExpression result = null;

			if (RecursivePseudoLanguage.elements.Count == 1 && RecursivePseudoLanguage.elements[0].getValue() is string ) {
				result = EvaluableExpression.parse ((string)RecursivePseudoLanguage.elements[0].getValue());
			}

			return result;
		}

		public static CSharp.IEvaluableExpression ObjectiveCToCSharp(ObjectiveC.IEvaluableExpression objectiveExpression)
		{
			CSharp.IEvaluableExpression CSharpExpression = null;

			//Q&D: en teoría funcionará para la mayoría de las cosas.
			//CSharpUnit = new CSharp.Unit (objectiveCUnit);

			if (objectiveExpression is BaseLanguage.INumber) {

				CSharpExpression = new CSharp.Number ((BaseLanguage.INumber)objectiveExpression);
			}
			if (objectiveExpression is BaseLanguage.IAddition) {

				CSharpExpression = new CSharp.Addition ((BaseLanguage.IAddition)objectiveExpression);
			}

			return CSharpExpression;
		}

//		public static CSharp.Unit ObjectiveCToCSharp(ObjectiveC.Unit objectiveCUnit)
//		{
//			CSharp.Unit CSharpUnit = null;
//
//			//Q&D: en teoría funcionará para la mayoría de las cosas.
//			//CSharpUnit = new CSharp.Unit (objectiveCUnit);
//
//			if (objectiveCUnit is BaseLanguage.INumber) {
//				
//				CSharpUnit = new CSharp.Number (objectiveCUnit);
//			}
//
//			return CSharpUnit;
//		}
	}
}

