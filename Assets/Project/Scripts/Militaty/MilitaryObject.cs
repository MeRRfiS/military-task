using Military.Scripts.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Military.Scripts.Military
{
    public class MilitaryObject : MonoBehaviour
    {
        [SerializeField] private MilitaryCheck _check;

        [Header("Information")]
        [SerializeField] private string _name;

        private bool _isMoving = true;

        public bool IsPlaced { private get; set; }

        private void Update()
        {
            ApplyPlacing();
        }

        public void ApplyPlacing()
        {
            if (IsPlaced) return;
            if (!_isMoving) return;

            var inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }

        public void StopPlacing()
        {
            if (_check.IsCanPlace)
            {
                _isMoving = false;
                IsPlaced = true;
                Destroy(_check);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveObject()
        {
            MapData.Instance.AddMilitary(new MilitaryData() { Name = _name, Position = transform.position });
        }
    }
}