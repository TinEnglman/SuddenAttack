using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Model.Behavior
{
    public class MovingBehavior : BehaviorBase
    {
        private const float DISTANCE_TRESHOLD = 0.001f;

        public Vector2 Destination { get; set; }

        public override void Update(IUnit unit, float dt)
        {
            IMobileUnit mobileUnit = unit as IMobileUnit;
            Vector2 direction = (Destination - mobileUnit.Position).normalized;
            mobileUnit.Position += direction * mobileUnit.Data.MoveSpeed * dt;

            if ((Destination - mobileUnit.Position).sqrMagnitude < DISTANCE_TRESHOLD)
            {
                mobileUnit.Position = Destination;
            }
        }

        public override void OnBegin(IUnit unit)
        {
            unit.OnMove(Destination);
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