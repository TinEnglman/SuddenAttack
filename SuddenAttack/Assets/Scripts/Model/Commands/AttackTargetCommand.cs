using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class AttackTargetCommand : UnitCommandBase
    {
        public IUnit AttackedUnit { get; set; }

        public AttackTargetCommand(BehaviorManager behaviorManager) : base(behaviorManager) { }

        public override void Execute()
        {
            Unit.OnAttack(AttackedUnit);
        }
    }
}