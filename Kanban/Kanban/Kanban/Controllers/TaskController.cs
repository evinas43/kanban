using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Kanban.Model;

namespace Kanban.Controllers
{
    internal class TaskController
    {
        string BaseUri;

        public TaskController()
        {   
            BaseUri = ConfigurationManager.AppSettings["BaseUri"];
        }



        public async Task<List<Tasques>> GetTasquesByEstatAsync(int estat)
        {
            List<Tasques> tasques = new List<Tasques>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // GET /tasks/estat/{estat}
                HttpResponseMessage response = await client.GetAsync($"task/estat/{estat}");

                if (response.IsSuccessStatusCode)
                {
                    // 204 → no data
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        tasques = new List<Tasques>();
                    }
                    else
                    {
                        tasques = await response.Content.ReadAsAsync<List<Tasques>>();
                        response.Dispose();
                    }
                }
                else
                {
                    // TODO: handle error (log / throw / return empty list)
                }
            }

            return tasques;
        }

        public async Task<Tasques> GetTascaByIdAsync(int id)
        {
            Tasques tasca = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // GET /tasks/{id}
                HttpResponseMessage response = await client.GetAsync($"task/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // 204 → no content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        tasca = null;
                    }
                    else
                    {
                        tasca = await response.Content.ReadAsAsync<Tasques>();
                    }
                }
                else
                {
                    // Optional: handle errors
                    // e.g. throw new Exception($"Error {response.StatusCode}");
                }
            }

            return tasca;
        }

        public async Task<Tasques> InsertTascaAsync(Tasques tasca)
        {
            Tasques tascaCreada = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // POST /task
                HttpResponseMessage response = await client.PostAsJsonAsync("task", tasca);

                if (response.IsSuccessStatusCode)
                {
                    // 204 → no content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        tascaCreada = null;
                    }
                    else
                    {
                        tascaCreada = await response.Content.ReadAsAsync<Tasques>();
                    }
                }
                else
                {
                    // TODO: handle error (log / throw)
                }
            }

            return tascaCreada;
        }


        public async Task<bool> UpdateTascaAsync(int id, Tasques tasca)
        {
            bool updated = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // PUT /task/{id}
                HttpResponseMessage response = await client.PutAsJsonAsync($"task/{id}", tasca);

                if (response.IsSuccessStatusCode)
                {
                    // 204 → update OK, no response body
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


        public async Task<bool> DeleteTascaAsync(int id)
        {
            bool deleted = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                // DELETE /task/{id}
                HttpResponseMessage response = await client.DeleteAsync($"task/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // 200 or 204 → deleted OK
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

