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
            
            builder.AddMultiSensor<LogSensor>();
            builder.AddMultiSensor<ChestSensor>();
            builder.AddWorldSensor<IsHoldingSensor<Log>>()
                .SetKey<IsHolding<Log>>()
                ;

            
            // Logs
            builder.AddGoal<PickupItemGoal<Log>>()
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(2)
                ;
            
            builder.AddAction<PickupLogAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Increase)
                .SetTarget<ClosestLog>();

            builder.AddGoal<HaulItemGoal<Log>>()
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);

            builder.AddAction<HaulLogAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Decrease)
                .SetTarget<ClosestChest>();
            
            
            
            return builder.Build();
            
            /*
            // Working backup
            builder.AddMultiSensor<LogSensor>();
            builder.AddMultiSensor<ChestSensor>();
            builder.AddWorldSensor<IsHoldingSensor<Log>>()
                .SetKey<IsHolding<Log>>()
                ;

            builder.AddGoal<PickupLogGoal>()
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(2)
                ;

            builder.AddGoal<HaulLogsGoal>()
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);

            builder.AddAction<PickupLogAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Increase)
                .SetTarget<ClosestLog>();

            builder.AddAction<HaulLogAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Decrease)
                .SetTarget<ClosestChest>();
            */
            
            /*
            // Working but old
            builder.AddMultiSensor<LogSensor>();
            builder.AddMultiSensor<ChestSensor>();

            builder.AddGoal<PickupLogGoal>()
                .AddCondition<LogCount>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(2)
                ;

            builder.AddGoal<HaulLogsGoal>()
                .AddCondition<LogCount>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(1);

            builder.AddAction<PickupLogAction>()
                .AddEffect<LogCount>(EffectType.Increase)
                .SetTarget<ClosestLog>();

            builder.AddAction<HaulLogAction>()
                .AddEffect<LogCount>(EffectType.Decrease)
                .SetTarget<ClosestChest>();
            */

            
            /*
            // Not working, won't return Logs
            builder.AddMultiSensor<ItemSensor<Log>>();
            builder.AddMultiSensor<ItemSensor<Chest>>();

            builder.AddGoal<HaulLogsGoal>()
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(0);

            
            builder.AddAction<PickupItemAction<Log>>()
                .AddEffect<IsHolding<Log>>(EffectType.Increase)
                //.AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetTarget<ClosestTarget<Log>>();
            
            
            builder.AddAction<HaulLogAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Decrease)
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .SetTarget<ClosestTarget<Chest>>();
            
            */
            /*
            builder.AddMultiSensor<ItemSensor<Log>>();
            builder.AddMultiSensor<ItemSensor<Chest>>();
            builder.AddWorldSensor<IsHoldingSensor<Log>>()
                .SetKey<IsHolding<Log>>()
                ;

            builder.AddGoal<HaulLogsGoal>()
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetBaseCost(0)
                ;

            
            builder.AddGoal<PickupItemGoal<Log>>()
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThan, 0)
                .SetBaseCost(2);
            

            
            builder.AddAction<PickupItemAction<Log>>()
                .AddEffect<IsHolding<Log>>(EffectType.Increase)
                .AddCondition<IsHolding<Log>>(Comparison.SmallerThanOrEqual, 0)
                .SetTarget<ClosestTarget<Log>>();
            
            
            builder.AddAction<HaulLogAction>()
                .AddEffect<IsHolding<Log>>(EffectType.Decrease)
                .AddCondition<IsHolding<Log>>(Comparison.GreaterThanOrEqual, 1)
                .SetTarget<ClosestTarget<Chest>>()
                .SetMoveMode(ActionMoveMode.PerformWhileMoving)
                ;
            
            */
        }
    }
}