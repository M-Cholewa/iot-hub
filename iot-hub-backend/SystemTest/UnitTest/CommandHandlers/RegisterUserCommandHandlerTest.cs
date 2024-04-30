using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTest.UnitTest.CommandHandlers
{
    public class RegisterUserCommandHandlerTest
    {
        [Fact]
        public void Handle()
        {
            Random r = new Random();
            int rInt = r.Next(100, 500);

            Thread.Sleep(rInt);
        }
    }
}
