using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class IconAttribute : Attribute
    {
        public string Icon { get; private set; }

        public IconAttribute(string icon)
        {
            Icon = icon;
        }
    }
}
