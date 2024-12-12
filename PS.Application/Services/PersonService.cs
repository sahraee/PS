using PS.Application.Interfaces;
using PS.Application.Result;
using PS.Domain;
using PS.Domain.Interfaces;
using PS.Domain.Models.Person;

namespace PS.Application.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _PersonRepository;

        public PersonService(IPersonRepository PersonRepository)
        {
            this._PersonRepository = PersonRepository;
        }

        
        public async Task<ResultSet<PersonInfo>> AddPersonAsync(PersonInfo Person)
        {
            if (_PersonRepository.IsPersonExists(Person.NationalCode, null))
                return new ResultSet<PersonInfo>()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.DuplicateData
                };

            if (!_PersonRepository.AddPerson(Person))
                return new ResultSet<PersonInfo>() { IsSucceed = false, Message = Constants.Messages.PersonNotInserted, Data = Person };


            try { await _PersonRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<PersonInfo>() { IsSucceed = false, Message = e.Message }; }

            return new ResultSet<PersonInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Person
            };
        }

        public async Task<ResultSet<PersonInfo>> EditPersonAsync(PersonInfo Person)
        {
            if (_PersonRepository.IsPersonExists(Person.NationalCode, Person.Id))
                return new ResultSet<PersonInfo>()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.DuplicateData,
                };
            if (_PersonRepository.GetPersonAsync(Person.Id).Result == null)
                return new ResultSet<PersonInfo>() { IsSucceed = false, Message = Constants.Messages.PersonNotFound, Data = Person };


            if (!_PersonRepository.EditPerson(Person))
                return new ResultSet<PersonInfo>() { IsSucceed = false, Message = Constants.Messages.PersonNotEdited, Data=Person};

            try
            {
                await _PersonRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new ResultSet<PersonInfo>() { IsSucceed = false, Message = e.Message,Data=null };
            }
            return new ResultSet<PersonInfo>() { IsSucceed = true, Message = string.Empty,Data=Person };
        }

        public async Task<ResultSet> DeletePersonAsync(int PersonId)
        {


            PersonInfo Person= await _PersonRepository.GetPersonAsync(PersonId);
            if(Person==null)
            {
                return new ResultSet() { IsSucceed = false, Message=Constants.Messages.PersonNotFound };
            }
            if (!_PersonRepository.DeletePerson(PersonId))
                return new ResultSet() { IsSucceed = false, Message = Constants.Messages.PersonNotDeleted };

            try
            {
                await _PersonRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = Constants.Messages.PersonNotDeleted  };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<PersonInfo>> GetPersonAsync(int PersonId)
        {
            PersonInfo Person = await _PersonRepository.GetPersonAsync(PersonId);

            if (Person == null)
                return new ResultSet<PersonInfo>()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.PersonNotFound,
                };

            return new ResultSet<PersonInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Person
            };

        }
    }
}
