using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Régle le timeScale avec une valeur donnée
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;

        // Ajuste fixedDeltaTime pour la physique 3D et 2D
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  // Physique 3D (général)
        Physics2D.simulationMode = SimulationMode2D.Script; // Active la simulation manuelle
    }

    // Activer le slow motion en transition
    public void SlowMotion(float targetTimeScale, float duration)
    {
        StartCoroutine(SlowMotionCoroutine(targetTimeScale, duration));
    }

    // Désactiver le slow motion et revenir à la vitesse normale
    public void ResetTimeScale(float duration)
    {
        StartCoroutine(ResetTimeScaleCoroutine(duration));
    }

    // Coroutine pour faire un slow motion fluide
    private IEnumerator SlowMotionCoroutine(float targetTimeScale, float transitionDuration)
    {
        float currentScale = Time.timeScale;
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float newScale = Mathf.Lerp(currentScale, targetTimeScale, t / transitionDuration);
            SetTimeScale(newScale);
            yield return null;
        }

        SetTimeScale(targetTimeScale); // Assure qu'on atteigne bien la cible
    }

    // Coroutine pour revenir à la vitesse normale
    private IEnumerator ResetTimeScaleCoroutine(float transitionDuration)
    {
        float currentScale = Time.timeScale;
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float newScale = Mathf.Lerp(currentScale, 1.0f, t / transitionDuration);
            SetTimeScale(newScale);
            yield return null;
        }

        SetTimeScale(1.0f); // Assure qu'on retourne à la vitesse normale
    }
}
