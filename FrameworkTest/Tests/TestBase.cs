using Testing.Common;

namespace FrameworkTest.Tests
{
    //All tests should inherit from this base class.
    public class TestBase
    {
        //ApiEngine is the core of the framework.
        protected ApiEngine ApiEngine { get; set; }

        public TestBase()
        {
            ApiEngine = new ApiEngine("token");
        }
        
        
    }
}
