using UnityEngine;
using SuddenAttack.Model.Units;

namespace SuddenAttack.Model.Commands
{
    public class AttackMoveCommand : UnitCommandBase
    {
        public Vector2 Destination { get; set; }
        public override void Execute() { }
    }
}