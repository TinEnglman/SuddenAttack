using UnityEngine;
using Zenject;

namespace SuddenAttack.Model
{
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
}