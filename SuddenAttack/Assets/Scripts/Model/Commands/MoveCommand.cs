using SuddenAttack.Model.Units;
using UnityEngine;

namespace SuddenAttack.Model.Commands
{
    public class MoveCommand : UnitCommandBase
    {
        public Vector2 Destination { get; set; }
        public override void Execute() { }
    }
}