using PS.Domain.Models.Person;

namespace PS.Domain.Interfaces
{
    public interface IPersonRepository
    {
      
        bool IsPersonExists(string NationalCode,int? id);

        bool AddPerson(PersonInfo Person);
        bool EditPerson(PersonInfo Person);
        bool DeletePerson(int PersonId);

        Task<PersonInfo> GetPersonAsync(int PersonId);
      

        void Save();
        Task SaveAsync();
    }
}
