using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音乐控制、播放类
/// </summary>
public class AudioManager : MonoBehaviour
{
    //音乐和UI等，会在外面很多类里面被调用，为了方便使用，定义为单例模式
    public static AudioManager InstansAudioManager { get; private set; }
    // Start is called before the first frame update

    //
    private AudioSource adioSource;
    void Start()
    {
        InstansAudioManager = this;
        adioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 播放指定音乐
    /// </summary>
    /// <param name="audioClip"></param>
    public void AudiOplay(AudioClip audioClip)
    {
        adioSource.PlayOneShot(audioClip);
    }
}
