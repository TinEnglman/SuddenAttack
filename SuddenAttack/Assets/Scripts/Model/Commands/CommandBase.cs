using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandBase : ICommand
{
    public abstract void Execute();
}
