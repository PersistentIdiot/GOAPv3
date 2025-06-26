using _GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Sensors.Multi;
using CrashKonijn.Goap.Demos.Complex.Targets;
using CrashKonijn.Goap.Demos.Complex.WorldKeys;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class CutTreeCapability: CapabilityFactoryBase {
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(CutTreeCapability));
            
            builder.AddMultiSensor<ItemSensor<Tree>>();

            builder.AddGoal<CutTreeGoal>()
                .SetBaseCost(1)
                .AddCondition<IsInWorld<Tree>>(Comparison.SmallerThan  , 1);

            builder.AddAction<CutTreeAction>()
                .SetTarget<ClosestTarget<Tree>>()
                .AddEffect<IsInWorld<Tree>>(EffectType.Decrease);
            

            return builder.Build();
        }
    }
}