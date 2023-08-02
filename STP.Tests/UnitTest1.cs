using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectSTP.Models;
using System;

namespace STP.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ManagerGetItems_lengthNot0returned()
        {
            //arrange

            int expected = 0;

            //act
            SQLManagerRepository sQLManagerRepository = new SQLManagerRepository();
            int actual = sQLManagerRepository.GetItemsList().Length;

            //assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ClientGetItems_lengthNot0returned()
        {
            //arrange

            int expected = 0;

            //act
            SQLClientRepository sQLClientRepository = new SQLClientRepository();
            int actual = sQLClientRepository.GetItemsList().Length;

            //assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ProductGetItems_lengthNot0returned()
        {
            //arrange

            int expected = 0;

            //act
            SQLProductRepository sQLProductRepository = new SQLProductRepository();
            int actual = sQLProductRepository.GetItemsList().Length;

            //assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ClientByClientByManagerGetItems_lengthNot0returned()
        {
            //arrange

            int expected = 0;

            //act
            SQLClientByManagerRepository sQLClientByManagerRepository = new SQLClientByManagerRepository();
            int actual = sQLClientByManagerRepository.GetItemsList().Length;

            //assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ClientByStatusGetItems_lengthNot0returned()
        {
            //arrange

            int expected = 0;

            //act
            SQLClientByStatusRepository sQLClientByStatusRepository = new SQLClientByStatusRepository();
            int actual = sQLClientByStatusRepository.GetItemsList().Length;

            //assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void ProductByClientGetItems_lengthNot0returned()
        {
            //arrange

            int expected = 0;

            //act
            SQLProductByClientRepository sQLProductByClientRepository = new SQLProductByClientRepository();
            int actual = sQLProductByClientRepository.GetItemsList().Length;

            //assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}
