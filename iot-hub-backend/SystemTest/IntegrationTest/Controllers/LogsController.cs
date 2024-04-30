using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTest.IntegrationTest.Controllers
{
    public class LogsController
    {
        [Fact]
        public void EndpointTest() {
            Random r = new Random();
            int rInt = r.Next(100, 500);

            Thread.Sleep(rInt);
        }
    }
}
