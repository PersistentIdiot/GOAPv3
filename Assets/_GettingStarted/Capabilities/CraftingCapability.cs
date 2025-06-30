using _GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Docs.GettingStarted.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Sensors.World;
using CrashKonijn.Goap.Runtime;
using Unity.VisualScripting;
using Comparison = CrashKonijn.Goap.Core.Comparison;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class CraftingCapability : CapabilityFactoryBase{
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(LogHaulingCapability));
            
            
            builder = CreateSensors(builder);
            builder = CreateGoals(builder);
            builder = CreateActions(builder);
            
            return builder.Build();
        }

        private CapabilityBuilder CreateSensors(CapabilityBuilder builder) {
            // Sensors
            builder.AddMultiSensor<LogSensor>();
            builder.AddWorldSensor<IsHoldingSensor<Log>>()
                .SetKey<IsHolding<Log>>();
            
            builder.AddMultiSensor<StoneSensor>();
            builder.AddWorldSensor<IsHoldingSensor<Stone>>()
                .SetKey<IsHolding<Stone>>();
            
            builder.AddWorldSensor<IsHoldingSensor<Axe>>()
                .SetKey<IsHolding<Axe>>();

            builder.AddMultiSensor<ChestSensor>();
            
            return builder;
        }
        
        private CapabilityBuilder CreateGoals(CapabilityBuilder builder) {
            // Goals
            /*
            builder.AddGoal<PickupItemGoal<Log>>()
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThan, 0)
                .SetBaseCost(2);
            
            builder.AddGoal<PickupItemGoal<Stone>>()
                .AddCondition<IsHolding<Stone>>(Comparison.GreaterThan, 0)
                .SetBaseCost(2);
            */

            builder.AddGoal<HaulItemGoal<Axe>>()
                .AddCondition<IsHolding<Axe>>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);

            builder.AddGoal<CraftItemGoal<Axe>>()
                .AddCondition<IsHolding<Stone>>(Comparison.GreaterThanOrEqual, 1)
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .AddCondition<IsHolding<Axe>>(Comparison.GreaterThan, 0)
                .SetBaseCost(1);

            return builder;
        }
        
        private CapabilityBuilder CreateActions(CapabilityBuilder builder) {
            // Actions
            builder.AddAction<PickupHoldableAction>()
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 3)
                .AddEffect<IsHolding<Log>>(EffectType.Increase)
                .SetTarget<ClosestHoldable<Log>>();
            
            builder.AddAction<PickupHoldableAction>()
                .AddCondition<IsHolding<Stone>>(Comparison.SmallerThanOrEqual, 3)
                .AddEffect<IsHolding<Stone>>(EffectType.Increase)
                .SetTarget<ClosestHoldable<Stone>>();

            builder.AddAction<CreateItemAction<Axe>>()
                .AddCondition<IsHolding<Stone>>(Comparison.GreaterThanOrEqual, 1)
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .AddEffect<IsHolding<Axe>>(EffectType.Increase)
                .AddEffect<IsHolding<Log>>(EffectType.Decrease)
                .AddEffect<IsHolding<Stone>>(EffectType.Decrease)
                .SetRequiresTarget(false);

            
            builder.AddAction<HaulItemAction<Axe>>()
                .AddCondition<IsHolding<Axe>>(Comparison.GreaterThanOrEqual, 1)
                .AddEffect<IsHolding<Axe>>(EffectType.Decrease)
                .SetTarget<ClosestChest>();
            

            return builder;
        }
    }
}