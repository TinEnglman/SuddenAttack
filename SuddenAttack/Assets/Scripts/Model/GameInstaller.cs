using System.Collections;
using System.Collections.Generic;
using SuddenAttack.Model.Commands.Factory;
using UnityEngine;
using Zenject;
using SuddenAttack.Model.Factories;
using SuddenAttack.Controller.FlowController;

namespace SuddenAttack.Model
{

    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _inputHandler = default;

        public override void InstallBindings()
        {
            Container.Bind<IInputManager>().FromComponentInNewPrefab(_inputHandler).AsSingle();

            Container.Bind<UnitBuildingManager>().To<UnitBuildingManager>().AsSingle();
            Container.Bind<GameManager>().To<GameManager>().AsSingle();
            Container.Bind<SelectionManager>().To<SelectionManager>().AsSingle();
            Container.Bind<TankFactory>().To<TankFactory>().AsSingle();
            Container.Bind<SoliderFactory>().To<SoliderFactory>().AsSingle();
            Container.Bind<CommandManager>().To<CommandManager>().AsSingle();
            Container.Bind<CombatManager>().To<CombatManager>().AsSingle();

            Container.Bind<ICommandFactory>().To<CommandFactory>().AsSingle();
        }
    }
}