using SuddenAttack.Controller.FlowController;
using SuddenAttack.Model.Units;
using System.Collections.Generic;
using UnityEngine;


namespace SuddenAttack.Model.Behavior
{
    public class AttackMoveBehavior : MovingBehavior
    {
        private UnitManager _unitManager;
        private CommandController _commandController;

        public AttackMoveBehavior(CommandController commandController, UnitManager unitManager)
        {
            _commandController = commandController;
            _unitManager = unitManager;
        }

        public override void Update(IUnit unit, float dt)
        {
            var units = GetTargets((IMobileUnit)unit);

            if (units.Count > 0)
            {
                EngageTarget(unit, units[0]);
            }
            else
            {
                base.Update(unit, dt);
            }
        }

        public override void OnBegin(IUnit unit)
        {
            var units = GetTargets((IMobileUnit)unit);

            if (units.Count > 0)
            {
                EngageTarget(unit, units[0]);
            }
        }

        public override void OnEnd(IUnit unit)
        {
        }

        public override bool IsFinished(IUnit unit)
        {
            return base.IsFinished(unit);
        }

        private List<IUnit> GetTargets(IMobileUnit unti) // todo; get closest
        {
            return _unitManager.GetTargets(unti);
        }

        private void EngageTarget(IUnit unit, IUnit target)
        {
            _commandController.SetAttackTargetCommand(unit, target);
            _commandController.InjectAttackMoveCommand(unit, Destination);
        }
    }
}

