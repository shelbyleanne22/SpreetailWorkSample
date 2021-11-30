using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreetailWorkSample.Interfaces;
using SpreetailWorkSample.Models;
using SpreetailWorkSample.Services;
using System.Collections.Generic;

namespace SpreetailWorkSampleUnitTests
{
    [TestClass]
    public class MultiValueDictionaryServiceTests
    {
        private MultiValueDictionaryService _multiValueDictionaryService;

        public MultiValueDictionaryServiceTests()
        {
            _multiValueDictionaryService = new MultiValueDictionaryService();
        }

        [TestMethod]
        public void KeysCommand_ReturnsAllKeys()
        {
            HashSet<string> expectedResult = new() { "Key1" };
            _multiValueDictionaryService.AddMember("Key1", "Value1");

            var actualResult = _multiValueDictionaryService.GetAllKeys();

            Assert.IsTrue(expectedResult.SetEquals(actualResult));
        }

        [TestMethod]
        public void MembersCommand_ReturnsAllMembers()
        {
            List<string> expectedResult = new() { "Value1", "Value2", "Value3" };
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.GetAllMembers();

            Assert.AreEqual(expectedResult.ToString(),actualResult.ToString());
        }

        [TestMethod]
        public void AddCommand_AddsMember()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.MemberExists("Key1", "Value2");

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void AddCommand_DuplicateNotAdded()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.GetAllMembersOfKey("Key1");

            Assert.IsTrue(actualResult.Count == 1);
        }

        [TestMethod]
        public void RemoveCommand_RemovesMember()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.RemoveMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.MemberExists("Key1", "Value2");

            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void RemoveAllCommand_RemovesAllMember()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.RemoveAllMembers("Key1");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.KeyExists("Key1");

            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void ClearCommand_RemovesAllKeysAndMembers()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.Clear();

            var actualResult = _multiValueDictionaryService.KeyExists("Key1");

            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void KeyExists_ReturnsTrueForExistingKey()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.KeyExists("Key2");

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void MemberExists_ReturnsTrueForExistinMember()
        {
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.MemberExists("Key1", "Value2");

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void AllMembers_ReturnsAllMembers()
        {
            HashSet<string> expectedResult = new() { "Value1", "Value2", "Value2" };
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.GetAllMembers();

            Assert.IsTrue(expectedResult.SetEquals(actualResult));
        }

        [TestMethod]
        public void AllItems_ReturnsAllItems()
        {
            HashSet<KeyValuePair<string,string>> expectedResult = new() { new KeyValuePair<string,string>("Key1", "Value1"), new KeyValuePair<string, string>("Key1","Value2"), new KeyValuePair<string, string>("Key2", "Value2") };
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.GetAllItems();

            Assert.IsTrue(expectedResult.SetEquals(actualResult));
        }

        [TestMethod]
        public void CountKeys_ReturnsNumberOfKeys()
        {
            int expectedResult = 2;
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.CountKeys();

            Assert.AreEqual(expectedResult,actualResult);
        }

        [TestMethod]
        public void CountMembers_ReturnsNumberOfMembersOfKey()
        {
            int expectedResult = 2;
            _multiValueDictionaryService.AddMember("Key1", "Value1");
            _multiValueDictionaryService.AddMember("Key1", "Value2");
            _multiValueDictionaryService.AddMember("Key2", "Value2");

            var actualResult = _multiValueDictionaryService.CountMembers("Key1");

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
