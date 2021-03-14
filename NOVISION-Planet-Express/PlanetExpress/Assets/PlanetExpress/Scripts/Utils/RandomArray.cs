using System;

namespace PlanetExpress.Scripts.Utils
{
    public static class RandomArray
    {
        private static Random _randomUtil = new Random();

        public static T GetRandom<T>(this T[] array)
        {
            int start2 = _randomUtil.Next(0, array.Length);
            return array[start2];
        }
        
        
        
    }
}