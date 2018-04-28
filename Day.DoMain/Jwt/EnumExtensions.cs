using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    internal static class EnumExtensions
    {


        public static string GetHeaderName(this HeaderName value) => GetDescription(value);



        private static string GetDescription(object value) => value.GetType().GetField(value.ToString()).GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();


        public static string GetPublicClaimName(this ClaimName value) => GetDescription(value);
            
     }
}
