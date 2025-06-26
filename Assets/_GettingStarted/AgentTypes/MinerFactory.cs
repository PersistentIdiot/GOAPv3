using CrashKonijn.Docs.GettingStarted.Capabilities;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.AgentTypes {
    public class MinerFactory: AgentTypeFactoryBase {
        public override IAgentTypeConfig Create() {
            var factory = new AgentTypeBuilder("Miner");

            factory.AddCapability<IdleCapability>();
            factory.AddCapability<PearCapability>();
            factory.AddCapability<EatCapability>();
            factory.AddCapability<MineCapability>();
            
            return factory.Build();
        }
    }
}