using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string music = "event:/Music/Music 2";
    public bool isPlaying = false;

    FMOD.Studio.EventInstance musicEv;

    // Start is called before the first frame update
    void Start()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();
    }

    //Player selects character
    public void GameStartedMusic()
    {
        musicEv.setParameterByName("gameStart", 1f);
    }

    //Player is half health
    public void IsHalfHealth()
    {
        musicEv.setParameterByName("belowHalfHealth", 1f);
    }

    //Player is dead
    public void IsDeadMusic()
    {
        musicEv.setParameterByName("isDead", 1f);
        musicEv.setParameterByName("endMusic", 1f);
    }
}
