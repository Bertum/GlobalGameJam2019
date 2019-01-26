using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFX : MonoBehaviour
{
    public AudioClip[] gruntClips;
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

    private AudioClip GetTakeGruntClip()
    {
        if (gruntClips != null)
        {
            int index = Random.Range(0, clipsCount);
            return gruntClips[index];
        }
        else
        {
            return null;
        }
    }

    private void PlayGrunt()
    {
        _audioSource.clip = GetTakeGruntClip();
        _audioSource.Play();
    }

    IEnumerator PlayGruntCourutine()
    {
        while (_keepGrunting)
        {
            yield return new WaitForSeconds(Random.Range(1, GameConfiguration.GRUNT_LIMIT));
            if(_keepGrunting)
                PlayGrunt();
        }        
    }
}
