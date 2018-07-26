using Alliance.Models;

namespace Alliance.Interfaces
{
    public interface IEntity
    {
        #region Properties

        Address Address { get; }

        string Id { get; }

        string Name { get; }

        #endregion

        #region Public Methods

        void Delete();

        void Save();

        #endregion
    }
}