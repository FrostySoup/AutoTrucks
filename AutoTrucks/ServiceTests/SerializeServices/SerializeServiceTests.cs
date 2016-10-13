using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SerializeServices.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class SerializeServiceTests
    {
        SerializeService serializeService;

        [TestInitialize]
        public void SetInitialValues()
        {
            serializeService = new SerializeService();
        }

        [TestMethod()]
        public void SerializeDataSourceEmptyDataTest()
        {
            DataSource data = new DataSource();
            bool result = serializeService.SerializeDataSource(data);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void SerializeDataSourceNullDataTest()
        {
            DataSource data = null;
            bool result = serializeService.SerializeDataSource(data);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void SerializeDataSourceEmptyListTest()
        {
            ObservableCollection<DataSource> dataSourceListReceived = new ObservableCollection<DataSource>();
            bool result = serializeService.SerializeDataSourceList(dataSourceListReceived);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void SerializeDataSourceNullListTest()
        {
            ObservableCollection<DataSource> dataSourceListReceived = null;
            bool result = serializeService.SerializeDataSourceList(dataSourceListReceived);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void SerializeDataSourceListTest()
        {
            ObservableCollection<DataSource> dataSourceListReceived = new ObservableCollection<DataSource>();
            dataSourceListReceived.Add(new DataSource()
            {
                UserName = "Babajaga"
            });
            bool result = serializeService.SerializeDataSourceList(dataSourceListReceived);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void ReturnDataSourceWhileFileExistTest()
        {
            ObservableCollection<DataSource> result = serializeService.ReturnDataSource();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ReturnDataSourceNoFileTest()
        {
            string path = "./SerializedData.bin";
            FileInfo myfileinf = new FileInfo(path);
            myfileinf.Delete();
            ObservableCollection<DataSource> result = serializeService.ReturnDataSource();
            Assert.IsNotNull(result);
        }
    }
}