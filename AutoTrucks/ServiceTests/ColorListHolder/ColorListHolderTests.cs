using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.ColorListHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void GetColorsTest()
        {
            
        }
    }
}