using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 伤害陷阱相关
/// </summary>
public class Damageable : MonoBehaviour
{
    public AudioClip audioclip;

    /// <summary>
    /// 停留检测 触发器检测的一种
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        //OnTriggerEnter2D 触碰检测 OnTriggerStay2D 停留检测
        //检测玩家脚本
        newplayerC pc = other.GetComponent<newplayerC>();
        if (pc!=null)
        {
            pc.ChangeH(-1);
        }

    }
}
