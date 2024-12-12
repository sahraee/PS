using PS.APIServer;
using Grpc.Net.Client;
using System;
using Google.Protobuf.WellKnownTypes;

namespace PS.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using var grpcChannel = GrpcChannel.ForAddress("https://localhost:7220/");

            var client = new PS.APIServer.Person.PersonClient(grpcChannel); ;



            //Add Person
            Console.WriteLine("Add Person");

            AddPersonRequest addPersonRequest = new AddPersonRequest()
            {
                FirstName = "ali",
                LastName = "alavi",
                BirthDate = DateTime.Now.AddYears(-33).ToUniversalTime().ToTimestamp(),
                NationalCode = "000000000000"

            };


            AddPersonResponse addPersonResponse = client.Add(addPersonRequest);

            Console.WriteLine(addPersonResponse.Result.ToString());
            Console.WriteLine(addPersonResponse.Message);


            //Get Person
            Console.WriteLine("\n Get Person");

            GetPersonRequest getPersonRequest = new GetPersonRequest() { PersonId = 2 };
            GetPersonResponse getPersonResponse = client.Get(getPersonRequest);

            Console.WriteLine(getPersonResponse.NationalCode.ToString());
            Console.WriteLine(getPersonResponse.Message);


            //Update Person
            Console.WriteLine("\n Update Person");

            UpdatePersonRequest updatePersonRequest = new UpdatePersonRequest()
            {
                PersonId = 2,
                FirstName = "Ali",
                LastName = "Alavi",
                NationalCode = "1234567890",
                BirthDate = DateTime.Now.AddYears(-35).ToUniversalTime().ToTimestamp()
            };
            UpdatePersonResponse updatePersonResponse = client.Update(updatePersonRequest);


            Console.WriteLine(updatePersonResponse.Result.ToString());
            Console.WriteLine(updatePersonResponse.Message);


            // Delete Person
            Console.WriteLine("\n Delete Person");
            DeletePersonRequest deletePersonRequest = new DeletePersonRequest() { PersonId = 1 };
            DeletePersonResponse deletePersonResponse = client.Delete(deletePersonRequest);
            Console.WriteLine(deletePersonResponse.Result.ToString());
            Console.WriteLine(deletePersonResponse.Message);




            Console.ReadKey();
        }
    }
}