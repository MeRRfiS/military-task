using Military.Scripts.CameraLogic;
using Military.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Military.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraMovement _cameraMovement;

        public override void InstallBindings()
        {
            BindCameraMovement();
        }

        private void BindCameraMovement()
        {
            Container.Bind<ICameraMovement>()
                                 .FromInstance(_cameraMovement)
                                 .AsSingle();
        }
    }
}