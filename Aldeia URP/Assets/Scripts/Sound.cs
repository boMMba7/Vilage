using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    private bool _loop;    

    [SerializeField]
    private bool _playOnAwake;

    [SerializeField]
    [Range(0f, 1f)]
    private float _volume;

    [SerializeField]
    [Range(0.1f, 3f)]
    private float _pitch;
    
    private AudioSource _source;

    public string GetName()
    {
        return _name;
    }

    public void SetAudioSource(AudioSource s)
    {
        _source = s;
    }

    public AudioSource GetAudioSource()
    {
        return _source;
    }

    public void ConfgClip()
    {
        _source.clip = _clip;
    }

    public void ConfgLoop()
    {
        _source.loop = _loop;
    }

    public void ConfgPlayOnAwake()
    {
        _source.playOnAwake = _playOnAwake;
    }

    public void ConfgVolume()
    {
        _source.volume = _volume;
    }

    public void ConfgPitch()
    {
        _source.pitch = _pitch;
    }

    
}
