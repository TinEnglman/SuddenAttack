using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{
    public class MovingBehavior : BehaviorBase
    {
        public Vector2 Destination { get; set; }

        public override void Update(IUnit unit, float dt)
        {
            IMobileUnit mobileUnit = unit as IMobileUnit;
            Vector2 direction = (Destination - unit.Position).normalized;
            unit.Position += direction * mobileUnit.Data.MoveSpeed * dt;

            if ((Destination - unit.Position).sqrMagnitude < 0.01f)
            {
                unit.Position = Destination;
            }
        }

        public override void OnBegin(IUnit unit)
        {
        }

        public override void OnEnd(IUnit unit)
        {
        }

        public override bool IsFinished(IUnit unit)
        {
            return unit.Position == Destination;
        }
    }
}