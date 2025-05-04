using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Supabase;
using System.Reflection;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class SupaBaseContext
    {
        public async Task<List<User>> GetUsers(Client _supabaseClient)
        {
            var result = await _supabaseClient.From<User>().Get();
            return result.Models;
        }

        public async Task<bool> InsertUser(Client _supabaseClient, User user)
        {
            try
            {
                await _supabaseClient.From<User>().Insert(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateNameUser(Client _supabaseClient, User user)
        {
            try
            {
                await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Set(x => x.Name, user.Name).Update();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateUser(Client _supabaseClient, User user)
        {
            try
            {
                var update_name = await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Set(x => x.Name, user.Name).Update();
                var update_age = await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Set(x => x.Age, user.Age).Update();
                var update_login = await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Set(x => x.Login, user.Login).Update();
                var update_password = await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Set(x => x.Password, user.Password).Update();
                var update_city = await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Set(x => x.City_id, user.City_id).Update();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(Client _supabaseClient, UserDelete user)
        {
            try
            {
                await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Delete();
                return true;
            }
            catch 
            { 
                return false;
            }
        }

    }
}
