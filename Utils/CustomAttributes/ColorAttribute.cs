using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public ColorAttribute(string color)
        {
            Color = color;
        }
    }
}
