using System.Data;

namespace MVCDHProject.Models
{
    public class CustomerXmlDAL: ICustomerDAL
    {

        DataSet ds;
        public CustomerXmlDAL()
        {
            ds = new DataSet();
            ds.ReadXml("Customer.xml");
            //Adding Primary Key on Cusid
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Custid"]};
        }
        public List<Customer> Customers_Select()
        {
            List<Customer> Customers = new List<Customer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Customer obj = new Customer()
                {
                    Custid = Convert.ToInt32(dr["Custid"]),
                    Name = (string)dr["Name"],
                    Balance = Convert.ToDecimal(dr["Balance"]),
                    City = (string)dr["City"],
                    Status = Convert.ToBoolean(dr["Status"])
                };
                Customers.Add(obj);
            }
            return Customers;
        }
        public Customer Customer_Select(int Custid)
        {
            DataRow dr = ds.Tables[0].Rows.Find(Custid);

            Customer obj = new Customer
            {
                Custid = Convert.ToInt32(dr["Custid"]),
                Name = Convert.ToString(dr["Name"]),
                Balance = Convert.ToDecimal(dr["Balance"]),
                City = Convert.ToString(dr["City"]),
                Status = Convert.ToBoolean(dr["Status"])


            };
            return obj;
        }
        public void Customer_Insert(Customer customer)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["Custid"] = customer.Custid;
            dr["Name"] = customer.Name;
            dr["Balance"] = customer.Balance;
            dr["City"] = customer.City;
            dr["Status"] = customer.Status;
            //Adding the new DataRow to DataTable
            ds.Tables[0].Rows.Add(dr);
            //Saving data back to XML file
            ds.WriteXml("Customer.xml");

        }
        public void Customer_Update(Customer customer)
        {

            DataRow dr = ds.Tables[0].Rows.Find(customer.Custid);

            int Index = ds.Tables[0].Rows.IndexOf(dr);
            //Overriding the old values in DataRow with new values based on the Index
            ds.Tables[0].Rows[Index]["Name"] = customer.Name;
            ds.Tables[0].Rows[Index]["Balance"] = customer.Balance;
            ds.Tables[0].Rows[Index]["City"] = customer.City;
            //Saving data back to XML file
            ds.WriteXml("Customer.xml");
        }
        public void Customer_Delete(int Custid)
        {
            DataRow dr = ds.Tables[0].Rows.Find(Custid);
            int Index = ds.Tables[0].Rows.IndexOf(dr);
            //Dele
            //ds.Tables[0].Rows[Index].Delete();
           ds.Tables[0].Rows[Index]["Status"] = false;
            //Saving data back to XML file
            ds.WriteXml("Customer.xml");

        }
    }

}