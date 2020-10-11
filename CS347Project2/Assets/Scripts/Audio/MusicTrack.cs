using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrack : MonoBehaviour
{
    /// <summary>
    /// Enum to keep track of the current state of the music
    /// 
    /// INTRO: The initial state of the music. This is typically the state
    ///     used when the music clip is just starting.
    /// LOOPING: The state for the music when it is looping.
    /// CLOSING: State to indicate playing the closing music when the loop
    ///     reaches it's end
    /// STOPPED: Indicates that this audio clip is not playing currently
    /// </summary>
    public enum MusicTrackState
    {
        INTRO,
        LOOPING,
        CLOSING,
        STOPPED
    }

    /// <summary>
    /// The three different audio clips to use. See above for more information
    /// on how these work.
    /// </summary>
    public AudioClip introClip;
    public AudioClip loopClip;
    public AudioClip closeClip;

    // The Audio Mixer to play through
    public AudioMixerGroup mixer;

    // Configures the clip to start on awake
    public bool playOnAwake;

    // The state of this music clip
    private MusicTrackState clipState = MusicTrackState.STOPPED;

    // The configuration for the different
    // fade in and fade out periods
    public FadeConfiguration fades;

    // Variables used for internal functions
    private AudioSource currentClip;
    private AudioSource nextClip;
    private float currentClipLength;
    private float currentFadeTime;

    /// <summary>
    /// Sets the current state of this clip
    /// </summary>
    /// <param name="state"></param>
    public void SetState(MusicTrackState state)
    {
        clipState = state;
    }

    /// <summary>
    /// Method to quickly start playing this music
    /// </summary>
    public void Play()
    {
        clipState = MusicTrackState.INTRO;
    }

    // Awake is called when this object is initialized
    public void Awake()
    {
        if (playOnAwake)
        {
            Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        ProcessMusicTrackState();
    }

    /// <summary>
    /// Handle all the processing for each of the individual music
    /// states.
    /// </summary>
    private void ProcessMusicTrackState()
    {
        // Find the correct state and then handle processing
        // for that state
        switch(clipState)
        {
            case MusicTrackState.INTRO:
                IntroState();
                break;
            case MusicTrackState.LOOPING:
                LoopingState();
                break;
            case MusicTrackState.CLOSING:
                ClosingState();
                break;
            case MusicTrackState.STOPPED:
                StoppedState();
                break;
        }
    }

    /// <summary>
    /// Handling the Intro Music State
    /// </summary>
    private void IntroState()
    {
        // Reset State
        nextClip = null;
        currentClip = null;

        // Configure and start intro music
        currentClip = GenerateNewSource(introClip);
        currentClip.Play();

        // Update to new state
        clipState = MusicTrackState.LOOPING;
        currentClipLength = currentClip.clip.length;
        currentFadeTime = fades.introFadeOut;
    }
    
    /// <summary>
    /// Handling the looping music state
    /// </summary>
    private void LoopingState()
    {
        float timeRemaining = currentClipLength - currentClip.time;
        if(timeRemaining <= currentFadeTime && nextClip == null)
        {
            nextClip = GenerateNewSource(loopClip);
            nextClip.Play();
        }

        if(timeRemaining <= 0)
        {
            Destroy(currentClip);
            currentClip = nextClip;
            nextClip = null;
            currentClipLength = currentClip.clip.length;
            currentFadeTime = fades.loopFadeOut;
        }
    }

    /// <summary>
    /// Handling the closing music state
    /// </summary>
    private void ClosingState()
    {
        // TODO: Right now this isn't needed, but if we ever need to
        // end an audio clip, we should implement this state.
    }

    /// <summary>
    /// Handling the stopped music state
    /// </summary>
    private void StoppedState()
    {
        // We should do nothing here (for now)
    }

    /// <summary>
    /// Setup and configuration of new Audio Sources
    /// </summary>
    /// <param name="clip">The clip to set this audio source up with</param>
    /// <returns>A fully configured audio source that is ready to play</returns>
    private AudioSource GenerateNewSource(AudioClip clip)
    {
        var newSource = gameObject.AddComponent<AudioSource>() ;
        newSource.clip = clip;
        newSource.outputAudioMixerGroup = mixer;
        return newSource;
    }
}
