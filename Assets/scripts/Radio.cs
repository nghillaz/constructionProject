using UnityEngine;
using System.Collections;

// Loops background music based on button clicks
// 1->2 | 2->3 | 3->4 | 4->muted | muted->1

// Audio Source and the script itself on the button
// Not too sure where to put the button itself since I don't have a idea of how the layout is at the moment
// Currently sits in the lower right corner using the same dimensions of the GUI button as normal

public class Radio : MonoBehaviour {
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public AudioSource current;

    void Start()
    {
    }

    public void radioPress()
    {
        current = gameObject.GetComponent<AudioSource>();

        //plays 2 if 1 is playing at the moment
        if (current.clip == music1)
        {
            current.clip = music2;
            current.Play();
        }

        //plays 3 if 2 is playing at the moment
        else if(current.clip == music2)
        {
            current.clip = music3;
            current.Play();
        }

        //plays 4 if 3 is playing at the moment
        else if(current.clip == music3)
        {
            current.clip = music4;
            current.Play();
        }

        //mutes if music is not muted and 4 is playing at the moment
        else if(current.clip == music4 && current.mute == false)
        {
            current.mute = true;
            Debug.Log(current.mute);
        }

        //unmutes and plays 1 if music is muted
        else if (current.mute == true)
        {
            current.mute = false;
            current.clip = music1;
            current.Play();
        }
    }
}
