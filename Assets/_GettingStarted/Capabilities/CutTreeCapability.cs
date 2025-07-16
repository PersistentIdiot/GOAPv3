using _GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Docs.GettingStarted.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Sensors.Multi;
using CrashKonijn.Goap.Demos.Complex.Sensors.World;
using CrashKonijn.Goap.Demos.Complex.Targets;
using CrashKonijn.Goap.Demos.Complex.WorldKeys;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class CutTreeCapability: CapabilityFactoryBase {
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(CutTreeCapability));

            builder = AddGoals(builder);
            builder = AddSensors(builder);
            builder = AddActions(builder);

            return builder.Build();
        }

        private CapabilityBuilder AddSensors(CapabilityBuilder builder) {
            
            builder.AddMultiSensor<ItemSensor<Tree>>();
            builder.AddWorldSensor<IsHoldingSensor<Axe>>()
                .SetKey<IsHolding<Axe>>();

            builder.AddMultiSensor<AxeSensor>();


            return builder;
        }

        private CapabilityBuilder AddGoals(CapabilityBuilder builder) {
            builder.AddGoal<CutTreeGoal>()
                .SetBaseCost(2)
                .AddCondition<IsInWorld<Tree>>(Comparison.SmallerThan  , 1)
                .AddCondition<IsHolding<Axe>>(Comparison.GreaterThanOrEqual, 1);

            builder.AddGoal<PickupItemGoal<Axe>>()
                .AddCondition<IsHolding<Axe>>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);

            return builder;
        }
        private CapabilityBuilder AddActions    (CapabilityBuilder builder) {
            builder.AddAction<CutTreeAction>()
                .SetTarget<ClosestTarget<Tree>>()
                .AddEffect<IsInWorld<Tree>>(EffectType.Decrease)
                .AddCondition<IsHolding<Axe>>(Comparison.GreaterThanOrEqual, 1);


            builder.AddAction<PickupItemAction<Axe>>()
                .AddEffect<IsHolding<Axe>>(EffectType.Increase)
                .AddCondition<IsHolding<Axe>>(Comparison.SmallerThanOrEqual, 0)
                .SetTarget<ClosestHoldable<Axe>>();
                

            return builder;
        }
    }
}