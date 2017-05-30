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
    public class AuthorTests
    {       
        [Test]
        public void AddAuthorWithoutName()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Author(null, "lastname", DateTime.Now, "bio")));
            Assert.Throws<Exception>(new TestDelegate(() => new Author("firstname", null, DateTime.Now, "bio")));
        }

        [Test]
        public void InvalidBirthDate()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Author("firstname", "lastname", DateTime.Now.AddDays(1), "bio")));
        }
    }
}
