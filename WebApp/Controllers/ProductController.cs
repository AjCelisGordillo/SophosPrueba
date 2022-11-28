using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = new List<Product>();
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(URL.GetProducts);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonString);
                return View(products);
            }

            return View(products);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
            };

            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(URL.GetProductById + id);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    productVM.Product = JsonConvert.DeserializeObject<Product>(jsonString);

                    if (productVM.Product == null)
                    {
                        return NotFound();
                    }

                }
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Product.ProductId == 0)
                {
                    Product product = new Product();
                    product.Name = productVM.Product.Name;
                    product.Price = productVM.Product.Price;

                    var jsonString = JsonConvert.SerializeObject(product);
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.PostAsync(URL.CreateProduct, httpContent);

                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var objDb = new Product();
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(URL.GetProductById + productVM.Product.ProductId);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        objDb = JsonConvert.DeserializeObject<Product>(jsonString);

                        if (objDb != null)
                        {
                            objDb.Name = productVM.Product.Name;
                            objDb.Price = productVM.Product.Price;

                            var jsonString2 = JsonConvert.SerializeObject(objDb);
                            var httpContent = new StringContent(jsonString2, Encoding.UTF8, "application/json");

                            HttpClient client2 = new HttpClient();
                            HttpResponseMessage response2 = await client2.PatchAsync(URL.UpdateProduct + objDb.ProductId, httpContent);

                            if (response2.StatusCode == HttpStatusCode.NoContent)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }
            return View(productVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetProductById + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(jsonString);
                return View(product);
            }

            return View(new Product());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL.GetProductById + id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return NotFound();
            }

            HttpClient client2 = new HttpClient();
            HttpResponseMessage response2 = await client2.DeleteAsync(URL.DeleteProduct + id);

            if (response2.StatusCode != HttpStatusCode.NoContent)
            {
                return NotFound();
            }
   
            return RedirectToAction("Index");
        }
    }
}
