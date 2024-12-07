using Military.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Military.Scripts.Inputs
{
    public class CameraInput : MonoBehaviour
    {
        private CameraInputAction _input;

        private ICameraMovement _cameraMovement;

        [Inject]
        private void Constructor(ICameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
        }

        private void Start()
        {
            _input = new CameraInputAction();

            _input.Camera.Moving.performed += x => ApplyCameraMovement(x.ReadValue<Vector2>());
            _input.Camera.Moving.canceled += x => ApplyCameraMovement(Vector2.zero);

            _input.Enable();
        }

        private void ApplyCameraMovement(Vector2 dragDelta)
        {
            _cameraMovement.Moving(dragDelta);
        }

        private void OnDisable()
        {
            _input.Camera.Moving.performed -= x => ApplyCameraMovement(x.ReadValue<Vector2>());
            _input.Camera.Moving.canceled -= x => ApplyCameraMovement(Vector2.zero);

            _input.Disable();
        }
    }
}