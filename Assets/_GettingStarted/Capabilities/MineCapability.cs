using _GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Sensors.Multi;
using CrashKonijn.Goap.Demos.Complex.Targets;
using CrashKonijn.Goap.Demos.Complex.WorldKeys;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class MineCapability: CapabilityFactoryBase {
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(MineCapability));
            
            builder.AddMultiSensor<ItemSensor<StoneMine>>();

            builder.AddGoal<HarvestGoal<StoneMine>>()
                .SetBaseCost(1)
                .AddCondition<IsInWorld<StoneMine>>(Comparison.SmallerThan, 0);

            builder.AddAction<MineAction>()
                .SetTarget<ClosestTarget<StoneMine>>()
                .AddEffect<IsInWorld<StoneMine>>(EffectType.Decrease);
            

            return builder.Build();
        }
    }
}