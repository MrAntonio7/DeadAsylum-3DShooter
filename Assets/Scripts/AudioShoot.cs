using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShoot : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot() { 

        source.PlayOneShot(clip);
    }
}
