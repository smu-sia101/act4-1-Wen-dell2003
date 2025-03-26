using Microsoft.AspNetCore.Mvc;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/pets")] 
    public class PetController : ControllerBase
    {

        private string[] dogNames = new string[] { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
        private string[] catNames = new string[] { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
        private string[] birdNames = new string[] { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

        [HttpGet("generate")]
        public IActionResult Get()
        {
        
            string[] animalTypes = new string[] { "dog", "cat", "bird" };
            Random rnd = new Random();
            string animalType = animalTypes[rnd.Next(animalTypes.Length)];

          
            string[] selectedNames = animalType switch
            {
                "dog" => dogNames,
                "cat" => catNames,
                "bird" => birdNames,
                _ => throw new InvalidOperationException("Invalid animal type")
            };

            
            string randomName = selectedNames[rnd.Next(selectedNames.Length)];

          
            return Ok(new { name = randomName });
        }


        [HttpPost("add/{animalType}")]
        public IActionResult Post(string animalType, [FromBody] string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest(new { error = "The 'newName' field is required." });
            }

          
            switch (animalType.ToLower())
            {
                case "dog":
                    dogNames = dogNames.Concat(new string[] { newName }).ToArray();
                    break;
                case "cat":
                    catNames = catNames.Concat(new string[] { newName }).ToArray();
                    break;
                case "bird":
                    birdNames = birdNames.Concat(new string[] { newName }).ToArray();
                    break;
                default:
                    return BadRequest(new { error = "Invalid animal type. Allowed values: dog, cat, bird." });
            }

         
            return Ok(new
            {
                message = $"{animalType} name added successfully!",
                names = animalType.ToLower() switch
                {
                    "dog" => dogNames,
                    "cat" => catNames,
                    "bird" => birdNames,
                    _ => throw new InvalidOperationException("Invalid animal type")
                }
            });
        }

    }
}
