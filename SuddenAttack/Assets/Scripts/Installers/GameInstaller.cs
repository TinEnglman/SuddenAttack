using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
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
    }
}
