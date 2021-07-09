//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Explosion : Shot_Common
{
//--------------------------------------------------------------------------------------
//参照系

    private Rigidbody2D rb;  //弾のRigidbody2Dを格納する変数だよ
    GameObject enemy_position;   //敵との距離を測る時に使う敵の位置を格納する変数だよ
    public  GameObject bullet_burst;    //最後に飛び散る処理があるときに使う弾を格納する変数するだよ

//--------------------------------------------------------------------------------------
//変数系

    public float burst_speed = 0 * 0.01f;  //炸裂する弾の速度だよ
    int check = 0;      //弾の現状を示す変数だよ。０～４　で数字によって出る数が変わるよ
    int time_count = 0;     //弾の出る時間を図る時に使うよ

    Vector3 vector3;    //角度を図るときに使うVector3を用意するよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        //中央の位置を調べるよ
        #region 対象設定
        if (this.gameObject.CompareTag("Bullet_1"))
        {
            target_position = GameObject.Find("Explosion_TargetP1");
            enemy_position = GameObject.Find("Player_R2");
        }
        else if (this.gameObject.CompareTag("Bullet_2"))
        {
            target_position = GameObject.Find("Explosion_TargetP2");
            enemy_position = GameObject.Find("Player_R1");
        }
        #endregion
        rb = this.GetComponent<Rigidbody2D>();   //弾についているRigidbody2Dを入れるよ
        bullet_position = GetComponent<Transform>();    //弾についている位置を入れるよ
        //敵がいるかのチェックをするよ
        #region 敵との距離計算
        if (target_position != null)
        {
            //弾から追いかける対象への方向を計算
            vector3 = target_position.transform.position - bullet_position.position;     //敵と弾の位置から角度を出すよ
        }
        else if (target_position == null)
        {
            //敵がいなかったら何もしないよ
        }
        #endregion
        rb.AddForce(vector3.normalized * bullet_Speed, 
        (ForceMode2D)ForceMode.Impulse);      //方向の長さを1に正規化して、任意の力をAddForceで加えるよ
    }

//--------------------------------------------------------------------------------------
//発射処理

    void Update()
    {
        #region 発射処理
        //４以上かどうかを見るよ
        if (check < 4)
        {
            if (time_count % 100 == 0)
            {
                switch (check)
                {
                    case 3:
                        for (int i = 0; i < 10; i++)
                        {
                            Exp(i);
                        }
                        check++;    //次のフェーズに移動するよ
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        for (int i = 0; i < 20; i++)
                        {
                            Exp(i);
                        }
                        check++;    //次のフェーズに移動するよ
                        break;
                    case 1:
                        for (int i = 0; i < 30; i++)
                        {
                            Exp(i);
                        }
                        check++;    //次のフェーズに移動するよ
                        break;
                    default:
                        break;
                }
            }
            time_count++;
        }
        #endregion
    }

    //--------------------------------------------------------------------------------------
    //弾の生成処理

    void Exp(int i)
    {
        Vector2 vec = enemy_position.transform.position - bullet_position.position;     //敵と弾の位置から角度を出すよ
        vec.Normalize();
        #region 弾を指定の角度ごとに出す処理
        if (check == 1)
        {
            vec = Quaternion.Euler(0, 0, (360 / 10) * i) * vec;     //３６度ずつ出すよ
        }
        else if (check == 2)
        {
            vec = Quaternion.Euler(0, 0, (360 / 20) * i) * vec;     //１８度ずつ出すよ
        }
        if (check == 3)
        {
            vec = Quaternion.Euler(0, 0, (360 / 10) * i) * vec;     //３６度ずつ出すよ
        }
        #endregion
        vec *= burst_speed;
        var q = Quaternion.Euler(0, 0, -Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg);     //敵との角度を代入するよ
        var t = Instantiate(bullet_burst, transform.position, q);   //弾に情報を代入するよ
        t.GetComponent<Rigidbody2D>().velocity = vec;   //発射ぁ！

    }

//--------------------------------------------------------------------------------------

    void OnTriggerEnter2D(Collider2D BD)
    {
        //０かどうかするよ
        if (check == 0)
        {
            if (BD.gameObject.name == "Explosion_TargetP1" || BD.gameObject.name == "Explosion_TargetP2")
            {
                rb.velocity = Vector3.zero;  //速度を０にするよ

                check = 1;  //１にして全体への攻撃を開始！
            }
        }  
    }
}
