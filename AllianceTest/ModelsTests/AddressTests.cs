using Alliance.Models;
using NUnit.Framework;

namespace AllianceTest.ModelsTests
{
    [TestFixture]
    internal class AddressTests
    {
        #region Setup/Teardown

        [SetUp]
        public void TestSetup()
        {
            _object = new Address("1 one street", "city", "state", "zipcode");
        }

        #endregion

        private Address _object;

        [Test]
        public void ConstructorAndPropertiesTest()
        {
            Assert.IsNotNull(_object);
            Assert.AreEqual(_object.Street, "1 one street");
            Assert.AreEqual(_object.City, "city");
            Assert.AreEqual(_object.State, "state");
            Assert.AreEqual(_object.ZipCode, "zipcode");
        }

        [Test]
        public void EqualsTest()
        {
            var obj = new Address("1 one street", "city", "state", "zipcode");
            var obj1 = new Address("1 one street", "city", "california", "zipcode");

            Assert.IsTrue(_object.Equals(_object));
            Assert.IsTrue(_object.Equals((object)_object));
            Assert.IsTrue(_object.Equals(obj));
            Assert.IsTrue(_object.Equals((object)obj));

            Assert.IsFalse(_object.Equals((object)null));
            Assert.IsFalse(_object.Equals(null));
            Assert.IsFalse(_object.Equals(default(Address)));
            Assert.IsFalse(_object.Equals(obj1));
            Assert.IsFalse(_object.Equals((object)obj1));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var obj = new Address("1 one street", "city", "state", "zipcode");
            var obj1 = new Address("1 one street", "city", "california", "zipcode");

            Assert.AreEqual(_object.GetHashCode(), _object.GetHashCode());
            Assert.AreEqual(_object.GetHashCode(), obj.GetHashCode());
            Assert.AreNotEqual(_object.GetHashCode(), obj1.GetHashCode());
        }
    }
}