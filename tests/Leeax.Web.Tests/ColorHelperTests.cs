using System.Drawing;
using Xunit;

namespace Leeax.Web.Tests
{
    public class ColorHelperTests
    {
        private const string HEX_STR = "#324CA8";
        private const string HEX_STR_WITHOUT_PREFIX = "324CA8";

        private const string RBG_STR = "rgb(50,76,168)";
        private const string RBGA_STR = "rgba(50,76,168,0.54)";
        private const string RBG_STR_WITH_SPACES = "rgb(50 ,76 ,168 )";

        private const byte HEX_STR_R = 50;
        private const byte HEX_STR_G = 76;
        private const byte HEX_STR_B = 168;
        private const byte HEX_STR_A = 138;

        #region Helper for tests

        private Color GetColor()
        {
            return Color.FromArgb(255, HEX_STR_R, HEX_STR_G, HEX_STR_B);
        }

        private void AssertParsedColor(Color color)
        {
            Assert.True(color.R == HEX_STR_R);
            Assert.True(color.G == HEX_STR_G);
            Assert.True(color.B == HEX_STR_B);
            Assert.True(color.A == 255);
        }

        #endregion

        [Fact]
        public void ParseHex()
        {
            var color = ColorHelper.FromHexStr(HEX_STR);

            AssertParsedColor(color);
        }

        [Fact]
        public void ParseHexWithoutPrefix()
        {
            var color = ColorHelper.FromHexStr(HEX_STR_WITHOUT_PREFIX);

            AssertParsedColor(color);
        }

        [Fact]
        public void ParseRgb()
        {
            var color = ColorHelper.FromRgbStr(RBG_STR);

            AssertParsedColor(color);
        }

        [Fact]
        public void ParseRgba()
        {
            var color = ColorHelper.FromRgbaStr(RBGA_STR);

            Assert.True(color.R == HEX_STR_R);
            Assert.True(color.G == HEX_STR_G);
            Assert.True(color.B == HEX_STR_B);
            Assert.True(color.A == HEX_STR_A);
        }

        [Fact]
        public void ParseRgbWithSpaces()
        {
            var color = ColorHelper.FromRgbStr(RBG_STR_WITH_SPACES);

            AssertParsedColor(color);
        }

        [Fact]
        public void ConvertToRgbStr()
        {
            var color = GetColor();
            var str = color.ToRgbStr();

            Assert.True(str == RBG_STR);
        }

        [Fact]
        public void ConvertToRgbaStr()
        {
            var color = Color.FromArgb(HEX_STR_A, HEX_STR_R, HEX_STR_G, HEX_STR_B);
            var str = color.ToRgbaStr();

            Assert.True(str == RBGA_STR);
        }

        [Fact]
        public void ConvertToHexStr()
        {
            var color = GetColor();
            var str = color.ToHexStr();

            Assert.True(str == HEX_STR);
        }

        [Fact]
        public void LighDarkColorTest()
        {
            Assert.True(ColorHelper.IsLightColor(Color.White));
            Assert.True(ColorHelper.IsDarkColor(Color.Black));
        }
    }
}
