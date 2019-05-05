using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip Fantasia;
    public static AudioClip Werq;

    public static void setFantasia() { source.clip = Fantasia; }
    public static void setWerq() { source.clip = Werq; }

    static AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IEnumerator Remote()
    {
        source.Play(0);
        yield return new WaitForSeconds(0);
    }
}
