using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClipList;

    public void PlaySFX(SFXType sfxType)
    {
        if (GameManager.Instance.IsSoundEnabled)
        {
            _audioSource.PlayOneShot(_audioClipList[(byte)sfxType]);
        }
    }
}
