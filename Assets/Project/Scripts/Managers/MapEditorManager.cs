using Military.Scripts.Interfaces;
using Military.Scripts.Military;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Military.Scripts.Managers
{
    public class MapEditorManager : MonoBehaviour, IMapEditorManager
    {
        private GameObject _prefab;

        private const string PrefabPath = "Prefabs/Military Objects";

        private IMilitaryList _militaryList;

        [Inject]
        private void Constructor(IMilitaryList militaryList)
        {
            _militaryList = militaryList;
        }

        private void Start()
        {
            Object[] objects = Resources.LoadAll(PrefabPath);
            _militaryList.CreateButtons(objects);
        }

        public void SetUpPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }

        public MilitaryObject CreateMilitaryObject(Vector3 position)
        {
            if (_prefab == null) return null;

            return Instantiate(_prefab, new Vector3(position.x, 0, position.z), Quaternion.identity).GetComponent<MilitaryObject>();
        }
    }
}