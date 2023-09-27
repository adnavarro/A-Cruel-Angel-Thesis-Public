using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Audio> sounds;

    void Awake()
    {
        foreach (var VARIABLE in sounds)
        {
            VARIABLE.source = gameObject.AddComponent<AudioSource>();
            if (VARIABLE.stop == 0)
                VARIABLE.source.clip = VARIABLE.clip;
            else
                VARIABLE.source.clip = MakeSubclip(VARIABLE.clip, VARIABLE.start,VARIABLE.stop);
            VARIABLE.source.volume = VARIABLE.volume;
            VARIABLE.source.pitch = VARIABLE.pitch;
            VARIABLE.source.loop = VARIABLE.loop;
            if(VARIABLE.autoplay)
                Play(VARIABLE.name);
        }

    }   
    
    public void Play(string name)
    {
        Audio s = sounds.Find(x => x.name == name);
        s.source.Play();
    }
    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }
}
