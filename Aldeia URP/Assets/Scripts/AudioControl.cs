using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioClip _audioWalk;
    public AudioClip _audioDie;
    public AudioClip _audioAttack;
    public AudioClip _audioAttack2;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioAttack()
    {        
        if (_audioAttack == null)
        {
            Debug.LogWarning("Sound Attack not found! on " + gameObject.name );
            return;
        }
        _audioSource.PlayOneShot(_audioAttack);
    }

    public void PlayAudioAttack2()
    {
        if (_audioAttack == null)
        {
            Debug.LogWarning("Sound Attack not found! on " + gameObject.name);
            return;
        }
        _audioSource.PlayOneShot(_audioAttack2);
    }

    public void PlayAudioDie()
    {
        if(_audioAttack == null)
        {
            Debug.LogWarning("Sound Die not found! on " + gameObject.name);
            return;
        }
        _audioSource.PlayOneShot(_audioDie);
    }

    public void PlayAudioWalk()
    {
        if (_audioAttack == null)
        {
            Debug.LogWarning("Sound Walk not found! on " + gameObject.name);
            return;
        }
        if (!_audioSource.isPlaying)
        {
            _audioSource.volume = Random.Range(0.6f, 0.8f);
            _audioSource.PlayOneShot(_audioWalk);
        }
    }

    public void PlayAudioWalk(float speed)
    {
        if (_audioAttack == null)
        {
            Debug.LogWarning("Sound Walk not found! on " + gameObject.name);
            return;
        }
        if (!_audioSource.isPlaying)
        {
            _audioSource.volume = Random.Range(0.6f, 0.8f);
            _audioSource.pitch = speed;

            _audioSource.PlayOneShot(_audioWalk);
        }
    }
}
