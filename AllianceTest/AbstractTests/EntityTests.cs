using Alliance.Abstract;
using Alliance.Interfaces;
using Alliance.Models;
using NUnit.Framework;

namespace AllianceTest.AbstractTests
{
    [TestFixture]
    public class EntityTests
    {
        #region Setup/Teardown

        [SetUp]
        public void TestSetup()
        {
            _addr = new Address("1 one street", "city", "state", "zipcode");
            _object = new EntityImpl("Bob", _addr);
        }

        #endregion

        private Address _addr;
        private EntityImpl _object;

        private class EntityImpl : Entity<EntityImpl>, IEntity
        {
            #region Constructors

            public EntityImpl(string name, Address address) : base(name, address)
            {
            }

            #endregion
        }

        [Test]
        public void ConstructorAndPropertiesTest()
        {
            Assert.IsNotNull(_object);
            Assert.AreEqual(_object.Address, _addr);
            Assert.IsNullOrEmpty(_object.Id);
            Assert.AreEqual(_object.Name, "Bob");
        }

        [Test]
        public void EqualsTest()
        {
            var obj = new EntityImpl("Bob", _addr);
            var obj1 = new EntityImpl("Bob1", _addr);

            Assert.IsTrue(_object.Equals(_object));
            Assert.IsTrue(_object.Equals((object)_object));
            Assert.IsTrue(_object.Equals(obj));
            Assert.IsTrue(_object.Equals((object)obj));

            Assert.IsFalse(_object.Equals((object)null));
            Assert.IsFalse(_object.Equals(null));
            Assert.IsFalse(_object.Equals(default(EntityImpl)));
            Assert.IsFalse(_object.Equals(obj1));
            Assert.IsFalse(_object.Equals((object)obj1));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var obj = new EntityImpl("Bob", _addr);
            var obj1 = new EntityImpl("Bob1", _addr);

            Assert.AreEqual(_object.GetHashCode(), _object.GetHashCode());
            Assert.AreEqual(_object.GetHashCode(), obj.GetHashCode());
            Assert.AreNotEqual(_object.GetHashCode(), obj1.GetHashCode());
        }

        [Test]
        public void SaveFindDeleteTest()
        {
            Assert.IsNullOrEmpty(_object.Id);
            _object.Save();
            Assert.IsNotNullOrEmpty(_object.Id);

            var saved = EntityImpl.Find(_object.Id);
            Assert.AreEqual(_object, saved);

            _object.Delete();
            Assert.IsNullOrEmpty(_object.Id);

            saved = EntityImpl.Find(_object.Id);
            Assert.AreEqual(default(EntityImpl), saved);
        }
    }
}