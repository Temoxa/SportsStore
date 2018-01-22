using System;
using Xunit;

namespace SportsStore.Tests
{
    public class Test
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2*2);
        }

        [Theory]
        [InlineData(3, 3)]
        [InlineData(5, 4)]
        [InlineData(6, 6)]
        public void Test2(int value1, int value2)
        {
            Assert.True(value1 == value2);
        }
    }
}
