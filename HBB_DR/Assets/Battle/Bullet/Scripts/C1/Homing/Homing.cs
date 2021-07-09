//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Homing s_Homing;
    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    float cooltime_count = 100; //クールタイムの時間だよ
    float cooltime = 10; //クールタイムの基準値だよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        s_Manager = GetComponent<Shot_Manager>(); //s_Managerにある変数を使えるようにするよ
    }

//--------------------------------------------------------------------------------------
//発射処理

    public void Shot3()
    {
        if (cooltime_count < cooltime)
        {
            cooltime_count += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Button_B1") || Input.GetButtonDown("Button_B2"))
        {
            if ((cooltime_count > cooltime))
            {
                GameObject Shot = Instantiate(s_Manager.BulletList[2]);
                Shot.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                Shot.transform.position = this.transform.position;
                //初期化
                cooltime_count = 0;
            }
        }
    }
}