using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hotel.Wifi.Access.Checker.Api
{
    public class DemoController : ApiController
    {
        // GET api/demo
        [HttpGet] 
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

        // GET api/demo/5 
        public string Get(int id)
        {
            return "Hello, World!";
        }

        // POST api/demo 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/demo/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/demo/5 
        public void Delete(int id)
        {
        }
    }
}
