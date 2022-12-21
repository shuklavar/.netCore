using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCDHProject.Models;

namespace MVCDHProject.Controllers
{
  
    public class CustomerController : Controller
    {
        private readonly ICustomerDAL obj;
        public CustomerController(ICustomerDAL obj)
        {
            this.obj = obj;
        }

        //CustomerXmlDAL obj = new CustomerXmlDAL();
        [AllowAnonymous]
        public ViewResult DisplayCustomers()
        {
            return View(obj.Customers_Select());
        }
        [AllowAnonymous]
        public ViewResult DisplayCustomer(int Custid)
        {
            //throw new IndexOutOfRangeException();
            return View(obj.Customer_Select(Custid));

        }
       

        public ViewResult AddCustomer()
        {
            return View(new Customer());
        }
        
        
        [HttpPost]
        public RedirectToActionResult AddCustomer(Customer customer)

        {
            obj.Customer_Insert(customer);
            return RedirectToAction("DisplayCustomers");
        }
   

        public ViewResult EditCustomer(int Custid)
        {
            return View(obj.Customer_Select(Custid));
        }
        public RedirectToActionResult UpdateCustomer(Customer customer)
        {
            obj.Customer_Update(customer);
            return RedirectToAction("Dispaly");

        }
    

        public RedirectToActionResult DeleteCustomer(int Custid)
        {
            obj.Customer_Delete(Custid);
            return RedirectToAction("DisplayCustomers");
        }
    }
}
