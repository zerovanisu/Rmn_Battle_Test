//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    int launch_interval = 0;    //発射間隔だよ
    float duration_time = 3;    //打ち続ける時間
    float quantity = 25;    //発射する量だよ
    float bullet_speed = 1f;    //弾の速度だよ
    int attack_time = 3;    //攻撃し続ける時間だよ

    bool duration = false;  //攻撃しているか見るよ
    bool cooltime_check = false;    //クールタイム中か見るよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        s_Manager = GetComponent<Shot_Manager>();  //s_Managerにある変数を使えるようにするよ
    }

//--------------------------------------------------------------------------------------
//最初の準備

    public void Shot5()
    {
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Button_Y1") || Input.GetButtonDown("Button_Y2"))
        {
            if (cooltime_check == false && duration == false)
            {
                duration = true;
            }
        }
        if (cooltime_check == false && duration == true)
        {
            launch_interval++;
            //ここの数字をいじると、飛んでいく弾の量を調整することができる
            if (launch_interval % quantity == 0)
            {
                #region 敵との角度計算
                if (s_Manager.target != null)
                {
                    //ベクトル計算
                    s_Manager.distance = s_Manager.target.transform.position - s_Manager.player.transform.position;
                }
                else if (s_Manager.target == null)
                {
                    s_Manager.distance = s_Manager.player.transform.position;
                }
                #endregion
                #region　弾を飛ばす処理
                Vector2 vec = new Vector2(0.0f, 1.0f);
                vec = Quaternion.Euler(0, 0, Random.Range(-35f, 35f)) * vec;
                vec *= bullet_speed;
                var q = Quaternion.Euler(0, 0, -Mathf.Atan2(s_Manager.distance.x + Random.Range(-400f, 400f), s_Manager.distance.y + Random.Range(-40f, 40f)) * Mathf.Rad2Deg);
                var t = Instantiate(s_Manager.BulletList[4], transform.position, q);
                t.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                t.GetComponent<Rigidbody2D>().velocity = vec;
                #endregion
            }
        }
        #region クールタイムの処理
        if (duration == true && cooltime_check == false)
        {
            duration_time -= Time.deltaTime;
            if (duration_time <= 0)
            {
                duration = false;
                cooltime_check = true;
                duration_time = attack_time;
            }
        }
        else if (cooltime_check == true && duration == false)
        {
            s_Manager.total_cool_time -= Time.deltaTime;
            if (s_Manager.total_cool_time <= 0)
            {
                cooltime_check = false;
                s_Manager.total_cool_time = 3;
                launch_interval = 0;
            }
        }
        #endregion
    }
}
