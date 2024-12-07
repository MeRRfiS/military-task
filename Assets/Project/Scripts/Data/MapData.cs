using Military.Scripts.Military;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Military.Scripts.Data
{
    [Serializable]
    public class MapData: SaveData
    {
        public static MapData Instance;
        public List<MilitaryData> MilitariesData = new List<MilitaryData>();

        private const string PrefabPath = "Prefabs/Military Objects/";

        public MapData()
        {
            Instance = this;
        }

        public void ClearList()
        {
            MilitariesData.Clear();
        }

        public void AddMilitary(MilitaryData data)
        {
            MilitariesData.Add(data);
        }

        public void CreateMilitaryObjects()
        {
            if (SceneManager.GetActiveScene().name == "Main Scene") return;

            foreach (var military in MilitariesData)
            {
                var obj = MonoBehaviour.Instantiate(Resources.Load<GameObject>(PrefabPath + military.Name));
                obj.GetComponent<MilitaryObject>().IsPlaced = true;
                obj.transform.position = military.Position;
                MonoBehaviour.Destroy(obj.GetComponent<MilitaryCheck>());
            }
        }
    }
}