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
        private List<IUnit> _currentTargets;

        public AttackMoveBehavior(CommandController commandController, UnitManager unitManager)
        {
            _commandController = commandController;
            _unitManager = unitManager;
        }

        public override void Update(IUnit unit, float dt)
        {
            RefreshTargets((IMobileUnit)unit);

            if (_currentTargets.Count > 0)
            {
                EngageTarget(unit, _currentTargets[0]);
            }
            else
            {
                base.Update(unit, dt);
            }
        }

        public override void OnBegin(IUnit unit)
        {
            RefreshTargets((IMobileUnit)unit);

            if (_currentTargets.Count > 0)
            {
                EngageTarget(unit, _currentTargets[0]);
            }
        }

        public override void OnEnd(IUnit unit)
        {
            _currentTargets.Clear();
        }

        public override bool IsFinished(IUnit unit)
        {
            return base.IsFinished(unit) || _currentTargets.Count > 0;
        }

        private void RefreshTargets(IMobileUnit unti) // todo; get closest
        {
            _currentTargets = _unitManager.GetTargets(unti);
        }

        private void EngageTarget(IUnit unit, IUnit target)
        {
            _commandController.InjectAttackMoveCommand(unit, Destination);
            _commandController.InjectAttackTargetCommand(unit, target);
        }
    }
}

