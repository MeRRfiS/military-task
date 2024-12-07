using UnityEngine;

namespace Military.Scripts.Military
{
    public class MilitaryRenderer : MonoBehaviour
    {
        private MeshRenderer[] _renderers;

        private void Start()
        {
            _renderers = transform.GetComponentsInChildren<MeshRenderer>();
        }

        public void ChangeColor(Color color)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = color;
            }
        }
    }
}