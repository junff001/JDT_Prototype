using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioBox", menuName ="ScriptableObject/AudioBox")]
public class AudioBoxSO : ScriptableObject
{
    [Header("Player")]
    public AudioClip p_shot_gun;
    public AudioClip p_shot_gun_reload;
}
