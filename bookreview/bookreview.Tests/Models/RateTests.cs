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
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(new ApplicationUser(),false,0,new Book())));
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(new ApplicationUser(), false, 11, new Book())));
        }

        [Test]
        public void AddRateWithoutUser()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(null, false, 1, new Book())));
        }

        [Test]
        public void AddRateWithoutEntity()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Rate(new ApplicationUser(), false, 1, null)));
        }

        [Test]
        public void IsAverageRateValid()
        {
           Book testBook = new Book("test", new Author(), DateTime.Now, "desc");
            testBook.RateList.Add(new Rate(new ApplicationUser(), true, 10, testBook));
            testBook.RateList.Add(new Rate(new ApplicationUser(), true, 5, testBook));
            testBook.RateList.Add(new Rate(new ApplicationUser(), true, 2, testBook));
            testBook.RateList.Add(new Rate(new ApplicationUser(), true, 1, testBook));
            Assert.AreEqual(((10 + 5 + 2 + 1) / 4.0), testBook.GetAverageOfRates());
        }

        [Test]
        public void IsAverageRateValidForZero()
        {
            Book testBook = new Book("test", new Author(), DateTime.Now, "desc");
            Assert.AreEqual(0, testBook.GetAverageOfRates());
        }


    }
}
