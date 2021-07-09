//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系
    
    int continue_time = 3; //スパイラルの一度の操作で続ける時間
    float is_attack_time = 3;
    float time_count = 0;
    
    bool rotation;  //回転方向を示すよ　左ならfalse,右ならtrue
    bool is_attack = false; //攻撃を許可するか否かを決めるものだよ  trueになったら攻撃開始だ！
    bool is_cooltime_check = false; //クールタイム中か見るよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        s_Manager = GetComponent<Shot_Manager>(); //s_Managerにある変数を使えるようにするよ
    }

//--------------------------------------------------------------------------------------
//発射処理

    public void Shot8()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Button_A1") || Input.GetButtonDown("Button_A2"))
        {
            if (is_cooltime_check == false && is_attack == false)
            {
                is_attack = true;
            }
        }
        if (is_cooltime_check == false && is_attack == true)
        {
            time_count++;
            if (time_count % 120 == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    #region　弾の角度計算
                    //玉の角度
                    float Shot_Angle = i * 45;//変更時は22.5(全方向ではなく前だけの場合)
                    Vector3 Angle = transform.eulerAngles;
                    Angle.x = transform.rotation.x;
                    Angle.y = transform.rotation.y;
                    Angle.z = Shot_Angle;
                    #endregion
                    for (int j = 0; j < 2; j++)
                    {
                        #region 弾についているrotationにboolを代入する処理
                        if (!rotation)
                        {
                            rotation = true;    // 左向きだよ！
                        }
                        else if (rotation)
                        {
                            rotation = false;   // 右向きだよ！
                        }
                        #endregion
                        s_Manager.BulletList[7].GetComponent<Shot_Cross>().is_direction = rotation;
                        GameObject Shot7 = Instantiate(s_Manager.BulletList[7]) as GameObject;
                        Shot7.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                        Shot7.transform.rotation = Quaternion.Euler(Angle);
                        Shot7.transform.position = this.transform.position;
                    }
                }
            }
        }
        #region クールタイム処理
        if (is_attack == true && is_cooltime_check == false)
        {
            is_attack_time -= Time.deltaTime;
            if (is_attack_time <= 0)
            {
                is_attack = false;
                is_cooltime_check = true;
                is_attack_time = continue_time;
            }
        }
        else if (is_cooltime_check == true && is_attack == false)
        {
            s_Manager.total_cool_time -= Time.deltaTime;
            if (s_Manager.total_cool_time <= 0)
            {
                is_cooltime_check = false;
                s_Manager.total_cool_time = 3;
            }
        }
        #endregion
    }
}
