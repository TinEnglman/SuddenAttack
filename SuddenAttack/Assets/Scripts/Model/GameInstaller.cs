using SuddenAttack.Model.Commands.Factory;
using UnityEngine;
using Zenject;
using SuddenAttack.Model.Factories;
using SuddenAttack.Controller.FlowController;
using SuddenAttack.Model.Behavior;

namespace SuddenAttack.Model
{

    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _inputHandler = default;

        public override void InstallBindings()
        {
            Container.Bind<IInputManager>().FromComponentInNewPrefab(_inputHandler).AsSingle();

            Container.Bind<BuildingFactoryManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitFactoryManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitCreationManager>().To<UnitCreationManager>().AsSingle();
            Container.Bind<UnitManager>().To<UnitManager>().AsSingle();
            Container.Bind<SelectionManager>().To<SelectionManager>().AsSingle();
            Container.Bind<CommandManager>().To<CommandManager>().AsSingle();
            Container.Bind<CombatManager>().To<CombatManager>().AsSingle();
            Container.Bind<BehaviorManager>().To<BehaviorManager>().AsSingle();
          
            Container.Bind<CommandFactory>().To<CommandFactory>().AsSingle();

            Container.Bind<LocalPlayerCommandController>().To<LocalPlayerCommandController>().AsSingle();
            Container.Bind<CommandController>().To<CommandController>().AsSingle();
        }
    }
}