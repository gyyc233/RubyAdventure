using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 控制子弹移动、碰撞
/// </summary>
public class BulletControl : MonoBehaviour
{
    public Rigidbody2D rigid;
    public AudioClip oneclip;

    // Start is called before the first frame update


    void Awake()
    {
        //获取子弹刚体
        rigid = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 控制子弹的移动
    /// </summary>
    public void Move(Vector2 movederection,float movefroce)
    {
        rigid.AddForce(movederection*movefroce);
    }
    /// <summary>
    /// 碰撞检测
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);
        EnemyRobot er = other.gameObject.GetComponent<EnemyRobot>();
        if (er!=null)
        {
            er.Fix();
        }
        AudioManager.InstansAudioManager.AudiOplay(oneclip);
        Destroy(this.gameObject);
    }
}
