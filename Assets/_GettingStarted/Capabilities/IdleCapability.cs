using _GettingStarted.Actions;
using CrashKonijn.Docs.GettingStarted.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class IdleCapability : CapabilityFactoryBase {
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder(nameof(IdleCapability));

            builder.AddGoal<IdleGoal>()
                .AddCondition<IsIdle>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(10);

            builder.AddAction<IdleAction>()
                .AddEffect<IsIdle>(EffectType.Increase)
                .SetTarget<IdleTarget>()
                .SetBaseCost(10);
            

            builder.AddTargetSensor<IdleTargetSensor>()
                .SetTarget<IdleTarget>();

            return builder.Build();
        }
    }
}