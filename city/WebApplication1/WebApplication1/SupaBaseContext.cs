using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Supabase;
using System.Reflection;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class SupaBaseContext
    {

        public async Task<List<City>> GetUsers(Client _supabaseClient)
        {

            var result = await _supabaseClient.From<City>().Get();
            return result.Models;
        }

        public async Task<bool> InsertCity(Client _supabaseClient, City city)
        {
            try
            {
                await _supabaseClient.From<City>().Insert(city);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateCity(Client _supabaseClient, City city)
        {
            try
            {
                var update_name = await _supabaseClient.From<City>().Where(x => x.Id == city.Id).Set(x => x.Name, city.Name).Update();
                var update_age = await _supabaseClient.From<City>().Where(x => x.Id == city.Id).Set(x => x.Population_size, city.Population_size).Update();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CityDelete(Client _supabaseClient, CityDelete city)
        {
            try
            {
                await _supabaseClient.From<City>().Where(x => x.Id == city.Id).Delete();
                return true;
            }
            catch 
            { 
                return false;
            }
        }

    }
}
