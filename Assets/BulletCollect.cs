using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹袋管理
/// </summary>
public class BulletCollect : MonoBehaviour
{
    public int bullet_count=10;//当前包里含有的子弹数量
   // public ParticleSystem collecttionEffect;
   // public AudioClip audioclip;//拾取音效


    /// <summary>
    /// 其他物体碰撞子弹袋时（注意函数名）
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
         newplayerC pc = other.GetComponent<newplayerC>();
        if (pc != null)
        {
            if (pc.my_curr_bullet_count < pc.my_max_bullet_count)
            {
                pc.ChengeBulletCount(bullet_count);
                //创建拾取草莓特效(实例化 物体，位置信息，角度)
              //  Instantiate(collecttionEffect, transform.position, Quaternion.identity);
              //  AudioManager.InstansAudioManager.AudiOplay(audioclip);
                Destroy(this.gameObject);
            }
            Debug.Log("玩家碰到了子弹袋");
        }
    }

}
