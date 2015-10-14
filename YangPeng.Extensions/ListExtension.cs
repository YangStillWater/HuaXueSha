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

        public static void MoveTo<TSource>(this List<TSource> listFrom, List<TSource> listTo, TSource item)
        {
            if (listFrom.Remove(item))
            {
                listTo.Add(item); 
            }
        }
        public static void MoveTo<TSource>(this List<TSource> listFrom, List<TSource> listTo, int indexFrom)
        {
            TSource item = listFrom[indexFrom];
            listFrom.MoveTo(listTo, item);
        }
        public static void MoveRangeTo<TSource>(this List<TSource> listFrom, List<TSource> listTo, List<TSource> items)
        {
            foreach (TSource i in items)
            {
                listFrom.MoveTo(listTo, i);
            }
        }
        public static void MoveRangeTo<TSource>(this List<TSource> listFrom, List<TSource> listTo, int[] indexesFrom)
        {
            var items = new List<TSource>(indexesFrom.Length);
            foreach (int i in indexesFrom)
            {
                items.Add(listFrom[i]);
            }
            listFrom.MoveRangeTo(listTo, items);
        }
        public static void MoveRangeTo<TSource>(this List<TSource> listFrom, List<TSource> listTo, int indexBegin,int count)
        {
            var items = listFrom.GetRange(indexBegin, count);
            listFrom.MoveRangeTo(listTo, items);
        }

    }
}
