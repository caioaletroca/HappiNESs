using System.Threading.Tasks;

namespace HappiNESs
{
    /// <summary>
    /// Handles the read/writing to the application server
    /// </summary>
    public interface IServerManager
    {
        #region Methods

        /// <summary>
        /// Creates a new entry on Server
        /// </summary>
        /// <param name="parameter"></param>
        void Create(object parameter);

        /// <summary>
        /// Changes a current entry on Server
        /// </summary>
        /// <param name="parameter"></param>
        void Change(object parameter);

        /// <summary>
        /// Deletes an entry on Server
        /// </summary>
        /// <param name="parameter"></param>
        void Delete(object parameter);

        /// <summary>
        /// Get an entry with ID
        /// </summary>
        /// <param name="ID"></param>
        object GetByID(int ID);

        /// <summary>
        /// Implements a type of search on Server
        /// </summary>
        /// <param name="parameter"></param>
        object Search(object parameter);

        /// <summary>
        /// Return all data from a server
        /// </summary>
        /// <returns></returns>
        object All();

        #endregion
    }
}
