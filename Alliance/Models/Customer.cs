using Alliance.Abstract;
using Alliance.Interfaces;
using Newtonsoft.Json;

namespace Alliance.Models
{
    public class Customer : Entity<Customer>, IEntity
    {
        #region Constructors
                                                                                   
        public Customer(string name, Address address) : base(name, address)
        {
            var split = name.Split(' ');
            FirstName = split[0];
            LastName = split[1];
        }

        [JsonConstructor]
        public Customer(string firstName, string lastName, Address address) : base(firstName + " " + lastName, address)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Properties

        public string FirstName { get; }

        public string LastName { get; }

        #endregion
    }
}