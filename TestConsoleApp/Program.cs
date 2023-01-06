using System;

namespace Coding.Exercise
{
    public class SingletonTester
    {
        /// <summary>
        /// Determines if an object created by factory method is a singleton or not
        /// </summary>
        /// <param name="func">Factory method</param>
        public static bool IsSingleton(Func<object> func)
        {
            // Get two objects and check for referential equality.
            // Reference equality means that the object variables that are compared refer to the same object. 
            // https://learn.microsoft.com/en-us/dotnet/api/system.object.equals?view=net-7.0

            var obj1 = func();
            var obj2 = func();

            return obj1.Equals(obj2);

            // Alternative:
            // return ReferenceEquals(obj1, obj2);
        }
    }
}