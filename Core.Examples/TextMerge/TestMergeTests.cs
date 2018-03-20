using Core.TextMerge;
using Core.TextMerge.Interface;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Examples.TextMerge
{
    [TestFixture]
    public class TestMergeTests
    {

        private ITextMergeProvider _textMergeProvider;

        [SetUp]
        public void SetUp()
        {
            _textMergeProvider = new TextMergeProvider();
        }

        [Test]
        public void TestTextMerge()
        {
            // arrange
            var mergeFields = new Dictionary<string, string>();
            mergeFields.Add("TestField", "TestValue");
            mergeFields.Add("FirstName", "Simon");
            mergeFields.Add("LastName", "Andrews");
            mergeFields.Add("TestLink", "https://github.com/SimonAndrews");

            var mergeFieldWrapper = new KeyValuePair<char, char>('[', ']');

            var preMergeContent = 
@"
Test Merge Content...

Test Field is here >>> [TestField].
FirstName is here >>> [FirstName].
LastName is here >>> [LastName].

TestLink click here >>> <a href='[TestLink]'>Linky</a>
";

            // act
            var result = _textMergeProvider.MergeText(mergeFields, mergeFieldWrapper, preMergeContent);

            // assert
            var expectedResult =
@"
Test Merge Content...

Test Field is here >>> TestValue.
FirstName is here >>> Simon.
LastName is here >>> Andrews.

TestLink click here >>> <a href='https://github.com/SimonAndrews'>Linky</a>
";
            Assert.AreEqual(result, expectedResult);
        }
    }
}
