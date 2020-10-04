using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketTracker.Testing.Security.Common;

namespace TicketTracker.Testing.Security.TokenGeneratorTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAtokenGeneratorIsNeeded : AbstractConfigurationBase
    {
        [TestMethod]
        public void WhenConstructed_WithValidArguments_ConstructsProperly()
        {
            var config = GetConfiguration();
        }
    }
}
