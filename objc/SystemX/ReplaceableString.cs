using System;
using System.Collections.Generic;

namespace SystemX
{
	public class ReplaceableString : String
	{
		//public string replaceText = "";
		//public string replaceText = "";
		public int parentIndex = 0;
		public int sustitutionLength = 0;

		public ReplaceableString ()
		{
		}
		public ReplaceableString ( String substitution  ):base(substitution.text)
		{
			ReplaceableString stringWithSubstitution  = substitution as ReplaceableString;
			if( stringWithSubstitution != null )
				this.substitutions = stringWithSubstitution.substitutions;
		}
		public ReplaceableString ( string text ):base(text)
		{
		}
		public ReplaceableString ( string text, int parentIndex  ):this( text, parentIndex, new List<String> () )
		{
			this.parentIndex = parentIndex;
		}
		public ReplaceableString ( string text, int parentIndex, List<String> substitutions ):base(text)
		{
			this.parentIndex = parentIndex;
			this.substitutions  = substitutions;
		}


		public ReplaceableString ( string text, int parentIndex, int sustitutionLength  ):this( text, parentIndex, new List<String> () )
		{
			this.parentIndex = parentIndex;
			this.sustitutionLength = sustitutionLength;
		}
		public ReplaceableString ( string text, int parentIndex, int sustitutionLength , List<String> substitutions ):base(text)
		{
			this.parentIndex = parentIndex;
			this.sustitutionLength = sustitutionLength;
			this.substitutions  = substitutions;
		}

		public List<String> substitutions =  new List<String>();


		public void Replace( int index, int length, string newText, ReplaceableString replaceableString )
		{

			if (index + length > text.Length) {
				throw new ArgumentException ();
			}
//			string substitutedText = text.Substring( index,length );
			text = text.Remove (index , length);
			text = text.Insert (index , newText);

			var diffLength = 0;

			diffLength -= length;
			diffLength += newText.Length;

			ReplaceableString newReplaceableString = new ReplaceableString ( replaceableString.text, index + replaceableString.parentIndex , newText.Length, replaceableString.substitutions   );//text?

			List<String> substitutionsInRange = getAllSubstitutions( index, length );
			//Le quito el index, ya que al tener nuevo padre el index cambia.
			//TODO revisar si hacerlo aqui o en otro lugar
			foreach( String s in substitutionsInRange )
			{
				substitutions.Remove(s);

				if( s is ReplaceableString )
				{
					ReplaceableString rs = s as ReplaceableString;
					//rs.parentIndex += - parentIndex - (index + replaceableString.parentIndex );//Para hacer de este el nuevo padre
//					rs.parentIndex -= parentIndex ;
					rs.parentIndex -= index ;
					newReplaceableString.substitutions.Add ( rs );
					//newReplaceableString.substitutions.Sort(
				}
			}
			substitutions.Add (newReplaceableString);

			//Mueve todos los demas que esten despues
			substitutions.Sort( delegate (String t1, String t2) 
				{ return ((ReplaceableString)t1).parentIndex.CompareTo(((ReplaceableString)t2).parentIndex); } 
			);
			foreach (String s in substitutions) {
				if( s is ReplaceableString )
				{
					ReplaceableString rs = s as ReplaceableString;
					if( index < rs.parentIndex  )
					{
						rs.parentIndex += diffLength;//Para manejar el desfase
					}
				}
			}
		}

		public void Replace( int index, int length, string newText )//Cuando muevo uno tengo que mover todos los demás que esten despues
		{
			if (index + length > text.Length) {
				throw new ArgumentException ();
			}
			string substitutedText = text.Substring( index,length );
			text = text.Remove (index , length);
			text = text.Insert (index , newText);

			var diffLength = 0;

			diffLength -= length;
			diffLength += newText.Length;

			//Si no hay substituciones anteriores
			if ( thereIsPreviousSubstitution (index, length) == false ) {
				substitutions.Add ( new ReplaceableString( substitutedText, index, newText.Length  ) );
			} 
			//Si hay substituciones anteriores
			else {
				List<String> substitutionsInRange = getAllSubstitutions( index, length );
				//Le quito el index, ya que al tener nuevo padre el index cambia.
				//TODO revisar si hacerlo aqui o en otro lugar
				foreach( String s in substitutionsInRange )
				{
					substitutions.Remove(s);
					if( s is ReplaceableString )
					{
						((ReplaceableString)s).parentIndex -= parentIndex;//Para hacer de este el nuevo padre
					}
				}
				substitutions.Add ( new ReplaceableString ( substitutedText, index,newText.Length, substitutionsInRange ) );
			}

			//Mueve todos los demas que esten despues
			substitutions.Sort( delegate (String t1, String t2) 
				{ return ((ReplaceableString)t1).parentIndex.CompareTo(((ReplaceableString)t2).parentIndex); } 
			);
			foreach (String s in substitutions) {
				if( s is ReplaceableString )
				{
					ReplaceableString rs = s as ReplaceableString;
					if( index < rs.parentIndex  )
					{
						rs.parentIndex += diffLength;//Para manejar el desfase
					}
				}
			}
			//substitutions.AddRange( substitution.substitutions );
			//text = text;
			//return this;
		}
		public ReplaceableString subString( int index, int length)
		{
			ReplaceableString result = null;
			List<String> substitutions = getAllSubstitutions(  index,  length);

			foreach (String s in substitutions) {
				ReplaceableString rs = s as ReplaceableString;
				if ( rs != null ) {
					rs.parentIndex -= index;
				}
			}
			result = new ReplaceableString (text.Substring (index, length), 0, substitutions);
			return result;
		}
		public List<String> getAllSubstitutions( int index, int length)
		{
			List<String> result = new List<String>();
			foreach( String s in substitutions )
			{

				if(  s is ReplaceableString && intersect( (ReplaceableString) s, index, length ) )
				{
					result.Add( s );
				}
			}
			return result;
		}
		public bool thereIsPreviousSubstitution( int index, int length)
		{
			foreach( String s in substitutions )
			{
				if( s is ReplaceableString && intersect( (ReplaceableString) s, index, length ) )
				{
					return true;
				}
			}
			return false;
		}

		public bool intersect( ReplaceableString s, int index, int length)//TODO revisar bien esta funcion y los limites, sobretodo cuando estan al final 1+ 2 = index 3
		{
			if (  index <=  s.parentIndex  &&  s.parentIndex <  ( index + length )  )
				return true;

			return false;
		}

//		public bool intersect( int x1ini, int x1end,int x2ini, int x2end)
//		{
//			if (  x2ini <=  x1ini  &&  x1ini <=  x2end  )
//				return true;
//
//			if (  x2ini <=  x1end  &&  x1end <=  x2end  )
//				return true;
//
//			if (  x1ini <=  x2ini  &&  x2ini <=  x1end  )
//				return true;
//
//			if (  x1ini <=  x2end  &&  x2end <=  x1end  )
//				return true;
//
//			return false;
//		}

		//TODO
		public void Add( ReplaceableString replaceableString )
		{

		}
		//TODO
		public void Remove( ReplaceableString replaceableString )
		{

		}



		public override string ToString()
		{
			string result = "";

			result = text;

			int diffLength = 0;
			substitutions.Sort( delegate (String t1, String t2) 
				{ return ((ReplaceableString)t1).parentIndex.CompareTo(((ReplaceableString)t2).parentIndex); } 
			);
			foreach( var s in substitutions )
			{
				if( s is ReplaceableString )
				{
					var substitucion = s as ReplaceableString;
					int index = 0;
					int count = 0;
					string value = "";

//					index = substitucion.parentIndex + diffLength;
//					count = substitucion.sustitutionLength;
					index = Math.Min( substitucion.parentIndex + diffLength, result.Length);
					count = ( index + substitucion.sustitutionLength <= result.Length) ?   substitucion.sustitutionLength:result.Length-index;
//					index = substitucion.parentIndex;
//					count = substitucion.sustitutionLength;



					result = result.Remove ( index, count );//TODO: esta fallando en esta parte

					value = substitucion.ToString ();
					result = result.Insert ( index, value );

					diffLength -= count;
					diffLength += value.Length;

				}
			}

			return result;
		}
	}
}

