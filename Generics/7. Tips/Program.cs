using System;
using System.Collections.Generic;

namespace Tips
{
    public enum Steps
    {
        Step1,
        Step2,
        Step3
    }

    public static class StringExtensions 
    {
        public static TEnum ParseEnum<TEnum>(this string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }

    public class Item2<T>
    {
        public Item2()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount { get; set; }
    }

    public class Item
    {
        
    }

    public class Item<T> : Item
    {
        public T Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // GenericsEnums();
            // UsingBaseTypes();
            GenericsAndStatics();
        }

        private static void GenericsEnums()
        {
            var input = "Step1";
            var value = input.ParseEnum<Steps>();

            Console.WriteLine(value);
        }

        private static void UsingBaseTypes()
        {
            var list = new List<Item>();
            list.Add(new Item<int> { Value = 1 });
            list.Add(new Item<double>{ Value = 2.0 });

            foreach (var item in list)
            {
                Console.WriteLine($"Value: {item}");
            }   
        }

        private static void GenericsAndStatics()
        {
            /* var a = new Item();
            var b = new Item();
            var c = new Item();
            Console.WriteLine(Item.InstanceCount); */

            var d = new Item2<int>();
            var f = new Item2<int>();
            var g = new Item2<string>();
            Console.WriteLine(Item2<int>.InstanceCount);
            Console.WriteLine(Item2<string>.InstanceCount);
        }
    }
}
