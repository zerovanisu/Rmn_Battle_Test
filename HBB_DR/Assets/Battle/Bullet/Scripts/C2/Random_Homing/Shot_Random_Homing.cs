//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Random_Homing : Shot_Common
{
//--------------------------------------------------------------------------------------
//参照系
    
    private Rigidbody2D rb;    //弾のRigidbody2Dを格納する変数だよ
//--------------------------------------------------------------------------------------
//変数系
   
    public int force_pawer; //ランダム移動する際の力の振れ幅をしめすよ

    float rnd;      //角度だよ
    float random_time = 3;      //最初の移動の時間
    float random_change = 0.3f;     //方向を変える時間だよ
    float stop_time = 5;    //ストップする時間だよ
    float homing_time = 5;  //ホーミングする時間だよ
    float homing_stop_time =0;  //ホーミングのむきを変えるまでの時間だよ

    bool is_homing_check = false;   //ホーミングを開始したかを見るチェックだよ
    bool is_hit_check = false;  //壁に当たったかを調べるチェックだよ
    bool is_stop_check = false;     //次の動きに移動するためのチェックだよ

    private Vector2 lastVelocity;   //角度を図るときに使うVector2を用意するよ
//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();  //弾についているRigidbody2Dを入れるよ
        rb.AddForce(new Vector2(Random.Range(-force_pawer, force_pawer) * 100f,
                                Random.Range(-force_pawer, force_pawer) * 100f));     //ランダムな方向に力を加えるよ
        bullet_position = GetComponent<Transform>(); //弾についている位置を入れるよ 

        rnd = Random.value * 360;   //ランダム変数に追加するよ
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rnd);    //弾の向きをランダム変数で得た値に変更する
        //追いかける対象を決めるよ
        #region 対象設定
        if (this.gameObject.CompareTag("Bullet_1"))
        {
            target_position = GameObject.Find("Hit_Body_P2");
        }
        else if(this.gameObject.CompareTag("Bullet_2"))
        {
            target_position = GameObject.Find("Hit_Body_P1");
        }
        #endregion
        InvokeRepeating("homing", 0.7f, 0.7f);  //ホーミングの処理　0.7秒ごとに実行するよ
    }

//--------------------------------------------------------------------------------------
//ランダム移動処理と、ホーミング処理

    void Update()
    {
        if (random_time > 0)
        {
            random_time -= Time.deltaTime;  //ランダム移動し続けるまでの時間を計算
            random_change -= Time.deltaTime;    //ランダムに移動するまでの時間を計算
            if (random_change < 0)
            {
                rb.velocity = Vector2.zero;     //移動を一回止めるよ
                rb.AddForce(new Vector2(Random.Range(-force_pawer, force_pawer) * 100f,
                                        Random.Range(-force_pawer, force_pawer) * 100f));   //ランダムな方向に移動するよ
                random_change = 0.3f;   //ランダムに移動するまでの時間を初期化
            }
        }
        else
        {
            //時間が来たら移動をやめ、色を変更→追尾に行動を変更させる処理だよ
            if (!is_stop_check)
            {
                rb.velocity = Vector2.zero;     //移動を一回止めるよ
                is_stop_check = true;   //ランダム移動を止めて、ホーミングに切り替えるよ
            }
            //相手の移動する時間を設けるよ
            else if (stop_time > 0)
            {
                stop_time -= Time.deltaTime;
            }
            else 
            {
                is_homing_check = true;     //ホーミング開始
                homing_time -= Time.deltaTime;
                if(homing_time < 0)
                {
                    is_hit_check = true;    //当たり判定オン！
                    Destroy(this.GetComponent<CircleCollider2D>());
                }
            }
        }
    }

//--------------------------------------------------------------------------------------
//常に力を加え続ける処理

    void FixedUpdate()
    {
        if (!is_stop_check)
        {
            this.lastVelocity = this.rb.velocity;
        }
    }

//--------------------------------------------------------------------------------------
//ランダム移動中にステージのはじに当たった場合の処理

    void OnCollisionEnter2D(Collision2D Reflect)
    {
        if (random_time > 0)
        {
            Vector2 refrectVec = Vector2.Reflect(this.lastVelocity, Reflect.contacts[0].normal);    ////反射する角度の計算
            this.rb.velocity = refrectVec;  //ぶち込みー
        }
    }

//--------------------------------------------------------------------------------------
//ホーミング処理

    void homing()
    {
        if (is_homing_check && !is_hit_check)
        {
            if (homing_stop_time > 0)
            {
                homing_stop_time -= Time.deltaTime;
            }
            else
            {
                #region おまけの三方向に飛ばす処理
                bullet_position = GetComponent<Transform>(); //弾についている位置を入れるよ 
                rb.velocity = Vector2.zero;     //一度速度を０にする
                if (target_position != null)
                {
                    Vector3 vector3 = target_position.transform.position - bullet_position.position;    //敵と弾の位置から角度を出すよ
                    rb.AddForce(vector3.normalized * bullet_Speed * 10);     //方向の長さを1に正規化して、任意の力をAddForceで加えるよ
                }
                else if (target_position == null)
                {
                    Vector3 vector3 = bullet_position.position;     //敵がいないからとりあえず自分の位置を出すよ
                    rb.AddForce(vector3.normalized * bullet_Speed * 10);     //方向の長さを1に正規化して、任意の力をAddForceで加えるよ
                }
                #endregion
            }
        }
    }

//--------------------------------------------------------------------------------------

}
