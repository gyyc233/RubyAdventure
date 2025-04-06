using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 敌人控制相关
/// </summary>
public class EnemyRobot : MonoBehaviour
{
    public float changeDirectionTime = 2f; //改变方向的时间
    private float changetimer; //改变方向的计时器
    public float speed = 3;
    public bool isVertical; //是否垂直方向移动
    private Vector2 moveDirection; //移动方向
    private Rigidbody2D rbody;
    private Animator anim;
    /// <summary>
    ///敌人毁坏标致
    /// </summary>
    public bool broken;

    public ParticleSystem EffectBroken;//损坏特效
    public AudioClip fixclip;

    // Start is called before the first frame update
    void Start()
    {
        //获取动画组件
        anim = GetComponent<Animator>();
        //获取刚体组件
        rbody = GetComponent<Rigidbody2D>();
        moveDirection = isVertical ? Vector2.up : Vector2.right; //如果垂直移动，
        //方向朝上，否则朝右
        changetimer = changeDirectionTime;
        broken = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        changetimer -= Time.deltaTime;
        if (changetimer < 0)
        {
            moveDirection *= -1;
            changetimer = changeDirectionTime; //改变方向后，计时器重新开始计时
        }

        Vector2 position = rbody.position;
        position.x += moveDirection.x*speed*Time.deltaTime;
        position.y += moveDirection.y*speed*Time.deltaTime;
        rbody.MovePosition(position);
        anim.SetFloat("movex", moveDirection.x);
        anim.SetFloat("movey", moveDirection.y);

    }

    /// <summary>
    /// 刚体检测,物理碰撞，物体不会穿越另一个物体
    /// </summary>
    void OnCollisionEnter2D(Collision2D other)
    {
        //与触发器不同
        newplayerC pc = other.gameObject.GetComponent<newplayerC>();
        if (pc!=null)
        {
           pc.ChangeH(-1); 
        }
    }
    public void Fix()
    {
        broken = false;
        if (EffectBroken.isPlaying==true)
        {
            EffectBroken.Stop();
        }
        rbody.simulated = false;
        anim.SetTrigger("fix");
        AudioManager.InstansAudioManager.AudiOplay(fixclip);
    }
}
