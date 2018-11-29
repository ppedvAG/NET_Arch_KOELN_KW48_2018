using Microsoft.QualityTools.Testing.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Calc_Sum_3_and_4_Result_7()
        {
            var calc = new Calc();

            var result = calc.Sum(3, 4);

            Assert.Equal(7, result);
        }

        [Fact]
        public void Calc_Sum_MAX_and_1_ThrowsOverflowException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));

        }

        [Theory]
        [InlineData(4, 7, 11)]
        [InlineData(0, 0, 0)]
        [InlineData(-6, -2, -8)]
        public void Calc_Sum(int a, int b, int soll)
        {
            var calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.Equal(soll, result);
        }

        [Fact]
        public void Calc_IsWeekend()
        {
            var calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 11, 26); //Mo
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 11, 27); //Di
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 11, 28); //Mi
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 11, 29); //Do
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 11, 30); //Fr
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 12, 1); //Sa
                Assert.True(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2018, 12, 2); //So
                Assert.True(calc.IsWeekend());
            }
        }
    }
}
