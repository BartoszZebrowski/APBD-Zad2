using System.Data.SqlClient;

namespace WebApplication1.Animals
{
    public class AnimallRepository
    {
        private readonly IConfiguration _configuration;

        public AnimallRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Animal> GetAllAnimals()
        {
            using var sqlConnection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnetion"]);

            sqlConnection.Open();

            var sqlCommand = new SqlCommand("SELECT * FROM Animals");
            sqlCommand.Connection = sqlConnection;
            var response = sqlCommand.ExecuteReader();

            var animals = new List<Animal>();

            while (response.Read())
            {
                var animal = new Animal
                {
                    Id = (int)response["IdAnimal"],
                    Name = response["Name"].ToString(),
                    Description = response["Description"].ToString(),
                    Category = response["Category"].ToString(),
                    Area = response["Area"].ToString()
                };

                animals.Add(animal);
            }
            
            return animals;
        }


    }
}
