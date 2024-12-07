using Military.Scripts.Interfaces;
using Military.Scripts.Military;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Military.Scripts.Inputs
{
    public class EditorInput : MonoBehaviour
    {
        private EditorInputAction _input;
        private MilitaryObject _militaryObj;

        private IMapEditorManager _mapEditorManager;

        [Inject]
        private void Constructor(IMapEditorManager mapEditorManager)
        {
            _mapEditorManager = mapEditorManager;
        }

        private void Start()
        {
            _input = new EditorInputAction();

            _input.Editor.Placing.performed += PointOnScreen;
            _input.Editor.Placing.canceled += x => PlacedMilitaryObject();

            _input.Enable();
        }

        private void PointOnScreen(InputAction.CallbackContext context)
        {
            PointerEventData eventData = new(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(eventData, results);
            if (results.Count > 0)
                return;

            Vector2 inputPosition = GetInputPosition();

            if (inputPosition != Vector2.zero)
            {
                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    _militaryObj = _mapEditorManager.CreateMilitaryObject(hit.point);
                    if (_militaryObj != null) _militaryObj.ApplyPlacing();
                }
            }
        }

        private static Vector2 GetInputPosition()
        {
            Vector2 inputPosition = Vector2.zero;
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            {
                inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            }

            return inputPosition;
        }

        private void PlacedMilitaryObject()
        {
            if (_militaryObj == null) return;

            _militaryObj.StopPlacing();
            _militaryObj = null;
        }

        private void OnDisable()
        {
            _input.Editor.Placing.performed -= PointOnScreen;
            _input.Editor.Placing.canceled -= x => PlacedMilitaryObject();

            _input.Disable();
        }
    }
}