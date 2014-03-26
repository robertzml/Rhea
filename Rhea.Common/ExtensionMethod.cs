using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Common
{
    public static class ExtensionMethod
    {
        /// <summary>
        /// 显示枚举DisplayName属性
        /// </summary>
        /// <param name="value">枚举对象</param>
        /// <returns></returns>
        public static string DisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs == null || attrs.Length == 0)
                return value.ToString();

            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }
}
