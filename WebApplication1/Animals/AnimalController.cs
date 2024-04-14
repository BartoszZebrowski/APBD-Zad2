using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animals
{
    
    public class AnimalController : Controller
    {
        private AnimalRepository _animallRepository;
        public AnimalController(AnimalRepository animallRepository)
        {
            _animallRepository = animallRepository;
        }

        [HttpGet("/animals")]
        public List<Animal> GetAnimals()
        {
            return _animallRepository.GetAllAnimals();
        }
    }
}
