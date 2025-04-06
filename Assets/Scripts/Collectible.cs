using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 草莓被玩家碰撞时相关类
/// </summary>
public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Collider2D aa=new BoxCollider2D();
    //    OnTrigger2DEnter(aa);
    //}
    public ParticleSystem collecttionEffect;    //草莓拾取特效
    public AudioClip audioclip;//拾取音效

    /// <summary>
    /// 其他物体碰撞草莓时（注意函数名）
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {

        newplayerC pc = other.GetComponent<newplayerC>();
        if (pc != null)
        {
            if (pc.MycurrH < pc.MymaxH)
            {
                pc.ChangeH(1);
                //创建拾取草莓特效(实例化 物体，位置信息，角度)
                Instantiate(collecttionEffect, transform.position, Quaternion.identity);
                AudioManager.InstansAudioManager.AudiOplay(audioclip);
                Destroy(this.gameObject);
            }
            Debug.Log("玩家碰到了草莓");
        }
    }

}
