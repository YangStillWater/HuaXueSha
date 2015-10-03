using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangPeng.Extensions
{
    public static class ListExtension
    {
        public static TSource TakeRandom<TSource>(this List<TSource> list)
        {
            int index = new Random().Next(list.Count);
            TSource item = list[index];
            list.Remove(item);
            return item;
        }
        public static List<TSource> TakeRandom<TSource>(this List<TSource> list, int count)
        {
            List<TSource> items = new List<TSource>(count);
            for (int i = 0; i < count; i++)
            {
                items.Add(list.TakeRandom());
            }
            return items;
        }

        public static TSource Random<TSource>(this List<TSource> list)
        {
            int index = new Random().Next(list.Count);
            return list[index];
        }
    }
}
