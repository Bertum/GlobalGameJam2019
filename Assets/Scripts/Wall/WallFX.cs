using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFX : MonoBehaviour
{
    public AudioClip[] repairWheatClips;
    public AudioClip[] repairWoodClips;
    public AudioClip[] repairStoneClips;
    public int clipsCount;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRepair(Material material)
    {
        _audioSource.clip = GetRepairMaterialClip(material);
        _audioSource.Play();
    }

    private AudioClip GetRepairMaterialClip(Material material)
    {
        AudioClip[] clips = null;
        switch (material)
        {
            case Material.Wheat:
                clips = repairWheatClips;
                break;
            case Material.Wood:
                clips = repairWoodClips;
                break;
            case Material.Stone:
                clips = repairStoneClips;
                break;
        }
        if(clips != null)
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
