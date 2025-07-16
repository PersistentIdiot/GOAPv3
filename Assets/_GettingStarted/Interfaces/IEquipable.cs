using CrashKonijn.Docs.GettingStarted.Behaviours;

namespace _GettingStarted.Interfaces {
    public interface IEquipable {
        public bool TryEquip(AgentData data);
    }
}