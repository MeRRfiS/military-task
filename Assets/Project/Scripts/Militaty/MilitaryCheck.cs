using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Military.Scripts.Military
{
    public class MilitaryCheck : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Color> OnShowStatus;

        private const string MilitaryTag = "Military";

        public bool IsCanPlace { get; private set; }

        private void Start()
        {
            IsCanPlace = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag != MilitaryTag) return;

            IsCanPlace = false;
            OnShowStatus?.Invoke(Color.red);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != MilitaryTag) return;

            IsCanPlace = true;
            OnShowStatus?.Invoke(Color.white);
        }
    }
}