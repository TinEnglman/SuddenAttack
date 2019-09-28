using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SuddenAttack.Model.Factories;

namespace SuddenAttack.Model
{

    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _inputHandler = default;

        public override void InstallBindings()
        {
            Container.Bind<GameManager>()
                .To<GameManager>()
                .AsSingle();

            Container.Bind<CombatManager>()
                .To<CombatManager>()
                .AsSingle();

            Container.Bind<TankFactory>()
                .To<TankFactory>()
                .AsSingle();

            Container.Bind<SoliderFactory>()
                .To<SoliderFactory>()
                .AsSingle();

            Container.Bind<IInputManager>()
                .FromComponentInNewPrefab(_inputHandler)
                .AsSingle();
        }
    }
}