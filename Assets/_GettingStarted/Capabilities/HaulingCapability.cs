using _GettingStarted.Actions;
using CrashKonijn.Agent.Core;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Docs.GettingStarted.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Sensors.Multi;
using CrashKonijn.Goap.Demos.Complex.Sensors.World;
using CrashKonijn.Goap.Demos.Complex.Targets;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class LogHaulingCapability: CapabilityFactoryBase {
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(LogHaulingCapability));
            builder.AddMultiSensor<ChestSensor>();
            
            builder = CreateLogHaulingCapability(builder);
            builder = CreateStoneHaulingCapability(builder);
            return builder.Build();
        }

        private CapabilityBuilder CreateLogHaulingCapability(CapabilityBuilder builder) {
            // Sensors
            builder.AddMultiSensor<LogSensor>();
            builder.AddWorldSensor<IsHoldingSensor<Log>>()
                .SetKey<IsHolding<Log>>();

            // Goals
            builder.AddGoal<PickupItemGoal<Log>>()
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(2);
            
            builder.AddGoal<HaulItemGoal<Log>>()
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);
            
            
            // Actions
            builder.AddAction<PickupHoldableAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Increase)
                .SetTarget<ClosestHoldable<Log>>();

            builder.AddAction<HaulItemAction<Log>>()
                .AddEffect<IsHolding<Log>>(EffectType.Decrease)
                .SetTarget<ClosestChest>();

            return builder;
        }

        
        private CapabilityBuilder CreateStoneHaulingCapability(CapabilityBuilder builder) {
            // Sensors
            builder.AddMultiSensor<StoneSensor>();
            builder.AddWorldSensor<IsHoldingSensor<Stone>>()
                .SetKey<IsHolding<Stone>>();
            
            
            // Goals
            builder.AddGoal<PickupItemGoal<Stone>>()
                .AddCondition<IsHolding<Stone>>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(2);
            
            builder.AddGoal<HaulItemGoal<Stone>>()
                .AddCondition<IsHolding<Stone>>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);
            
            
            // Actions
            builder.AddAction<PickupHoldableAction>()
                .AddEffect<IsHolding<Stone>>(EffectType.Increase)
                .SetTarget<ClosestHoldable<Stone>>();

            builder.AddAction<HaulItemAction<Stone>>()
                .AddEffect<IsHolding<Stone>>(EffectType.Decrease)
                .SetTarget<ClosestChest>();

            return builder;
        }
    }
}