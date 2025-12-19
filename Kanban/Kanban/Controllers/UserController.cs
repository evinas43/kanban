using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Kanban.Model;
using System.Windows;
using System.Diagnostics;

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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // GET /user
                HttpResponseMessage response = await client.GetAsync("users");

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

        public async Task<User> GetUserByIdAsync(int id)
        {
            User user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // GET /user/{id}
                HttpResponseMessage response = await client.GetAsync($"users/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        user = await response.Content.ReadAsAsync<User>();
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    user = null;
                }
                else
                {
                    // TODO: handle other errors (log / throw)
                }
            }

            return user;
        }

        public async Task<int> GetUserTaskCountAsync(int id)
        {
            int count = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // GET /users/count/{id}
                HttpResponseMessage response = await client.GetAsync($"users/count/{id}");

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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // POST /user
                HttpResponseMessage response = await client.PostAsJsonAsync("users", user);

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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // PUT /user/{id}
                HttpResponseMessage response = await client.PutAsJsonAsync($"users/{id}", user);

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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // DELETE /user/{id}
                HttpResponseMessage response = await client.DeleteAsync($"users/{id}");

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

        public async Task<User> LoginWithUsersAsync(string username, string password)
        {
            try
            {
                var users = await GetAllUsersAsync();

                // Mostrar cuántos usuarios se han recibido
                Debug.WriteLine($"Usuarios recibidos: {users?.Count ?? 0}");

                // Mostrar detalles de cada usuario
                if (users != null)
                {
                    foreach (var u in users)
                    {
                        Debug.WriteLine($"User: id={u.Id}, username={u.UserName}, nom={u.Nom}, password={u.Passwd}, admin={u.IsAdmin}");
                    }
                }

                // Comprobar login
                foreach (User u in users)
                {
                    if (u.UserName == username && u.Passwd == password)
                    {
                        Debug.WriteLine($"Usuario encontrado: {u.UserName}");
                        return u;
                    }
                }

                Debug.WriteLine("Usuario no encontrado");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error en LoginWithUsersAsync: {e.Message}");
                MessageBox.Show(e.Message);
                return null;
            }
        }
    }
}
