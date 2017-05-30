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
    public class ReviewTests
    {
        [Test]
        public void AddReviewWithoutUser()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Review(null, false, new Object(), "test")));
        }

        [Test]
        public void AddReviewWithoutText()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Review(new ApplicationUser(), false, new Object(), null)));
        }
    }
}
