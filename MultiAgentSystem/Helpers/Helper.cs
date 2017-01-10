using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MultiAgentSystem.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Allow to test if a properties exist in a dynamic variable
        /// </summary>
        /// <param name="settings">Dynamic variable</param>
        /// <param name="name">Name of properties</param>
        /// <returns>Exist or Not</returns>
        public static bool IsPropertyExist(dynamic settings, string name)
        {
            try
            {
                var x = settings.name;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// To copu integraly a Dictionnary and its structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="dictinary"></param>
        /// <returns></returns>
        public static Dictionary<T, Y> HardCopyDictionary<T, Y>(Dictionary<T, Y> dictinary)
        {
            return JsonConvert.DeserializeObject<Dictionary<T, Y>>(JsonConvert.SerializeObject(dictinary));
        }

        /// <summary>
        /// To copy integraly any object and its structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Copy<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// To shuffle a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// To populate an array with a value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="value"></param>
        public static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        /// <summary>
        /// To populate an 2D array with a value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="value"></param>
        public static void Populate<T>(T[,] array, T value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }
    }
}
