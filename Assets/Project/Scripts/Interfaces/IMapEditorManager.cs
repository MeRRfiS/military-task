using Military.Scripts.Military;
using UnityEngine;

namespace Military.Scripts.Interfaces
{
    public interface IMapEditorManager
    {
        public void SetUpPrefab(GameObject prefab);
        public MilitaryObject CreateMilitaryObject(Vector3 position);
    }
}