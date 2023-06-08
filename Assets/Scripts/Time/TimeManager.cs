using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private RawImage viewImage;

    [SerializeField]
    private Gradient timeOfDay;

#if (UNITY_EDITOR)
    [SerializeField]
    [Range(0f, 1f), Tooltip("Value used for debugging only.")]
    private float timePassed;

    [SerializeField]
    private bool testInEditor = false;
#endif

    void Start() => StartCoroutine(UpdateTime());

    void OnDestroy() => StopAllCoroutines();

    private IEnumerator UpdateTime()
    {
        while (true)
        {
#if (UNITY_EDITOR)
            if (testInEditor)
                viewImage.color = timeOfDay.Evaluate(timePassed);
            else
                viewImage.color = timeOfDay.Evaluate(TimePassed());

            yield return new WaitForEndOfFrame();
#else
            viewImage.color = timeOfDay.Evaluate(TimePassed());
            yield return new WaitForSecondsRealtime(300);
#endif
        }
    }

    /// <summary>Calculates time passed since midnight in seconds.</summary>
    /// <returns>Returns time ratio (from 0 to 1).</returns>
    private float TimePassed()
    {
        DateTime time = DateTime.Now;

        float secondsPassed = time.Hour * 60 * 60 + time.Minute * 60 + time.Second;
        float secondsFull = 24 * 60 * 60;

        float timeRatio = secondsPassed / secondsFull;

        return timeRatio;
    }
}
