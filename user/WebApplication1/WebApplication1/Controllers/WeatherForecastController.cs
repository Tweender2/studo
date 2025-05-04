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
            try
            {
                var result = await _supabaseContext.GetUsers(_supabaseClient);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [HttpPost("InsertUser", Name = "InsertUser")]
        public async Task<ActionResult> InsertUser([FromBody] UserData userData)
        {
            try
            {
                if(string.IsNullOrEmpty(userData.Login) || string.IsNullOrEmpty(userData.Password))
                {
                    return BadRequest("Пустой логин или пароль");
                }
                else
                {
                    User newUser = new User
                    {
                        Login = userData.Login,
                        Password = userData.Password
                    };
                    bool result = await _supabaseContext.InsertUser(_supabaseClient, newUser);
                    if (result == true)
                    {
                        return Ok("Регистрация прошла успешно!");
                    }
                    else
                    {
                        return BadRequest("Не удалось добавить пользователя в БД...");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

        [HttpPut("UpdateNameUser", Name = "UpdateNameUser")]
        public async Task<ActionResult> UpdateNameUser([FromBody] UpdateNameUser UpdateNameUser)
        {
            try
            {
                User newUser = new User
                {
                    Id = UpdateNameUser.Id,
                    Name = UpdateNameUser.Name
                };
                bool result = await _supabaseContext.UpdateNameUser(_supabaseClient, newUser);
                if (result == true)
                {
                    return Ok("Имя успешно обновленно!");
                }
                else
                {
                    return BadRequest("Не обновить имя...");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }


        [HttpPut("UpdateUser", Name = "UpdateeUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUser UpdateUser)
        {
            try
            {
                User newUser = new User
                {
                    Id = UpdateUser.Id,
                    Name = UpdateUser.Name,
                    Age = UpdateUser.Age,
                    Login = UpdateUser.Login,
                    Password = UpdateUser.Password,
                    City_id = UpdateUser.City_id
                };
                bool result = await _supabaseContext.UpdateUser(_supabaseClient, newUser);
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

        [HttpDelete("DeleteUser", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromBody] UserDelete userDelete)
        {
            try
            {
                bool result = await _supabaseContext.DeleteUser(_supabaseClient, userDelete);
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
    public class UserData
    {
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class UpdateNameUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UpdateUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]  
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("city_id")]
        public string City_id { get; set; }

    }

    public class UserDelete
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
