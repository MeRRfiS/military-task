using UnityEngine;
using UnityEngine.UI;

namespace Military.Scripts.Interfaces
{
    public interface IMilitaryList
    {
        public void SetSelectedButton(Image buttonImage, GameObject prefab);
        public void DiselectButton();
        public void CreateButtons(Object[] objects);
    }
}