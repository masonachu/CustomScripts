using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string sfx;

    public void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(sfx, transform.position);
    }
}
