using Military.Scripts.CameraLogic;
using Military.Scripts.Interfaces;
using Military.Scripts.Managers;
using Military.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Military.Scripts.Installers
{
    public class MapEditorInstaller : MonoInstaller
    {
        [SerializeField] private CameraMovement _cameraMovement;

        [Header("Managers")]
        [SerializeField] private MapEditorManager _mapEditorManager;

        [Header("UI")]
        [SerializeField] private MilitaryList _militaryList;

        public override void InstallBindings()
        {
            BindMapEditorManager();
            BindCameraMovement();
            BindMilitaryList();
        }

        private void BindMilitaryList()
        {
            Container.Bind<IMilitaryList>()
                             .FromInstance(_militaryList)
                             .AsSingle();
        }

        private void BindCameraMovement()
        {
            Container.Bind<ICameraMovement>()
                     .FromInstance(_cameraMovement)
                     .AsSingle();
        }

        private void BindMapEditorManager()
        {
            Container.Bind<IMapEditorManager>()
                             .FromInstance(_mapEditorManager)
                             .AsSingle();
        }
    }
}