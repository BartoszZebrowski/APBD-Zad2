using Microsoft.AspNetCore.Mvc;
using WebApplication1.Animals.Dto;

namespace WebApplication1.Animals
{
    public class AnimalController : Controller
    {
        private AnimalRepository _animallRepository;

        public AnimalController(AnimalRepository animallRepository)
        {
            _animallRepository = animallRepository;
        }

        [HttpGet("api/animal/{orderBy}")]
        public ActionResult<List<Animal>> GetAnimals(string orderBy = "Name")
        {
            return _animallRepository.GetAllAnimals(orderBy);
        }

        [HttpPost("api/animal")]
        public IActionResult AddAnimal([FromBody] AnimalDto animal)
        {
            _animallRepository.AddAnimal(animal);
            return Ok();
        }

        [HttpPut("api/animal/{idAnimal}")]
        public IActionResult UpdateAnimal(int idAnimal, [FromBody] AnimalDto animal)
        {
            _animallRepository.UpdateAnimal(idAnimal, animal);
            return Ok();
        }

        [HttpDelete("api/animal/{idAnimal}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            _animallRepository.DeleteAnimal(idAnimal);
            return Ok();
        }
    }
}
