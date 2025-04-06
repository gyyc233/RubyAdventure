using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC管理
/// </summary>
public class NPCmanager : MonoBehaviour
{
    public GameObject DialogTipImage;//提示对话框
    public GameObject DialogImage;//对话框图片

    public float ShowImageTime = 4;//对话框每次显示4秒
    public float ShowImageTimer;//对话框显示计时器

    // Start is called before the first frame update
    void Start()
    {
        DialogTipImage.SetActive(true);
        DialogImage.SetActive(false);//初始默认隐藏对话框
        ShowImageTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        ShowImageTimer -= Time.deltaTime;
        if (ShowImageTimer<=0)
        {
            DialogTipImage.SetActive(true);
            DialogImage.SetActive(false);
        }
    }
    /// <summary>
    /// 显示对话框
    /// </summary>
    public void ShowDailogImage()
    {
        ShowImageTimer = ShowImageTime;
        DialogTipImage.SetActive(false);

        DialogImage.SetActive(true);
    }
}
