using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFX : MonoBehaviour
{
    public AudioClip[] takeWheatClips;
    public AudioClip[] takeWoodClips;
    public AudioClip[] takeStoneClips;
    public int clipsCount;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayTakeResource(Material material)
    {
        _audioSource.clip = GetTakeMaterialClip(material);
        _audioSource.Play();
    }

    private AudioClip GetTakeMaterialClip(Material material)
    {
        AudioClip[] clips = null;
        switch (material)
        {
            case Material.Wheat:
                clips = takeWheatClips;
                break;
            case Material.Wood:
                clips = takeWoodClips;
                break;
            case Material.Stone:
                clips = takeStoneClips;
                break;
        }
        if (clips != null)
        {
            int index = Random.Range(0, clipsCount);
            return clips[index];
        }
        else
        {
            return null;
        }
    }
}
