using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class AttackTargetCommand : UnitCommandBase
    {
        public IUnit AttackedUnit { get; set; }
        public override void Execute() { }
    }
}