using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Kanban.Model;


namespace Kanban.Controllers
{
    public class UserController
    {
        string BaseUri;


        public UserController()
        {
            BaseUri = ConfigurationManager.AppSettings["BaseUri"];
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // GET /user
                HttpResponseMessage response = await client.GetAsync("user");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        users = new List<User>();
                    }
                    else
                    {
                        users = await response.Content.ReadAsAsync<List<User>>();
                    }
                }
                else
                {
                    // TODO: error handling
                }
            }

            return users;
        }

        public async Task<int> GetUserTaskCountAsync(int id)
        {
            int count = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // GET /user/count/{id}
                HttpResponseMessage response = await client.GetAsync($"user/count/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        count = int.Parse(json);

                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    count = 0;
                }
                else
                {
                    // TODO: handle other errors (log / throw)
                }
            }

            return count;
        }

        public async Task<User> InsertUserAsync(User user)
        {
            User createdUser = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // POST /user
                HttpResponseMessage response = await client.PostAsJsonAsync("user", user);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        createdUser = await response.Content.ReadAsAsync<User>();
                    }
                }
                else
                {
                    // TODO: handle error (log / throw)
                }
            }

            return createdUser;
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            bool updated = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // PUT /user/{id}
                HttpResponseMessage response = await client.PutAsJsonAsync($"user/{id}", user);

                if (response.IsSuccessStatusCode)
                {
                    // 204 → update successful
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        updated = true;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    updated = false;
                }
                else
                {
                    // TODO: handle other errors (log / throw)
                }
            }

            return updated;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            bool deleted = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // DELETE /user/{id}
                HttpResponseMessage response = await client.DeleteAsync($"user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // 200 or 204 → deleted successfully
                    deleted = true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    deleted = false;
                }
                else
                {
                    // TODO: handle other errors (log / throw)
                }
            }

            return deleted;
        }

    }
}
