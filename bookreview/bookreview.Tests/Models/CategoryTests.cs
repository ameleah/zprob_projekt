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
    public class CategoryTests
    {
        [Test]
        public void AddCategoryWithoutName()
        {
            Assert.Throws<Exception>(new TestDelegate(() => new Category(null)));            
        }
    }
}
