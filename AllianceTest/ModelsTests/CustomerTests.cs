using Alliance.Models;
using NUnit.Framework;

namespace AllianceTest.ModelsTests
{
    [TestFixture]
    public class CustomerTests
    {
        #region Setup/Teardown

        [SetUp]
        public void TestSetup()
        {
            _addr = new Address("1 one street", "city", "state", "zipcode");
            _object = new Customer("bob dobbs", _addr);
            _object1 = new Customer("bob", "dobbs", _addr);
        }

        #endregion

        private Address _addr;
        private Customer _object;
        private Customer _object1;

        [Test]
        public void ConstructorAndPropertiesTest()
        {
            Assert.IsNotNull(_object);
            Assert.AreEqual(_object.FirstName, "bob");
            Assert.AreEqual(_object.LastName, "dobbs");

            Assert.IsNotNull(_object1);
            Assert.AreEqual(_object1.FirstName, "bob");
            Assert.AreEqual(_object1.LastName, "dobbs");
        }
    }
}