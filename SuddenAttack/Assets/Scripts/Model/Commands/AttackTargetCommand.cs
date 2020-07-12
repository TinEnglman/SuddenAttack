using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class AttackTargetCommand : UnitCommandBase
    {
        public IUnit Target { get; set; }

        public AttackTargetCommand(BehaviorManager behaviorManager) : base(behaviorManager) { }

        public override void Execute()
        {
            float distance = (Unit.Prefab.transform.position - Target.Prefab.transform.position).magnitude;

            if (distance > Unit.WeaponData.Range)
            {
                // add pursuit behavior
            }
            else
            {
                var attackingBehavior = new AttackingBehavior();
                attackingBehavior.Target = Target;
                _behaviorManager.SetBehavior(Unit, attackingBehavior);
                // combat manager should be notified in the local player conmmand controller
                // add pursuiz command to queue in the local player conmmand controller
            }
        }
    }
}