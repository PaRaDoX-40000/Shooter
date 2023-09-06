using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RifleSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _audioClip1;
    [SerializeField] private AudioClip _audioClip2;

    [SerializeField] private AudioSource audioSource;

    private void PlayClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void Reload()
    {
        StartCoroutine(ReloadCor());
    }

    private IEnumerator ReloadCor()
    {
        yield return new WaitForSeconds(0.30f);
        PlayClip(_audioClip1);
        //yield return new WaitForSeconds(1.15f - 0.35f);
        //PlayClip(_audioClip2);
    }



}
