using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookreview.Models.BaseModels;
using bookreview.Models;
using NUnit.Framework;

namespace bookreview.Tests.Models
{
    [TestFixture]
    class RateTests
    {
        [Test]
        public void AddInvalidRate()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(new ApplicationUser(),false,0,new Object())));
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(new ApplicationUser(), false, 11, new Object())));
        }

        [Test]
        public void AddRateWithoutUser()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(null, false, 1, new Object())));
        }

        [Test]
        public void AddRateWithoutEntity()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(new ApplicationUser(), false, 1, null)));
        }

    }
}
