using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TateScroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField] float speed = 100;
    public bool field_direction;   //falseなら左,trueなら右
    void Update()
    {
        //下方向にスクロール
        transform.position -= new Vector3(0, Time.deltaTime * speed);
        if (!field_direction)
        {
            //Yが-1699まで来れば、1685まで移動する
            if (transform.position.y <= -1699f)
            {
                transform.position = new Vector2(-898f, 1685f);
            }
        }
        if (field_direction)
        {
            //Yが1685まで来れば、-1699まで移動する
            if (transform.position.y <= -1699f)
            {
                transform.position = new Vector2(898f, 1685f);
            }
        }


    }
}
