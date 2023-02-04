using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public List<Sound> sounds;

    private AudioSource source;
    private Dictionary<Sounds, AudioClip> soundsDict = new Dictionary<Sounds, AudioClip>();
    private Dictionary<Sounds, float> volumesDict = new Dictionary<Sounds, float>();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        source = GetComponent<AudioSource>();
        sounds.ForEach(s => {
            volumesDict.Add(s.sound, s.volume);
            soundsDict.Add(s.sound, s.clip);
        });
    }


    public void Play(Sounds sound) {
        // source.clip = soundsDict[sound];
        // source.Play();
        source.PlayOneShot(soundsDict[sound], volumesDict[sound]);
    }
}

[System.Serializable]
public class Sound {
    public Sounds sound;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
}

public enum Sounds {
    Sans,
    Wrong,
    DoorClosed,
    Spear,
    Write,
    Hit,
}
