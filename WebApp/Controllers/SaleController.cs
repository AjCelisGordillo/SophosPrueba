using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class SaleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<SaleVM> sales = new List<SaleVM>();
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(URL.GetSales);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                sales = JsonConvert.DeserializeObject<IEnumerable<SaleVM>>(jsonString);

                return View(sales);
            }

            return View(sales);
        }

        public async Task<IActionResult> CreateSale(int? id)
        {
            IEnumerable<Product> products = new List<Product>();
            IEnumerable<Client> clients = new List<Client>();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetProducts);
            HttpResponseMessage response2 = await client.GetAsync(URL.GetClients);


            if (response.StatusCode == HttpStatusCode.OK && response2.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
                var jsonString2 = await response2.Content.ReadAsStringAsync();
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonString2);
            }

            IEnumerable<SelectListItem> ProductsDropDown = products.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ProductId.ToString()
            });

            IEnumerable<SelectListItem> ClientsDropDown = clients.Select(i => new SelectListItem
            {
                Text = String.Concat(i.Name, " ", i.LastName),
                Value = i.Id.ToString()
            });

            ViewBag.ProductsDropDown = ProductsDropDown;
            ViewBag.ClientsDropDown = ClientsDropDown;

            CreateSaleVM saleVM = new CreateSaleVM();

            return View(saleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSale(CreateSaleVM saleVM)
        {
            if (ModelState.IsValid)
            {

                Sale sale = new Sale();
                sale.ClientId = saleVM.ClientId;
                sale.ProductId = saleVM.ProductId;
                sale.Quantity = saleVM.Quantity;

                var jsonString = JsonConvert.SerializeObject(sale);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(URL.CreateSale, httpContent);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index");
                }


            }
            return View(saleVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            IEnumerable<Product> products = new List<Product>();
            IEnumerable<Client> clients = new List<Client>();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetProducts);
            HttpResponseMessage response2 = await client.GetAsync(URL.GetClients);


            if (response.StatusCode == HttpStatusCode.OK && response2.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
                var jsonString2 = await response2.Content.ReadAsStringAsync();
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonString2);
            }

            IEnumerable<SelectListItem> ProductsDropDown = products.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ProductId.ToString()
            });

            IEnumerable<SelectListItem> ClientsDropDown = clients.Select(i => new SelectListItem
            {
                Text = String.Concat(i.Name, " ", i.LastName),
                Value = i.Id.ToString()
            });

            ViewBag.ProductsDropDown = ProductsDropDown;
            ViewBag.ClientsDropDown = ClientsDropDown;

            var saleVM = new SaleVM();

            HttpResponseMessage response3 = await client.GetAsync(URL.GetSaleById + id);

            if (response3.StatusCode != HttpStatusCode.OK)
            {
                return NotFound();
            }

            var jsonString3 = await response3.Content.ReadAsStringAsync();
            saleVM = JsonConvert.DeserializeObject<SaleVM>(jsonString3);

            return View(saleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SaleVM saleVM)
        {
            if (ModelState.IsValid)
            {
                var saleInDb = new Sale();
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(URL.GetSaleById + saleVM.SaleId);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    saleInDb = JsonConvert.DeserializeObject<Sale>(jsonString);

                    if (saleInDb != null)
                    {
                        saleInDb.SaleId = saleVM.SaleId;
                        saleInDb.ClientId = saleVM.ClientId;
                        saleInDb.ProductId = saleVM.ProductId;
                        saleInDb.Quantity = saleVM.Quantity;

                        var jsonString2 = JsonConvert.SerializeObject(saleInDb);
                        var httpContent = new StringContent(jsonString2, Encoding.UTF8, "application/json");

                        HttpClient client2 = new HttpClient();
                        HttpResponseMessage response2 = await client2.PatchAsync(URL.UpdateSale + saleInDb.SaleId, httpContent);

                        if (response2.StatusCode == HttpStatusCode.NoContent)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View(saleVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetSaleById + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sale = JsonConvert.DeserializeObject<SaleVM>(jsonString);
                return View(sale);
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.DeleteSale + id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return NotFound();
            }

            HttpClient client2 = new HttpClient();
            HttpResponseMessage response2 = await client2.DeleteAsync(URL.DeleteSale + id);

            if (response2.StatusCode != HttpStatusCode.NoContent)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
