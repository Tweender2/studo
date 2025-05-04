using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using System.Reflection;
using System.Xml.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly SupaBaseContext _supabaseContext;

        public WeatherForecastController(Supabase.Client supabaseClient, SupaBaseContext supabaseContext)
        {
            _supabaseClient = supabaseClient;
            _supabaseContext = supabaseContext;
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public async Task<string> GetAllUsers()
        {
                var result = await _supabaseContext.GetUsers(_supabaseClient);
                return JsonConvert.SerializeObject(result, Formatting.Indented);

        }

        [HttpPost("InsertCity", Name = "InsertCity")]
        public async Task<ActionResult> InsertCity([FromBody] CityData cityData)
        {
            try
            {
                if(string.IsNullOrEmpty(cityData.Name))
                {
                    return BadRequest("Имя должно быть обязательно!");
                }
                else
                {
                    City newCity = new City
                    {
                        Name = cityData.Name
                    };
                    bool result = await _supabaseContext.InsertCity(_supabaseClient, newCity);
                    if (result == true)
                    {
                        return Ok("Регистрация прошла успешно!");
                    }
                    else
                    {
                        return BadRequest("Не удалось добавить город в БД...");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }


        [HttpPut("UpdateCity", Name = "UpdateCity")]
        public async Task<ActionResult> UpdateCity([FromBody] UpdateCity UpdateCity)
        {
            try
            {
                City newUser = new City
                {
                    Id = UpdateCity.Id,
                    Name = UpdateCity.Name,
                    Population_size = UpdateCity.Population_size
                    
                };
                bool result = await _supabaseContext.UpdateCity(_supabaseClient, newUser);
                if (result == true)
                {
                    return Ok("Данные обновленны успешно!");
                }
                else
                {
                    return BadRequest("Не удалось обновить данные...");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

        [HttpDelete("CityDelete", Name = "CityDelete")]
        public async Task<ActionResult> CityDelete([FromBody] CityDelete CityDelete)
        {
            try
            {
                bool result = await _supabaseContext.CityDelete(_supabaseClient, CityDelete);
                if (result == true)
                {
                    return Ok("Пользователь успешно удалён!");
                }
                else
                {
                    return BadRequest("Не удалось удалить пользователя...");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

    }
    public class CityData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UpdateCity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]  
        public string Name { get; set; }
        [JsonProperty("population_size")]
        public int Population_size { get; set; }

    }

    public class CityDelete
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
