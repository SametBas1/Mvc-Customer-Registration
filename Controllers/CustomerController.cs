using Microsoft.AspNetCore.Mvc;
using Mvc_Musterı_Kayıt.Models;
using System.Linq;

namespace Mvc_Musterı_Kayıt.Controllers
{
    public class CustomerController : Controller
    {
        private static List<Customer> customers = new List<Customer>();

        // Ana sayfa (Müşteri Listesi)
        public IActionResult Index()
        {
            if (!customers.Any())
            {
                customers.Add(new Customer { Id = 1, FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet@example.com", Phone = "05303301212", Address = "İstanbul/Maltepe" });
                customers.Add(new Customer { Id = 2, FirstName = "Mehmet", LastName = "Kaya", Email = "mehmet@example.com", Phone = "08503451269", Address = "İstanbul/Pendik" });
            }
            return View(customers);
            //return View();
        }

        // Müşteri Ekleme Sayfası 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Müşteri Ekleme İşlemi
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid) // Eğer model doğruysa
            {
                customer.Id = customers.Count + 1;
                customers.Add(customer);
                return RedirectToAction("Index");
            }
            return View(customer); // Hata varsa aynı sayfayı tekrar yükle
        }

        // Müşteri Düzenleme Sayfası
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound(); // Eğer müşteri bulunamazsa hata döner

            return View(customer);
        }

        // Müşteri Düzenleme İşlemi
        [HttpPost]
        public IActionResult Edit(int id, Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            if (ModelState.IsValid) // Eğer model doğruysa
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                customer.Phone = updatedCustomer.Phone;
                customer.Address = updatedCustomer.Address;

                return RedirectToAction("Index"); // Güncelleme başarılıysa ana sayfaya dön
            }

            return View(updatedCustomer); // Hata varsa düzenleme sayfasına geri dön
        }

        // Müşteri Silme
        public IActionResult Delete(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            customers.Remove(customer);
            return RedirectToAction("Index");
        }
    }
}
