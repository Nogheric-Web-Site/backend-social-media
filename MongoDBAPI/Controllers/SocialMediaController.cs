using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

using MongoDBAPI.Models;
using MongoDBAPI.Repositories;

namespace MongoDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : Controller
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private ISocialMediaCollection db = new SocialMediaCollection();

        private IMongoCollection<SocialMedia> Collection;
        //private IResponseFormat res;

        ResponseModel resm = new ResponseModel();

        public SocialMediaController()
        {
            Collection = _repository.db.GetCollection<SocialMedia>("SocialMedia");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSocialMedia()
        {
            return Ok(await db.GetAllSocialMedia());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetSocialMediaDetails(string code)
        {
            await db.GetSocialMediaById(code);
            if (db.GetSocialMediaById(code).Result.code != "")
            {
                resm.Success = true;
                resm.Message = "Consulta Correcta.";
                resm.Body = new SocialMediaResponse()
                {
                    code = db.GetSocialMediaById(code).Result.code,
                    name = db.GetSocialMediaById(code).Result.name,
                    url = db.GetSocialMediaById(code).Result.url
                };
            }
            else
            {
                resm.Success = false;
                resm.Message = "No se encontraron resultados.";
                resm.Body = null;
            }
            return Ok(resm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia([FromBody] SocialMedia socialMedia)
        {
            if (socialMedia == null)
                return BadRequest();
            if(socialMedia.name == string.Empty)
            {
                ModelState.AddModelError("name", "El nombre no debe estar vacío");
            }
            var filter = Collection.Find(s => s.code == socialMedia.code).FirstOrDefault();
            if (filter == null)
            {
                await db.InsertSocialMedia(socialMedia);
                resm.Success = true;
                resm.Message = "Creado correctamente.";
                resm.Body = new SocialMediaResponse()
                {
                    code = socialMedia.code,
                    name = socialMedia.name,
                    url = socialMedia.url
                };
                return Ok(resm);
            }
            else
            {
                resm.Success = false;
                resm.Message = "El código ya existe";
                resm.Body = new SocialMediaResponse()
                {
                    code = socialMedia.code,
                    name = socialMedia.name,
                    url = socialMedia.url
                };
                return Ok(resm);
            }
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateSocialMedia([FromBody] SocialMedia socialMedia, string code)
        {
            if (socialMedia == null)
                return BadRequest();
            if (socialMedia.name == string.Empty)
            {
                ModelState.AddModelError("name", "El nombre no debe estar vacío");
            }

            socialMedia.code = code;
            await db.UpdateSocialMedia(socialMedia);

            return Created("Created", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSocialMedia(string code)
        {
            await db.DeleteSocialMedia(code);

            return NoContent(); 
        }
    }
}
