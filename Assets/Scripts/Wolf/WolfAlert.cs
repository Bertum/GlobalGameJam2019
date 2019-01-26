using UnityEngine;

public class WolfAlert : MonoBehaviour
{
    public AudioClip alertClip;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = alertClip;
    }

    void Start()
    {
        audioSource.Play();
    }
}
