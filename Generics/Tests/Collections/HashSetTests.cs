using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CollectionsIT.HashSet.Tests
{
    [TestClass]
    public class HashSetTests
    {
        [TestMethod]
        public void Intersect_Sets()
        {
            var set1 = new HashSet<int>() { 1, 2, 3 };
            var set2 = new HashSet<int>() { 2, 3, 4 };

            set1.IntersectWith(set2); // Deja solo los numero que estan en ambos conjuntos

            Assert.IsTrue(set1.SetEquals(new [] { 2, 3}));
        }

        [TestMethod]
        public void Union_Sets()
        {
            var set1 = new HashSet<int>() { 1, 2, 3 };
            var set2 = new HashSet<int>() { 2, 3, 4 };

            set1.UnionWith(set2); // Une los dos conjuntos sin repetir elementos

            Assert.IsTrue(set1.SetEquals(new [] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void SymectricExcepWith_Sets()
        {
            var set1 = new HashSet<int>() { 1, 2, 3 };
            var set2 = new HashSet<int>() { 2, 3, 4 };

            set1.SymmetricExceptWith(set2); // Solo los numeros que estan en set1 y set2, pero no en ambos

            Assert.IsTrue(set1.SetEquals(new [] { 1, 4 }));
        }
    }
}