using System;

namespace Alliance.Models
{
    public class Address : IEquatable<Address>
    {
        #region Constructors

        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        #endregion

        #region Properties

        public string City { get; }

        public string State { get; }

        public string Street { get; }

        public string ZipCode { get; }

        #endregion

        #region Public Methods

        public bool Equals(Address other)
        {
            if (other == null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || string.Equals(City, other.City) && string.Equals(State, other.State) && string.Equals(Street, other.Street) && string.Equals(ZipCode, other.ZipCode);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Address;
            if (other == null)
            {
                return false;
            }

            return ReferenceEquals(this, obj) || Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = City != null ? City.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Street != null ? Street.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ZipCode != null ? ZipCode.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}