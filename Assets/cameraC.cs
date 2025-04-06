using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraC : MonoBehaviour
{
    public GameObject Ruby;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        //球和相机的相对位置
        offset = transform.position - Ruby.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Ruby.transform.position ;
    }
}
