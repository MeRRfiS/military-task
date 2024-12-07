using Military.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Military.Scripts.CameraLogic
{
    public class CameraMovement : MonoBehaviour, ICameraMovement
    {
        private const float Speed = 0.01f;

        public event Func<bool> OnBlock;

        public void Moving(Vector2 dragDelta)
        {
            if (IsBlock()) return;
            if(dragDelta == Vector2.zero) return;

            Vector3 move = new Vector3(-dragDelta.x, 0, -dragDelta.y) * Speed;
            transform.Translate(move, Space.World);
        }

        private bool IsBlock()
        {
            if(OnBlock == null) return false;

            return (bool)OnBlock?.Invoke();
        }

        private void OnApplicationQuit()
        {
            OnBlock = null;
        }
    }
}