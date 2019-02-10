using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public IEnumerator PlayBellSound(int nbTime)
    {
        int timeTemp = nbTime;

        AudioClip clip = audio.clip;

        while (timeTemp > 0)
        {
            audio.Play();

            timeTemp--;
            yield return new WaitForSeconds(clip.length);
        }

    }
}
