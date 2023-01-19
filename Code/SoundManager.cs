using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;
    
    [SerializeField] private AudioSource musicSoundClip;
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
    [SerializeField] private AudioClip music3;
    [SerializeField] private AudioClip music4;
    [SerializeField] private GameObject soundTemplate;
    [SerializeField] private AudioClip ui1;
    [SerializeField] private AudioClip ui2;
    [SerializeField] private AudioClip ui3;
    [SerializeField] private AudioClip shock1;
    [SerializeField] private AudioClip shock2;
    [SerializeField] private AudioClip shock3;
    [SerializeField] private AudioClip shock4;
    [SerializeField] private AudioClip shock5;
    [SerializeField] private AudioClip shock6;
    [SerializeField] private AudioClip shock7;
    [SerializeField] private AudioClip shock8;
    [SerializeField] private AudioClip hit1;
    [SerializeField] private AudioClip hit2;
    [SerializeField] private AudioClip enHit1;
    [SerializeField] private AudioClip enHit2;
    [SerializeField] private AudioClip enHit3;
    [SerializeField] private AudioClip enHit4;
    [SerializeField] private AudioClip enHit5;
    [SerializeField] private AudioClip enHit6;
    [SerializeField] private AudioClip jump1;
    [SerializeField] private AudioClip cancel1;

    [SerializeField] private float globalVolume = 0.6f;
    [SerializeField] private float musicVolume = 0.6f;
    [SerializeField] private float uiVolume = 1f;
    [SerializeField] private float shockVolume = 1f;
    [SerializeField] private float hitVolume = 1f;
    [SerializeField] private float enHitVolume = 1f;
    [SerializeField] private float jumpVolume = 1f;
    [SerializeField] private float cancelVolume = 1f;


    void Awake() {
        if (I != null) {
            Destroy(this.gameObject);
        } else {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void spawnSound(AudioClip clip, float volume, bool randomize = false) {
        GameObject result = GameObject.Instantiate(soundTemplate);
        AudioSource source = result.GetComponent<AudioSource>();
        source.clip = clip;
        if (randomize) {
            source.pitch = Random.Range(0.8f,1.2f);
        }
        source.volume = globalVolume * volume;
        source.Play();
        result.transform.parent = transform;
    }

    private void playMusic(AudioClip clip, float volume) {
        musicSoundClip.clip = clip;
        musicSoundClip.volume = globalVolume * volume;
        musicSoundClip.Play();
    }

    public void Spawn_ui1() { spawnSound(ui1, uiVolume); }
    public void Spawn_music1() { playMusic(music1, musicVolume); }
    public void Spawn_music2() { playMusic(music2, musicVolume); }
    public void Spawn_music3() { playMusic(music3, musicVolume); }
    public void Spawn_music4() { playMusic(music4, musicVolume); }
    public void Spawn_ui2() { spawnSound(ui2, uiVolume); }
    public void Spawn_ui3() { spawnSound(ui3, uiVolume); }
    public void Spawn_shock1() { spawnSound(shock1, shockVolume); }
    public void Spawn_shock2() { spawnSound(shock2, shockVolume); }
    public void Spawn_shock3() { spawnSound(shock3, shockVolume); }
    public void Spawn_shock4() { spawnSound(shock4, shockVolume); }
    public void Spawn_shock5() { spawnSound(shock5, shockVolume); }
    public void Spawn_shock6() { spawnSound(shock6, shockVolume); }
    public void Spawn_shock7() { spawnSound(shock7, shockVolume); }
    public void Spawn_shock8() { spawnSound(shock8, shockVolume); }
    public void Spawn_hit1() { spawnSound(hit1, hitVolume, true); }
    public void Spawn_hit2() { spawnSound(hit2, hitVolume, true); }
    public void Spawn_jump1() { spawnSound(jump1, jumpVolume); }
    public void Spawn_cancel1() { spawnSound(cancel1, cancelVolume); }
    public void Spawn_ran_enHit() {
        switch(Random.Range(1,7)) 
        {
            case 1: spawnSound(enHit1, enHitVolume); break;
            case 2: spawnSound(enHit2, enHitVolume); break;
            case 3: spawnSound(enHit3, enHitVolume); break;
            case 4: spawnSound(enHit4, enHitVolume); break;
            case 5: spawnSound(enHit5, enHitVolume); break;
            case 6: spawnSound(enHit6, enHitVolume); break;
            default: spawnSound(enHit6, enHitVolume); break;
        }
    }
}
