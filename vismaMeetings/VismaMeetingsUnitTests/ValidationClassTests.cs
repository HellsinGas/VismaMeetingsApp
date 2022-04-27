using Xunit;
using vismaMeetings.Repository;
using System.Globalization;
using System;

namespace VismaMeetingsUnitTests
{
    public class ValidationClassTests
    {
        [Fact]
        public void CheckStringEqualityTest()
        {
            bool expected = true;
            ValidationClass validationClass = new ValidationClass();

            bool actual = validationClass.StringEqualityValidation("Test", "test");
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CheckStringContainsTest()
        {
            ValidationClass validationClass = new ValidationClass();
            bool expected = true;
            bool actual = validationClass.CheckIfStringContains("I did something", "someth");
            Assert.Equal(expected, actual);
        }
      /*  [Fact]
        public void EndDateInputTest()
        {
            ValidationClass validationClass = new ValidationClass();
            DateTime expected = DateTime.MinValue.AddMinutes(60);
            bool actual = validationClass.CheckIfStringContains("I did something", "someth");
            Assert.Equal(expected, actual);
        }*/

    }
}