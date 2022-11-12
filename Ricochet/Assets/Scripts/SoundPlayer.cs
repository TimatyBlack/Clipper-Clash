using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource sound;

    private bool isReady = false;

    private void Start()
    {
        StartCoroutine(soundDelay());
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if(isReady == true)
        sound.Play();
    }

    IEnumerator soundDelay()
    {
        yield return new WaitForSeconds(0.1f);
        isReady = true;
    }
}
