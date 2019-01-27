using System.Collections;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public AudioSource layer01, layer02, layer03;
    public float fadeTime;

    public float bpm, beats, bars;

    private AudioSource currentSource, nextSource;

    private float initialTime;
    private float sectionTime;
    private float scheduleTime;
    private bool isChangingLayer = false;


    // Start is called before the first frame update
    void Start()
    {
        layer01.volume = 1f;
        layer02.volume = 0f;
        layer03.volume = 0f;

        currentSource = layer01;

        initialTime = Time.time;
        sectionTime = 60f * bars * beats / bpm;
    }

    public void CheckWolfs(int counter)
    {
        if (!isChangingLayer)
        {
            if (counter >= 0 && counter <= 10 && currentSource != layer01)
            {
                StartCoroutine(ScheduleFade(layer01));
            }
            else if (counter > 10 && counter <= 20 && currentSource != layer02)
            {
                StartCoroutine(ScheduleFade(layer02));
            }
            else if (counter > 20 && counter <= 30 && currentSource != layer03)
            {
                StartCoroutine(ScheduleFade(layer03));
            }
        }
    }

    IEnumerator ScheduleFade(AudioSource layer)
    {
        isChangingLayer = true;
        float timeSinceStart = Time.time;
        float currentTime = timeSinceStart - initialTime;
        float timeToNextPoint = sectionTime - (currentTime % sectionTime);

        if (timeToNextPoint < fadeTime)
        {
            timeToNextPoint += sectionTime;
        }

        scheduleTime = timeSinceStart + timeToNextPoint - fadeTime;

        while (Time.time < scheduleTime)
        {
            yield return null;
        }

        StartCoroutine(CrossFade(layer));
        print("new layer!");
    }


    IEnumerator CrossFade(AudioSource layer)
    {
        nextSource = layer;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float normTime = elapsedTime / fadeTime;

            currentSource.volume = Mathf.Sqrt(1f - normTime);
            nextSource.volume = Mathf.Sqrt(normTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        currentSource.volume = 0f;
        nextSource.volume = 1f;

        currentSource = nextSource;
        isChangingLayer = false;
    }
}
