using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Docs.GettingStarted.Capabilities;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Factories.Extensions;
using CrashKonijn.Goap.Demos.Complex.Sensors.Multi;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.AgentTypes {
    public class HaulerFactory: AgentTypeFactoryBase {
        public override IAgentTypeConfig Create() {
            var factory = new AgentTypeBuilder("Hauler");

            factory.AddCapability<IdleCapability>();
            factory.AddCapability<PearCapability>();
            factory.AddCapability<EatCapability>();
            factory.AddCapability<LogHaulingCapability>();
            
            return factory.Build();
        }
    }
}