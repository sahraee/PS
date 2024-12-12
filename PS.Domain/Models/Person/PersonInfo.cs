using PS.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;


namespace PS.Domain.Models.Person
{
    public class PersonInfo :  BaseModel<int>
    {

        [MaxLength(150)]
        public string FirstName {  get; set; }

        [MaxLength(150)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string NationalCode { get; set; }
        public DateTime BirthDate {  get; set; }


    }
}
