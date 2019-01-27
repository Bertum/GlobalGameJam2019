using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFX : MonoBehaviour
{
    public AudioClip[] gruntClips;
    public AudioClip[] deathClips;
    public AudioClip[] noMaterialClips;
    public int clipsCount;

    private AudioSource _audioSource;
    private bool _keepGrunting;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _keepGrunting = false;
        InitPlayGrunt();
    }

    public void InitPlayGrunt()
    {
        if (!_keepGrunting)
        {
            _keepGrunting = true;
            StartCoroutine(PlayGruntCourutine());
        }        
    }

    public void StopPlayGrunt()
    {
        if (_keepGrunting)
        {
            _keepGrunting = false;
            _audioSource.Stop();
        }        
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
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

    public void PlayDeath()
    {
        StopPlayGrunt();
        _audioSource.clip = GetRandomClip(deathClips);
        _audioSource.Play();        
    }

    public void PlayNoMaterial()
    {
        _audioSource.clip = GetRandomClip(noMaterialClips);
        _audioSource.Play();
    }

    private void PlayGrunt()
    {
        _audioSource.clip = GetRandomClip(gruntClips);
        _audioSource.Play();
    }

    IEnumerator PlayGruntCourutine()
    {
        while (_keepGrunting)
        {
            yield return new WaitForSeconds(Random.Range(GameConfiguration.GRUNT_LIMIT_INF, GameConfiguration.GRUNT_LIMIT_SUP));
            if(_keepGrunting && !_audioSource.isPlaying)
                PlayGrunt();
        }        
    }
}
