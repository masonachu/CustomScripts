using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string ambience;
    [FMODUnity.EventRef]
    public string music;

    private FMOD.Studio.EventInstance amb;
    private FMOD.Studio.EventInstance mus;

    void Awake()
    {
        amb = FMODUnity.RuntimeManager.CreateInstance(ambience);
        amb.start();
    }

    void OnDestroy()
    {
        amb.release();
    }
}
