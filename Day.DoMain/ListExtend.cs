using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace Wss.DoMain
{
    public static class ListExtend
    {

        public static string GetDisPlayName<TSource>(this TSource source ,string name)
        {
            var detail = source.GetType().GetMember("Name").SingleOrDefault();
            var type = source.GetType().GetMember(name).SingleOrDefault();
            var attribute=(DisplayNameAttribute)type.GetCustomAttributes(false).FirstOrDefault();
            return attribute.DisplayName;
        }


        //public static IEnumerable<TSource> OrderByFormWss<TSource, Tkey>(this IEnumerable<TSource> source,Func<TSource, Tkey> func)
        //{
        //    IEnumerable<TSource> result = new List<TSource>();
        //    foreach (var item in source)
        //    {
        //        var age = func(item);
        //    }
        //}


    }
}
