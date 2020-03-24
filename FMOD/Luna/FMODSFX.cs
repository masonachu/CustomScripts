using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSFX : MonoBehaviour

{
    //Allows you to use FMOD's event browser for strings
    [FMODUnity.EventRef]
    public string footstep;
    [FMODUnity.EventRef]
    public string jump;
    [FMODUnity.EventRef]
    public string digDown;
    [FMODUnity.EventRef]
    public string digUp;
    [FMODUnity.EventRef]
    public string bark;
    [FMODUnity.EventRef]
    public string whimper;

    // Used and called from Animation events mostly
    void Step(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(footstep, transform.position);
    }

    void Jump(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(jump, transform.position);
    }
    
    void DigDown(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(digDown, transform.position);
    }
    
    void DigOut(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(digDown, transform.position);
    }
    
    void Bark(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(bark, transform.position);
    }

    void Whimper(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(whimper, transform.position);
    }
}
