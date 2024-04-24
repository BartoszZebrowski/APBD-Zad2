using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplication1.Animals.Dto;
using WebApplication1.Exceptions;

namespace WebApplication1.Animals
{
    public class AnimalRepository
    {
        private readonly IConfiguration _configuration;

        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Animal> GetAllAnimals(string orderBy)
        {
            var column = Enum.Parse(typeof(AnimalColumn), orderBy);
            
            using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnetion"]);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Animal ORDER BY " +  column + " ASC");

            command.Connection = connection;

            var response = command.ExecuteReader();

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

        internal void AddAnimal(AnimalDto animal)
        {
            if (!Validate(animal))
                throw new ValidationException("Wrong animal data");

            SqlConnection connection;
            SqlCommand command;

            using (connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnetion"]))
            {
                connection.Open();

                var sql = "INSERT INTO Animal ( Name, Description, Category, Area) " +
                          "VALUES ( @Name, @Description, @Category, @Area); ";

                using (command = new SqlCommand(sql, connection))

                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", (object)animal.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                command.ExecuteScalar();
            }
        }
        internal void UpdateAnimal(int animalId, AnimalDto animal)
        {
            if (!Validate(animal))
                throw new ValidationException("Wrong animal data");

            SqlConnection connection;
            SqlCommand command;

            using (connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnetion"]))
            {
                connection.Open();

                var sql = "UPDATE Animal " +
                          "SET Name = @Name, " +
                          "    Description = @Description, " +
                          "    Category = @Category, " +
                          "    Area = @Area " +
                          "WHERE IdAnimal = @IdAnimal";

                using (command = new SqlCommand(sql, connection))

                command.Parameters.AddWithValue("@IdAnimal", animalId);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", (object)animal.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                    throw new InvalidOperationException($"Animal with ID {animalId} not found.");
            }
        }

        internal void DeleteAnimal(int idAnimal)
        {
            SqlConnection connection;
            SqlCommand command;

            using (connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnetion"]))
            {
                connection.Open();

                using (command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @IdAnimal", connection))

                    command.Parameters.AddWithValue("@IdAnimal", idAnimal);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                    throw new InvalidOperationException($"Animal with ID {idAnimal} not found.");
            }
        }

        public bool Validate(AnimalDto animal)
        {
            return !string.IsNullOrEmpty(animal.Name) &&
                !string.IsNullOrEmpty(animal.Description) &&
                !string.IsNullOrEmpty(animal.Area);
        }
    }
}
