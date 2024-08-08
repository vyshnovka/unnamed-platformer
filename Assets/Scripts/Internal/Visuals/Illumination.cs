using Internal.Time;
using UnityEngine;

namespace Internal.Visuals
{
    public abstract class Illumination : MonoBehaviour
    {
        void OnEnable() => TimeManager.Instance.OnTimeUpdated += UpdateTimeVisual;

        void OnDisable() => TimeManager.Instance.OnTimeUpdated -= UpdateTimeVisual;

        public abstract void UpdateTimeVisual(float timePassed);
    }
}