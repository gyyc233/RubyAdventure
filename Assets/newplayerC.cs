using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 控制角色移动/生命/动画等
/// </summary>
public class newplayerC : MonoBehaviour
{
    public float speed=5f;
    Rigidbody2D rbody;
    public Rigidbody2D r { get { return rbody; } }  
    private int maxH=5;//最大生命值
    private int currH;//当前生命值
   
    private int max_bullet_count = 99;//最大子弹数
    private int curr_bullet_count;//当前子弹数

    //添加属性
    public int my_max_bullet_count { get { return max_bullet_count;} }
    public int my_curr_bullet_count { get { return curr_bullet_count; } }
    //或者进行序列化，在 private int max_bullet_count = 5;//最大子弹数
    //的前面加上  [SerializeField]  进行序列化



    public int MymaxH { get { return maxH; } }//用于访问，就进行添加属性
    public int MycurrH { get { return currH; } }


    private float invicibletime = 2f;//无敌时间2秒
    private float invicibletimer;//无敌计时器
    private bool isinvicible;//是否处于无敌状态
    //玩家的朝向信息
    private Vector2 lookdirection=new Vector2(1,0);//默认朝右
    //动画
    private Animator anim;
    //攻击子弹
    public GameObject BulletGameObject;
    //======玩家音效=======
    public AudioClip hitaudioclip;//受伤音效
    public AudioClip LunchAudioClip;//发射齿轮音效
    public AudioClip WalkAudioClip;//行走音效


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("1111");
        currH = 5;
        curr_bullet_count = 2;
        //获取刚体组件
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();//获取动画组件
        isinvicible = false;
        invicibletimer = 0;
        //初始化血条
        UImanager.InsUImanager.updateHeadBar(currH, maxH);//更新血条
        UImanager.InsUImanager.UpdateBulletCount(2,99);//更新子单数目
    }
    
    // Update is called once per frame
    void Update()
    {
        // transform.Translate(transform.right * speed * Time.deltaTime);//使角色向右移动
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY= Input.GetAxisRaw("Vertical");
        //移动向量 
        Vector2 moveVector=new Vector2(MoveX,MoveY);
        if (moveVector.x!=0|| moveVector.y!=0)
        {
            lookdirection = moveVector;
        }
        anim.SetFloat("Look X",lookdirection.x);
        anim.SetFloat("Look Y", lookdirection.y);
        //moveVector.magnitude向量的长度
        anim.SetFloat("Speed",moveVector.magnitude);

        //============移动======================================================
        Vector2 position = rbody.position;  // 使用刚体组件移动
        //Vector2 Position = transform.position;  //不使用钢体组件
        //Position.x += MoveX*speed * Time.deltaTime;
        //Position.y += MoveY * speed * Time.deltaTime;


        //另一种移动方式
        position += moveVector * speed * Time.deltaTime;
        //transform.position = Position;//不使用钢体组件
        rbody.MovePosition(position);
        //==========无敌计时=======================================================
        if (isinvicible==true)
        {
            invicibletimer -= Time.deltaTime;
            if (invicibletimer<0)
            {
                isinvicible = false;
            }
        }
        //====按下  J 键，进行攻击
        if (Input.GetKeyDown(KeyCode.F)&&curr_bullet_count>0)
        {
            ChengeBulletCount(-1);
            //实例化将对象作为第一个参数，并在第二个参数的位置处创建一个副本，并在第三个参数中旋转
            GameObject bullet = Instantiate(BulletGameObject, rbody.position+Vector2.up*0.5f, Quaternion.identity);
            BulletControl bc = bullet.GetComponent<BulletControl>();
            if (bc!=null)
            {
               bc.Move(lookdirection,200);
               anim.SetTrigger("Launch");
               AudioManager.InstansAudioManager.AudiOplay(LunchAudioClip);
            }
        }
        //====按下E键，进行NPC交互
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //发射射线,射线碰到NPC就反应
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, 
                lookdirection,2f,
                LayerMask.GetMask("npc"));
            if (hit.collider!=null)
            {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
                if (npc!=null)
                {
                    npc.ShowDailogImage();
                }
                Debug.Log("碰到NPC了");
            }
        }
    }
    /// <summary>
    /// 改变生命值的方法
    /// </summary>
    /// <param name="num"></param>
    public void ChangeH(int num)
    {
        //如果玩家受到伤害
        if (num<0)
        {
            if (isinvicible==true)
            {
                return;
            }
            isinvicible = true;
            anim.SetTrigger("Hit");
            AudioManager.InstansAudioManager.AudiOplay(hitaudioclip);
            invicibletimer = invicibletime;
        }
        currH=Mathf.Clamp(currH+num,0,maxH);//currH+num大于0，,小于maxH
        UImanager.InsUImanager.updateHeadBar(currH,maxH);//更新血条
        Debug.Log(currH+"/"+maxH);
        //UnityEngine.Debug.LogWarning("无用警告");
    }
    
    public void ChengeBulletCount(int n)
    {
        curr_bullet_count = Mathf.Clamp(curr_bullet_count + n, 0, max_bullet_count);
        //更新子弹条
        UImanager.InsUImanager.UpdateBulletCount(curr_bullet_count,max_bullet_count);
    }
}
