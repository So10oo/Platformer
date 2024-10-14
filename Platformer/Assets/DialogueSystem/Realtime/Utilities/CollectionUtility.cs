using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace DialogSystem.Editor
{
    public static class CollectionUtility
    {
        public static void AddItem<T, K>(this /*Serializable*/IDictionary<T, List<K>> serializableDictionary, T key, K value)
        {
            if (serializableDictionary.ContainsKey(key))
            {
                serializableDictionary[key].Add(value);

                return;
            }

            serializableDictionary.Add(key, new List<K>() { value });
        }
    }
}