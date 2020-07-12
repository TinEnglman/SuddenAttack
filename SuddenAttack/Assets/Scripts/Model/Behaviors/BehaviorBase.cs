using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Behavior
{
    public abstract class BehaviorBase : IBehavior
    {
        public abstract void Update(IUnit unit, float dt);
        public abstract void OnBegin(IUnit unit);
        public abstract void OnEnd(IUnit unit);
        public abstract bool IsFinished(IUnit unit);
    }
}