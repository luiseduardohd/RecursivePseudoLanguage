using System;

namespace CSharpVerbalExpressions
{
    public sealed class CommonRegex
    {
        private readonly String name;
        private readonly int value;
        public string Name
        {
			
            get
            {
                return name;
            }
        }
        public int Value
        {
            get
            {
                return value;
            }
        }

        public static readonly CommonRegex Url = new CommonRegex(1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[^-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[^-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w_]*)?\??(?:[-\+=&;%@.\w-_]*)#?‌​(?:[\w]*))?)");
        public static readonly CommonRegex Email = new CommonRegex(2, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
        
        private CommonRegex(int value, String name)
        {
            this.name = name;
            this.value = value;
        }
        static CommonRegex()
        {
        }
    }
}
