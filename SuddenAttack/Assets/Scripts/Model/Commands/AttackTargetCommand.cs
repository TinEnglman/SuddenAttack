using SuddenAttack.Controller.FlowController;
using SuddenAttack.Model.Behavior;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class AttackTargetCommand : UnitCommandBase
    {
        public IUnit Target { get; set; }

        private CombatManager _combatManager;
        private CommandController _commandController;

        public AttackTargetCommand(BehaviorManager behaviorManager, CombatManager combatManager, CommandController commandController) : base(behaviorManager)
        {
            _combatManager = combatManager;
            _commandController = commandController;
        }

        public override void Execute()
        {
            if (Target.HitPoints <= 0)
            {
                return;
            }

            float distance = (Unit.Prefab.transform.position - Target.Prefab.transform.position).magnitude;

            if (distance > Unit.WeaponData.Range)
            {
                var pursuingBehavior = new PursuingBehavior(_commandController);
                pursuingBehavior.PursuedUnit = Target;
                _behaviorManager.SetBehavior(Unit, pursuingBehavior);
                _commandController.InjectAttackTargetCommand(Unit, Target);
            }
            else
            {
                var attackingBehavior = new AttackingBehavior(_combatManager);
                attackingBehavior.Target = Target;
                _behaviorManager.SetBehavior(Unit, attackingBehavior);
            }
        }
    }
}