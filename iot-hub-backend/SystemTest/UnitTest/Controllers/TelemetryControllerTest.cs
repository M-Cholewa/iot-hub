

namespace SystemTest.UnitTest.Controllers
{
    public class TelemetryControllerTest
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