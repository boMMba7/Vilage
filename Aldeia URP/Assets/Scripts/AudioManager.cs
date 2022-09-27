using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] _sounds;

    public static AudioManager instance;

    void Awake()
    {
        //para ter certesa que so ha 1 AudioManager quando se muda de cena
        //assim a musica nao comecar de novo
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in _sounds)
        {
            s.SetAudioSource( gameObject.AddComponent<AudioSource>());

            // iguala os atributos do novo AudioSource aos atributos preconfigurados na clase Sounds
            s.ConfgClip();            
            s.ConfgLoop();
            s.ConfgPlayOnAwake();
            s.ConfgVolume();
            s.ConfgPitch();
        }   
    }

    private void Start()
    {
        Play("GameMusic");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.GetName() == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.GetAudioSource().Play();
    }

    //public void FootStep()
    //{
    //    Sound s = _sounds[2];
    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound " + name + " not found!");
    //        return;
    //    }
    //    if (!s._source.isPlaying)
    //    {
    //        s._volume = Random.Range(0.6f, 0.8f);
    //        s._pitch = Random.Range(2.5f, 3f);
    //        s._source.Play();
    //    }
    //}
}
