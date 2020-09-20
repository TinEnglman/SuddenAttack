using SuddenAttack.Controller.FlowController;
using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{
    public class PursuingBehavior : BehaviorBase
    {
        public IUnit PursuedUnit;

        private CommandController _commandController;

        public PursuingBehavior(CommandController commandController)
        {
            _commandController = commandController;
        }

        public override void Update(IUnit unit, float dt)
        {
            IMobileUnit pursuingUnit = unit as IMobileUnit;
            Vector2 direction = (PursuedUnit.Position - pursuingUnit.Position).normalized;
            unit.Position += direction * pursuingUnit.Data.MoveSpeed * dt;
        }

        public override void OnBegin(IUnit unit)
        {
            unit.OnMove(PursuedUnit.Position);
        }

        public override void OnEnd(IUnit unit)
        {
            _commandController.SetAttackTargetCommand(unit, PursuedUnit);
        }

        public override bool IsFinished(IUnit unit)
        {
            return ((PursuedUnit.Position - unit.Position).magnitude <= unit.WeaponData.Range);
        }
    }
}