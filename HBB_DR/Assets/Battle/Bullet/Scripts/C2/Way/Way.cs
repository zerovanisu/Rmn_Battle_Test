//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    float bullet_Speed = 1f;    //弾の速度だよ
    float launch_time = 0;   //発射時間だよ
    float push_count_time = 0;  //ボタン受付時間を記憶するものだよ
    int push_count = 0;     //ボタンの押された回数だよ
    int bullet_number = 1;  //弾の発射する数だよ　０からじゃなくて１からだよ

    bool is_attack = false;     //攻撃を許可するか否かを決めるものだよ    trueになったら攻撃開始だ！

//--------------------------------------------------------------------------------------
//最初の準備

    private void Start()
    {
        s_Manager = GetComponent<Shot_Manager>();     //s_Managerにある変数を使えるようにするよ
    }

//--------------------------------------------------------------------------------------
//ボタンを押してから発射されるまでの処理

    public void Shot1()
    {
        //ボタンをどれだけ押したかの処理
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Button_X1") || Input.GetButtonDown("Button_X2"))
        {
            if (!is_attack)     //攻撃を行っていないことを確認するよ
            {
                if (push_count < 5)     //MAX５回まで押すことができるよ
                {
                    push_count++;
                }
                else if (push_count == 5)
                {
                    is_attack = true;
                }
            }
        }

        #region ボタンを受け付ける時間に関する処理
        if (push_count_time < 2 && push_count != 0)     //２秒まで受け付けるよ
        {
            push_count_time += Time.deltaTime;
        }
        else if (push_count_time > 2)   //２秒たったら５回押されていなくても攻撃許可まで持っていくよ
        {
            is_attack = true;
        }
        #endregion
        //発射間隔やうち終わりの初期化の処理
        if (is_attack)
        {
            launch_time += Time.deltaTime;
            if (launch_time > 0.5 && push_count != 0 && push_count >= bullet_number)    //ボタンを押した回数になるまで、0.5秒ごとに発射するよ
            {
                #region 敵との角度計算
                if (s_Manager.target != null)
                {
                    s_Manager.distance = s_Manager.target.transform.position - s_Manager.player.transform.position;
                }
                else if (s_Manager.target == null)
                {
                    s_Manager.distance = s_Manager.player.transform.position;
                }
                #endregion
                if (bullet_number == 5)     //bullet_numberが１からスタートになっているため ０～４+１で判断するよ
                {
                    for (int i = 0; i < 5; i++)
                    {
                        way(s_Manager.distance, bullet_number, i);
                    }
                }
                else if (bullet_number < 5)
                {
                    for (int i = 0; i < bullet_number; i++)
                    {
                        way(s_Manager.distance, bullet_number, i);
                    }
                }
                bullet_number++;
                launch_time = 0;    //再計算を行うために時間は初期化するよ
            }
            #region 初期化処理
            if (push_count < bullet_number)
            {
                launch_time = 0;
                push_count = 0;
                bullet_number = 1;  //bullet_numberは発射する弾が１発からスタートするため初期化は１
                push_count_time = 0;
                is_attack = false;
            }
            #endregion
        }
    }

//--------------------------------------------------------------------------------------
//発射処理

    public void way(Vector3 Distance, int Way_count, int i)
    {
        Vector2 vec = new Vector2(0.0f, 1.0f);
        vec = Quaternion.Euler(0, 0, 0) * vec;
        vec *= bullet_Speed;
        var q = Quaternion.Euler(0, 0, 0);  //まずは代入する前に初期化を行うよ
        #region 各countに対応した弾の発射処理
        if (Way_count == 1)
        {
            q = Quaternion.Euler(0, 0, (-Mathf.Atan2(Distance.x, Distance.y) * Mathf.Rad2Deg));
        }
        else if (Way_count == 2)
        {
            q = Quaternion.Euler(0, 0, (-Mathf.Atan2(Distance.x, Distance.y) * Mathf.Rad2Deg) + (-3 + (6 * i)));
        }
        else if (Way_count == 3)
        {
            q = Quaternion.Euler(0, 0, (-Mathf.Atan2(Distance.x, Distance.y) * Mathf.Rad2Deg) + (-4 + (4 * i)));
        }
        else if (Way_count == 4)
        {
            q = Quaternion.Euler(0, 0, (-Mathf.Atan2(Distance.x, Distance.y) * Mathf.Rad2Deg) + (-6 + (4 * i)));
        }
        else if (Way_count == 5)
        {
            q = Quaternion.Euler(0, 0, (-Mathf.Atan2(Distance.x, Distance.y) * Mathf.Rad2Deg) + (-10 + (5 * i)));
        }
        #endregion
        var t = Instantiate(s_Manager.BulletList[0], transform.position, q);   //一つにまとめたら！
        t.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
        t.GetComponent<Rigidbody2D>().velocity = vec;   //発射！
    }
}
