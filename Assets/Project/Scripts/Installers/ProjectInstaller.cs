using Military.Scripts.Interfaces;
using Military.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace Military.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindSaveManager();
            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>()
                             .FromInstance(_sceneLoader)
                             .AsSingle();
        }

        private void BindSaveManager()
        {
            Container.Bind<ISaveManager>()
                             .FromInstance(_saveManager)
                             .AsSingle();
        }
    }
}