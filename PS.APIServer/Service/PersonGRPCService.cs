using Grpc.Core;
using PS.Application.Interfaces;
using PS.Domain.Models.Person;
using PS.APIServer;
using Google.Protobuf.WellKnownTypes;

namespace PS.APIServer.Service
{
    public class PersonAPIService : Person.PersonBase
    {

        private readonly IPersonService _PersonService;

        public PersonAPIService(IPersonService PersonService)
        {
            _PersonService = PersonService;

        }
        public override async Task<AddPersonResponse> Add(AddPersonRequest request, ServerCallContext context)
        {
            AddPersonResponse response = new AddPersonResponse();
            response.Result = 0;

            try
            {
                PersonInfo Person = new PersonInfo()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate.ToDateTime(),
                    NationalCode = request.NationalCode,

                };

                var resultSave = await _PersonService.AddPersonAsync(Person);
                if (resultSave.IsSucceed)
                    response.Result = resultSave.Data.Id;
                else response.Message = resultSave.Message;
                return (response);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return (response);

            }
        }

        public override async Task<GetPersonResponse> Get(GetPersonRequest request, ServerCallContext context)
        {
            GetPersonResponse response = new GetPersonResponse();

            try
            {


                var resultSave = await _PersonService.GetPersonAsync(request.PersonId);
                if (resultSave.IsSucceed)
                {
                    response.PersonId = resultSave.Data.Id;
                    response.FirstName = resultSave.Data.FirstName;
                    response.LastName = resultSave.Data.LastName;
                    response.NationalCode = resultSave.Data.NationalCode;
                    response.BirthDate = resultSave.Data.BirthDate.ToUniversalTime().ToTimestamp();
                }
                else response.Message = resultSave.Message;
                return (response);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return (response);

            }
            //
            //return base.Add(request, context);
        }

        public override async Task<UpdatePersonResponse> Update(UpdatePersonRequest request, ServerCallContext context)
        {
            UpdatePersonResponse response = new UpdatePersonResponse();
            response.Result = false;

            try
            {
                PersonInfo Person = new PersonInfo()
                {
                    Id = request.PersonId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate.ToDateTime(),
                    NationalCode = request.NationalCode,

                };

                var resultSave = await _PersonService.EditPersonAsync(Person);
                if (resultSave.IsSucceed)
                    response.Result = resultSave.IsSucceed;
                else response.Message = resultSave.Message;
                return (response);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return (response);

            }
        }


        public override async Task<DeletePersonResponse> Delete(DeletePersonRequest request, ServerCallContext context)
        {
            DeletePersonResponse response = new DeletePersonResponse();
            response.Result = false;

            try
            { 

                var resultSave = await _PersonService.DeletePersonAsync(request.PersonId);
                if (resultSave.IsSucceed)
                    response.Result = resultSave.IsSucceed;
                else response.Message = resultSave.Message;
                return (response);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return (response);

            }
        }


    }
}
