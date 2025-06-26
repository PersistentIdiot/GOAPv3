using System.Collections;
using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted;
using CrashKonijn.Docs.GettingStarted.Actions;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Capabilities {
    public class EatCapability : CapabilityFactoryBase {
        public override ICapabilityConfig Create() {
            var builder = new CapabilityBuilder("EatCapability");

            builder.AddGoal<EatGoal>()
                .AddCondition<Hunger>(Comparison.SmallerThanOrEqual, 0);

            builder.AddAction<EatAction>()
                .AddCondition<PearCount>(Comparison.GreaterThanOrEqual, 1)
                .AddEffect<Hunger>(EffectType.Decrease)
                //.SetTarget<ClosestPear>()
                .SetRequiresTarget(false);

            return builder.Build();
        }
    }
}