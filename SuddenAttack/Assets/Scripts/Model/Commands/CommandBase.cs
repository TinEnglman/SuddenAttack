using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SuddenAttack.Model.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract void Execute();
    }
}