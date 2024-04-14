using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animals
{
    
    public class AnimalController : Controller
    {
        private AnimallRepository _animallRepository;
        public AnimalController(AnimallRepository animallRepository)
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
