//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    public float interval;  //発射間隔だよ
    public int launch_interval = 0;    //発射際に使うやつだよ。自由に調整できるよ
    float duration_time = 3; //発射し続ける時間だよ
    float rotation_speed = 1f;  //回転する速度だよ  １０００にすると必殺技みたいになるよ（よけられると思うな）
    public int continue_time = 3;  //一度の操作で出続ける時間だよ
    float cool_time = 3; //クールタイムだよ

    bool is_attack = false;   //攻撃を許可するか否かを決めるものだよ  trueになったら攻撃開始だ！
    bool is_cooltime_check = false;     //クールタイムの入っているかチェックするよ  trueの時はクールタイム中だよ

//--------------------------------------------------------------------------------------
//最初の準備

    private void Start()
    {
        s_Manager = GetComponent<Shot_Manager>();  //s_Managerにある変数を使えるようにするよ
    }

//--------------------------------------------------------------------------------------
//発射処理

    public void Shot2()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Button_A1") || Input.GetButtonDown("Button_A2"))
        {
            //攻撃の許可を出すかチェック
            if (is_cooltime_check == false && is_attack == false)
            {
                is_attack = true;
            }
        }
        if (is_cooltime_check == false && is_attack == true)
        {
            launch_interval++;
            //出る数を調整する処理
            if (launch_interval % interval == 0)
            {
                for (int i = 0; i < 1; i++)     //ここのiの数を変えると一度に出る弾の数が変わるよ
                {
                    Vector2 Vec = new Vector2(0.0f, 10f);
                    Vec = Quaternion.Euler(0, 0, 50f * launch_interval) * Vec;
                    Vec.Normalize();
                    Vec = Quaternion.Euler(0, 0, (360 / 5) * i) * Vec;  //どの位の角度で出すか計算するよ
                    Vec *= rotation_speed;
                    //３方向に出す処理
                    for (int bullet_counter = 0; bullet_counter < 3; bullet_counter++)
                    {
                        var a = Quaternion.Euler(0, 0, -Mathf.Atan2(Vec.x, Vec.y) * Mathf.Rad2Deg + (bullet_counter * 120));    //３方向のspralなので１２０度ごとに出すよ
                        var b = Instantiate(s_Manager.BulletList[1], transform.position, a);
                        b.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                        b.GetComponent<Rigidbody2D>().velocity = Vec;
                    }
                }
            }
        }
        if (is_attack == true && is_cooltime_check == false)
        {
            duration_time -= Time.deltaTime;
            if (duration_time <= 0)
            {
                //初期化
                is_attack = false;
                is_cooltime_check = true;
                duration_time = continue_time;
            }
        }
        else if (is_cooltime_check == true && is_attack == false)
        {
            //初期化処理だよ
            cool_time -= Time.deltaTime;
            if (cool_time <= 0)
            {
                is_cooltime_check = false;
                cool_time = 3;
            }
        }
    }
}

