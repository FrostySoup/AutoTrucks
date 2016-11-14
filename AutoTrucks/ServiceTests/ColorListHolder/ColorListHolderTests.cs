using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace Service.ColorListHolder.Tests
{
    [TestClass()]
    public class ColorListHolderTests
    {

        ColorListHolder colorListHolder;

        [TestInitialize]
        public void SetInitialValues()
        {
            colorListHolder = new ColorListHolder();
        }

        [TestMethod()]
        public void GetColorByRefIdNullTest()
        {
            string value = null;
            var result = colorListHolder.GetColorByReferenceId(value);
            Assert.AreEqual(new ColorItem(Colors.White, "White"), result);
        }

        [TestMethod()]
        public void GetColorByRefIdShortTest()
        {
            string value = "Short";
            var result = colorListHolder.GetColorByReferenceId(value);
            Assert.AreEqual(new ColorItem(Colors.White, "White"), result);
        }

        [TestMethod()]
        public void GetColorByRefIdInvalidTest()
        {
            string value = "aaaaaaaaaaaaaaaaaaaaaa";
            var result = colorListHolder.GetColorByReferenceId(value);
            Assert.AreEqual(new ColorItem(Colors.White, "White"), result);
        }

        [TestMethod()]
        public void GetColorByRefIdValidTest()
        {
            string value = "testa05";
            var result = colorListHolder.GetColorByReferenceId(value);
            Assert.AreEqual(new ColorItem(Colors.FloralWhite, "FloralWhite"), result);
        }

        [TestMethod()]
        public void SetReferenceIdByColorTest()
        {
            Color value = Colors.AntiqueWhite;
            var result = colorListHolder.SetReferenceByColor(value);
            Assert.AreEqual("" + result[5] + result[6], "01");
        }

        [TestMethod()]
        public void SetReferenceIdByColorVioletTest()
        {
            Color value = Colors.Violet;
            var result = colorListHolder.SetReferenceByColor(value);
            Assert.AreEqual("" + result[5] + result[6], "16");
        }

        [TestMethod()]
        public void DefaultGetsTest()
        {
            var rez = colorListHolder.GetColors();
        }
    }
}