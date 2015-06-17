using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NunitTest
{
    [TestFixture]
    public class Class1
    {       
        [Test]
        public void PositiveTest()
        {
            int a = 100;
            int b = 100;
            Assert.AreEqual(a, b);

        }

        [Test]
        public void NegativeTest()
        {
            if (true)
                Assert.Fail("this is a failure");
        }

        [Test,ExpectedException(typeof(NotSupportedException))]
        public void ExceptionExpectedTest()
        {
            throw new NotSupportedException();
        }

        [Test,Ignore]
        public void NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}
