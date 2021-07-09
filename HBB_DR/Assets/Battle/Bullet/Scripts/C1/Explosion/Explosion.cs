//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    float cooltime_count = 100;//Shot7用のクールタイムカウンター
    float explosion_cooltime = 5;   //エクスプロージョンのクールタイム数

    public bool burst_prefab;   //burstのprefabの時に使うよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        s_Manager = GetComponent<Shot_Manager>(); //s_Managerにある変数を使えるようにするよ
        //中央の位置を調べるよ
        if (s_Manager.prefab.gameObject.CompareTag("Bullet_1"))
        {
            burst_prefab = true;
        }
        else if (s_Manager.prefab.gameObject.CompareTag("Bullet_2"))
        {
            burst_prefab = false;
        }
    }

//--------------------------------------------------------------------------------------
//発射処理

    public void Shot7()
    {
        cooltime_count += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Button_X1") || Input.GetButtonDown("Button_X2"))
        {
            if (cooltime_count > explosion_cooltime)
            {
                cooltime_count = 0;
            }
            if (cooltime_count == 0)
            {
                GameObject Shot = Instantiate(s_Manager.BulletList[6]);
                Shot.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                Shot.transform.position = this.transform.position;
            }
        }
    }
}
