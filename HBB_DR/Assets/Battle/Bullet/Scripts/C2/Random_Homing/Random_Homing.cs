//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Homing : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    float cooltime_count = 100;     //クールタイムの時間だよ
    float random_homing_cooltime = 15;  //クールタイムの基準値だよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        s_Manager = GetComponent<Shot_Manager>(); //s_Managerにある変数を使えるようにするよ
    }

//--------------------------------------------------------------------------------------
//発射処理

    public void Shot6()
    {
        cooltime_count += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Button_Y1") || Input.GetButtonDown("Button_Y2"))
        {
            if (cooltime_count > random_homing_cooltime)
            {
                cooltime_count = 0;
            }
            if (cooltime_count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject Shot = Instantiate(s_Manager.BulletList[5]);
                    Shot.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                    Shot.transform.position = this.transform.position;
                }
            }
        }
    }
}
