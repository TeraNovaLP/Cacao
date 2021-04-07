using System;

namespace Cacao.Tests
{
    public static class TestHelper
    {
        public static string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static int GetRandomInt()
        {
            return new Random().Next();
        }
    }
}
