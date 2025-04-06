using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理相关
/// </summary>
public class UImanager : MonoBehaviour
{
    //创建单例模式
    public static UImanager InsUImanager { get; private set; }
    public Image HeadBar;//角色血量

    public Text BulletText;//角色当前子弹数量
    //

    void Awake()
    {
        InsUImanager = this;
    }
  

    public void updateHeadBar(int curAmount,int Amount)
    {
        HeadBar.fillAmount = (float)curAmount / (float)Amount;
    }
    /// <summary>
    /// 更新子弹数量
    /// </summary>
    /// <param name="curcount"></param>
    /// <param name="maxcount"></param>
    public void UpdateBulletCount(int curcount, int maxcount)
    {
        BulletText.text = curcount.ToString() + "/" + maxcount.ToString();
    }
}
