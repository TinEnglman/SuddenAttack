using UnityEngine;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class PatrolCommand : UnitCommandBase
    {
        public Vector2 Destination { get; set; }
        public Vector2 Start { get; set; }

        public override void Execute() { }
    }
}