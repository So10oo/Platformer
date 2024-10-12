using System;
using System.Collections.Generic;

public static class ListExtinctions
{
    public static List<T> Clone<T>(this List<T> list) where T : ICloneable
    {
        var returnList = new List<T>();
        foreach (var item in list)
            returnList.Add((T)item.Clone());
        return returnList;
    }
}
