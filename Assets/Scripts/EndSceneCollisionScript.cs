using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndSceneCollisionScript : MonoBehaviour
{
    public AudioClip _impactSound;

    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _audioSource.PlayOneShot(_impactSound);
    }
}
