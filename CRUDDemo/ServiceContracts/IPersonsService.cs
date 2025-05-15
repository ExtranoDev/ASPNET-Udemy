using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business login for manipulating Person entity
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns>Returns the same person details along with newly generated PersonID</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of PersonResponse type</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Returns a Person object based on the specified PersonID
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>Returns mateching Person object</returns>
        PersonResponse GetPersonByPersonID(Guid? personID);
    }
}
