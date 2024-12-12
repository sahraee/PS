using PS.Domain.Interfaces;
using PS.Domain.Models.Person;
using PS.Persistence.Contexts;

namespace PS.Persistence.Repository
{
    public class PersonRepository : IPersonRepository
    {

        private GenericRepository<PersonInfo> _PersonRepository;

        public PersonRepository(PSDBContext ctx)
        {
            this._PersonRepository = new GenericRepository<PersonInfo>(ctx);
        }

       
         
        public bool AddPerson(PersonInfo Person)
        {
            try
            {
                _PersonRepository.Insert(Person);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditPerson(PersonInfo Person)
        {
            try
            {
                
                _PersonRepository.Update(Person);
            }
            catch(Exception ex)
            { 
                return false; 
            }
            return true;
        }

        public async Task<PersonInfo> GetPersonAsync(int PersonId)
        {
            return await _PersonRepository.GetByIdAsync(s=>s.Id==PersonId);
        
        }
 
         
        public void Save()
        {
            _PersonRepository.Save();
        }

        public async Task SaveAsync() =>
            await _PersonRepository.SaveAsync();
 
        public bool IsPersonExists(string NationalCode, int? id)
        {
            return (_PersonRepository.Get(b => b.NationalCode == NationalCode && (id==null || b.Id!=id)).Count() > 0 ? true : false);
        }

        

        public  bool DeletePerson(int PersonId)
        {
            var Person = GetPerson(PersonId);
            if (Person == null)
                return false;
            try
            {
                Person.IsRemoved = true;
                Person.RemoveDate = DateTime.Now;
                _PersonRepository.Update(Person);
            }
            catch
            {
                return false;
            }
            return true;
        }


        private PersonInfo GetPerson(int PersonId)
        {
            return _PersonRepository.GetById(b=>b.Id== PersonId );
        }
    }
}
