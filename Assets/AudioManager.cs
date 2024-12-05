using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public AudioSource source;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0f, 1f)]
    public float pitch = 1.0f;

    [Range(0f, 1f)]
    public float randomVolume = 0.1f;
    [Range(0f, 1f)]
    public float randomPitch = 0.1f;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void play ()
    {
        source.volume = volume * ( 1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{    
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }else
        {
            instance = null;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("sounds_" + i + "_" + sounds[i].name);
            _go.transform.parent = transform;
            sounds[i].setSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++) { 
            if (sounds[i].name == _name)
            {
                sounds[i].play();
                return;
            }
        }

        Debug.LogWarning("Audio manager : sound not found in list: " + _name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
