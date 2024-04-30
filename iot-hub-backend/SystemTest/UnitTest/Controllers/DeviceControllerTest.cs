

namespace SystemTest.UnitTest.Controllers
{
    public class DeviceControllerTest
    {
        [Fact]
        public void SomeEndpointTest()
        {
            Random r = new Random();
            int rInt = r.Next(100, 500);

            Thread.Sleep(rInt);
        }
    }
}