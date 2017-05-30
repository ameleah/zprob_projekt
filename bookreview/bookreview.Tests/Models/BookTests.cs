using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookreview.Models.BaseModels;
using bookreview.Models;

namespace bookreview.Tests.Models
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void AddBookWithoutAuthor()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Book("name",null,DateTime.Now,"desc")));
        }

        [Test]
        public void AddBookWithoutName()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Book(null, new Author(), DateTime.Now, "desc")));
        }

        [Test]
        public void ShortDescpriptionTest()
        {
            string test = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Book book = new Book("name", new Author(), DateTime.Now, test);
            Assert.LessOrEqual(book.ShortText().Count(), 53);
        }

    }
}
