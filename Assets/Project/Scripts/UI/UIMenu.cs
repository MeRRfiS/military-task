using Military.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Military.Scripts.UI
{
    public class UIMenu : MonoBehaviour
    {
        private ICameraMovement _cameraMovement;

        private bool Bloker() => true;

        [Inject]
        private void Constructor(ICameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
        }

        protected void BlockCameraMoving()
        {
            _cameraMovement.OnBlock += Bloker;
        }

        protected void UnblockCameraMoving()
        {
            _cameraMovement.OnBlock -= Bloker;
        }
    }
}