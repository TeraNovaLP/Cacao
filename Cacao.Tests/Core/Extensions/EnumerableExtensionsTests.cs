using System.Collections.Generic;
using System.Linq;
using Cacao.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cacao.Tests.Core.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void RemoveDuplicatesBy()
        {
            var entries = new List<(int key, string value)>
            {
                (TestHelper.GetRandomInt(), TestHelper.GetRandomString()),
                (1, TestHelper.GetRandomString()),
                (TestHelper.GetRandomInt(), TestHelper.GetRandomString()),
                (1, TestHelper.GetRandomString())
            };

            var result = entries.RemoveDuplicatesBy(x => x.key).ToList();

            Assert.AreEqual(1, result.Count(x => x.key == 1));
        }
    }
}
