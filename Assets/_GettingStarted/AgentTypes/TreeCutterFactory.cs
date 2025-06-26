using CrashKonijn.Docs.GettingStarted.Capabilities;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.AgentTypes {
    public class TreeCutterFactory : AgentTypeFactoryBase {
        public override IAgentTypeConfig Create() {
            var factory = new AgentTypeBuilder("TreeCutter");

            factory.AddCapability<IdleCapability>();
            factory.AddCapability<PearCapability>();
            factory.AddCapability<EatCapability>();
            factory.AddCapability<CutTreeCapability>();
            
            return factory.Build();
        }
    }
}