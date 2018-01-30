using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public List<Employee> EmpGetAll()
        {
            testEntities db = new testEntities();
            var query = from Employee in db.Employees
                        select Employee;
            List<Employee> emplist = query.ToList();
            return emplist;
        }
        [HttpPost]
        public Boolean EditEmp([FromBody]Newtonsoft.Json.Linq.JObject value)
        {

            var mystr = JObject.Parse(value.ToString());
            int id =int.Parse(mystr["ID"].ToString());
            if (id != null)
            {
                testEntities db = new testEntities();
                var Query = from Employee in db.Employees
                            where Employee.ID == id
                            select Employee;
                Employee emp = Query.Single();
                emp.Name = mystr["Name"].ToString();
                emp.Title = mystr["Title"].ToString();
                emp.Email = mystr["Email"].ToString();
                db.SaveChanges();
                return true;
            }
            return false;
        }
        [HttpPost]
        public Boolean AddEmp([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            try
            {
                var mystr = JObject.Parse(value.ToString());
                Employee emp = new Employee();
                emp.Name = mystr["Name"].ToString();
                emp.Title = mystr["Title"].ToString();
                emp.Email = mystr["Email"].ToString();
                testEntities db = new testEntities();
                db.Employees.Add(emp);
                db.SaveChanges();
                return true;
            }
            catch (Exception e){
                return false;
            }
        }
        [HttpPost]
        public Boolean DelteEmp([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            try
            {
            var mystr = JObject.Parse(value.ToString());
            int id =int.Parse(mystr["ID"].ToString());
            testEntities db = new testEntities();
            Employee objEmp = new Employee() { ID = id }; 
           db.Employees.Attach(objEmp);
           db.Employees.Remove(objEmp);
           db.SaveChanges();
            return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
       
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}