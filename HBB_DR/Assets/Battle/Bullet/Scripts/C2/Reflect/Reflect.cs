//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ

//--------------------------------------------------------------------------------------
//変数系

    float cooltime_count = 100;//Shot4用のクールタイムカウンター

//--------------------------------------------------------------------------------------
//最初の準備

    private void Start()
    {
        s_Manager = GetComponent<Shot_Manager>();  //s_Managerにある変数を使えるようにするよ
        //追いかける対象と角度を測るスタート地点を決めるよ
        #region 対象の設定
        if (this.gameObject.CompareTag("Player_L1"))
        {
            s_Manager.player = GameObject.Find("Player_L1");
            s_Manager.target = GameObject.Find("Hit_Body_P2");
        }
        else if (s_Manager.player.gameObject.CompareTag("Player_L2"))
        {
            s_Manager.player = GameObject.Find("Player_L2");
            s_Manager.target = GameObject.Find("Hit_Body_P1");
        }
        #endregion
    }

    //--------------------------------------------------------------------------------------
    //発射処理

    public void Shot4()
    {
        cooltime_count += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Button_B1") || Input.GetButtonDown("Button_B2"))
        {
            //発射間隔の調整
            if (cooltime_count >= s_Manager.total_cool_time)
            {
                cooltime_count = 0;
            }
            #region 発射処理
            if (cooltime_count == 0)
            {
                if (s_Manager.target != null)
                {
                    //ベクトル計算
                    s_Manager.distance = s_Manager.target.transform.position - s_Manager.player.transform.position;

                }
                else if (s_Manager.target == null)
                {
                    s_Manager.distance = s_Manager.player.transform.position;
                }
                Vector2 vec = new Vector2(0.0f, 10f);
                vec = Quaternion.Euler(0, 0, 50f) * s_Manager.distance;
                //３方向に出す処理
                for (int bullet_counter = 0; bullet_counter < 3; bullet_counter++)
                {
                    var q = Quaternion.Euler(0, 0, -Mathf.Atan2(s_Manager.distance.x, s_Manager.distance.y)
                                                               * Mathf.Rad2Deg + (-45 + (bullet_counter * 45)));    //それぞれの角度にするよ
                    var t = Instantiate(s_Manager.BulletList[3], transform.position, q);   //代入するよ
                    t.transform.parent = s_Manager.prefab.transform;    //プレハブをここを親にして出すよ
                    vec.Normalize();    //１に正規化するよ
                    t.GetComponent<Rigidbody2D>().velocity = vec;
                }
                //初期化するよ
                cooltime_count = 0;
            }
            #endregion
        }
    }
}