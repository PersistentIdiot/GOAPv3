using _GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Docs.GettingStarted.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class PearCapability : CapabilityFactoryBase{
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(PearCapability));

            builder.AddGoal<PickupPearGoal>()
                .AddCondition<PearCount>(Comparison.GreaterThanOrEqual, 3);
            
            builder.AddAction<PickupItemAction<Pear>>()
                .AddEffect<PearCount>(EffectType.Increase)
                .SetTarget<ClosestPear>();

            builder.AddMultiSensor<PearSensor>();

            return builder.Build();
        }
    }
}