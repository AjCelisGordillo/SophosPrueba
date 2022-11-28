using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<Client> clients = new List<Client>();
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(URL.GetClients);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                clients = JsonConvert.DeserializeObject<IEnumerable<Client>>(jsonString);
                return View(clients);
            }

            return View(clients);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ClientVM clientVM = new ClientVM()
            {
                Client = new Client(),
            };

            if (id == null)
            {
                return View(clientVM);
            }
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(URL.GetClientById + id);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    clientVM.Client = JsonConvert.DeserializeObject<Client>(jsonString);

                    if (clientVM.Client == null)
                    {
                        return NotFound();
                    }

                }
                return View(clientVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ClientVM clientVM)
        {
            if (ModelState.IsValid)
            {
                if (clientVM.Client.Id == 0)
                {
                    Client client = new Client();
                    client.DocumentId = clientVM.Client.DocumentId;
                    client.Name = clientVM.Client.Name;
                    client.LastName = clientVM.Client.LastName;
                    client.Telephone = clientVM.Client.Telephone;

                    var jsonString = JsonConvert.SerializeObject(client);
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    HttpClient httpclient = new HttpClient();
                    HttpResponseMessage response = await httpclient.PostAsync(URL.CreateClient, httpContent);

                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var objDb = new Client();
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(URL.GetClientById + clientVM.Client.Id);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        objDb = JsonConvert.DeserializeObject<Client>(jsonString);

                        if (objDb != null)
                        {
                            objDb.DocumentId = clientVM.Client.DocumentId;
                            objDb.Name = clientVM.Client.Name;
                            objDb.LastName = clientVM.Client.LastName;
                            objDb.Telephone = clientVM.Client.Telephone;

                            var jsonstring2 = JsonConvert.SerializeObject(objDb);
                            var httpContent = new StringContent(jsonstring2, Encoding.UTF8, "application/json");

                            HttpClient client2 = new HttpClient();
                            HttpResponseMessage response2 = await client2.PatchAsync(URL.UpdateClient + objDb.Id, httpContent);

                            if (response2.StatusCode == HttpStatusCode.NoContent)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }
            return View(clientVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetClientById + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var Client = JsonConvert.DeserializeObject<Client>(jsonString);
                return View(Client);
            }

            return View(new Client());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetClientById + id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return NotFound();
            }

            HttpClient client2 = new HttpClient();
            HttpResponseMessage response2 = await client2.DeleteAsync(URL.DeleteClient + id);

            if (response2.StatusCode != HttpStatusCode.NoContent)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
