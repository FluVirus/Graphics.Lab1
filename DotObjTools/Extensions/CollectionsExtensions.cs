using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotObjTools.Extensions;

internal static class CollectionsExtensions
{
    public static T[][] ToTwoDimensionalArray<T>(this List<List<T>> list)
    {
        T[][] outer = new T[list.Count][];
        for(int i = 0; i < list.Count; i++)
        {
            outer[i] = list[i].ToArray();
        }

        return outer;
    }
}
