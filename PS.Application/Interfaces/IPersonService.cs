using PS.Application.Result;
using PS.Domain.Models.Person;


namespace PS.Application.Interfaces
{
    public interface IPersonService
    {
        Task<ResultSet<PersonInfo>> AddPersonAsync(PersonInfo Person);
        Task<ResultSet<PersonInfo>> EditPersonAsync(PersonInfo Person);
        Task<ResultSet> DeletePersonAsync(int PersonId);
        Task<ResultSet<PersonInfo>> GetPersonAsync(int PersonId);     

    }
}
