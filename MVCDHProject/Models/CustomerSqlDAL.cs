namespace MVCDHProject.Models
{
    public class CustomerSqlDAL : ICustomerDAL
    {
        private readonly MVCCoreDbContext dc;
        public CustomerSqlDAL(MVCCoreDbContext context)
        {
            this.dc = context;
        }
        public List<Customer> Customers_Select()
        {
            var customers = dc.Customers.Where(C => C.Status == true).ToList();
            return customers;
        }
        public Customer Customer_Select(int Custid)
        {

            return dc.Customers.Find(Custid);
        }
        public void Customer_Insert(Customer customer)
        {
            dc.Customers.Add(customer);
            dc.SaveChanges();
        }
        public void Customer_Update(Customer customer)
        {
            customer.Status = true;
            dc.Update(customer);
            dc.SaveChanges();
        }
        public void Customer_Delete(int Cusid)
        {

            Customer customer = dc.Customers.Find(Cusid);
            dc.Remove(customer);//Permanent deletion
            customer.Status = false;
            dc.SaveChanges();

        }
    }
}
