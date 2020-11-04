using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to indicate fading values. This is used with music tracks to
/// allow clips to have their fades handled properly when changing between
/// different audio segments.
/// 
/// NOTE: a fade of 0 indicates instant transition between segments.
/// </summary>
[System.Serializable]
public class MusicFadeConfiguration
{
    public float introFadeIn = 0;
    public float introFadeOut = 0;
    public float loopFadeIn = 0;
    public float loopFadeOut = 0;
    public float closingFadeIn = 0;
    public float closingFadeOut = 0;

}
