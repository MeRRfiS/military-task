using System;
using UnityEngine;

namespace Military.Scripts.Interfaces
{
    public interface ICameraMovement
    {
        public event Func<bool> OnBlock;

        public void Moving(Vector2 dragDelta);
    }
}