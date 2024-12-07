using Military.Scripts.Data;
using Military.Scripts.Interfaces;
using Military.Scripts.Military;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Military.Scripts.Managers
{
    public class SaveManager : MonoBehaviour, ISaveManager
    {
        private MapData _mapData;

        private const string FileType = ".dat";

        private void Start()
        {
            _mapData = new MapData();

            SceneManager.sceneLoaded += CreareObjectToScene;
        }

        public void Save(string mapName)
        {
            _mapData.ClearList();

            var militaries = GameObject.FindObjectsByType<MilitaryObject>(FindObjectsSortMode.None);
            foreach (var military in militaries)
            {
                military.SaveObject();
            }

            _mapData.SaveToFile(mapName + FileType, _mapData);
        }

        public void Load(string mapName)
        {
            _mapData.LoadDataFromFile(mapName + FileType, _mapData);
        }

        private void CreareObjectToScene(Scene scene, LoadSceneMode mode)
        {
            _mapData.CreateMilitaryObjects();
        }
    }
}