using Alliance.Abstract;
using Alliance.Interfaces;
using Newtonsoft.Json;

namespace Alliance.Models
{
    public class Company : Entity<Company>, IEntity
    {
        #region Constructors
             
        [JsonConstructor]
        public Company(string name, Address address) : base(name, address)
        {
        }

        #endregion
    }
}