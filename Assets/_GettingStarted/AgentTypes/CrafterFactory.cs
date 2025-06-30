using CrashKonijn.Docs.GettingStarted.Capabilities;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.AgentTypes {
    public class CrafterFactory: AgentTypeFactoryBase {
        public override IAgentTypeConfig Create() {
            var factory = new AgentTypeBuilder("Crafter");

            factory.AddCapability<IdleCapability>();
            factory.AddCapability<PearCapability>();
            factory.AddCapability<EatCapability>();
            factory.AddCapability<CraftingCapability>();
            
            return factory.Build();
        }
    }
}