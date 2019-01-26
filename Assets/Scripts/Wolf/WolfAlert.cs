using System.Collections.Generic;
using UnityEngine;

public class WolfAlert : MonoBehaviour
{
    List<AudioClip> alertClips;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        var rnd = Random.Range(0, alertClips.Count - 1);
        audioSource.clip = alertClips[rnd];
        audioSource.Play();
    }
}
