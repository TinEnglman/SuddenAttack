using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField]
    GameObject _cameraPrefab = default;

    public override void InstallBindings()
    {
        Container.Bind<Camera>()
            .FromComponentInNewPrefab(_cameraPrefab)
            .AsSingle();
    }
}