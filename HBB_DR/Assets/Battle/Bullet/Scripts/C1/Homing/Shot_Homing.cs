//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Homing : Shot_Common
{
//--------------------------------------------------------------------------------------
//参照系
    
    Shot_Manager s_Manager;   //Shot_Managerを呼び出すためのものだよ
    private Rigidbody2D rb; //弾のRigidbody2Dを格納する変数だよ

//--------------------------------------------------------------------------------------
//変数系
    
    [SerializeField] private float limitSpeed;  //弾の速度に関する制限値だよ

    public float Homing_Time = 10;  //追いかける時間だよ

    //--------------------------------------------------------------------------------------
    //最初の準備

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //弾についているRigidbody2Dを入れるよ
        bullet_position = GetComponent<Transform>(); //弾についている位置を入れるよ 

        //中央の位置を調べるよ
        #region 対象設定
        if (this.gameObject.CompareTag("Bullet_1"))
        {
            target_position = GameObject.Find("Hit_Body_P2");
        }
        else if (this.gameObject.CompareTag("Bullet_2"))
        {
            target_position = GameObject.Find("Hit_Body_P1");
        }
        #endregion
    }

//--------------------------------------------------------------------------------------
//追いかける時間とその後の処理

    void Update()
    {
        Homing_Time -= Time.deltaTime;  //追いかける時間を計算するよ
        //追いかける時間が０になったら行う処理だよ
        if (Homing_Time <= 0)
        {
            rb.velocity = new Vector3(0f, 0f);  //移動を一旦停止させるよ
            Destroy(this.gameObject);
            #region ３方向に弾を出す処理
            for (int i = 1; i <= 3; i++)
            {
                float Shot_Angle = i * 120;     //１２０度ごとに弾を出すよ
                Vector3 Angle = transform.eulerAngles;  //今の角度を入れるよ
                Angle.x = transform.rotation.x;     //ｘ軸を入れるよ
                Angle.y = transform.rotation.y;     //ｙ軸を入れるよ
                Angle.z = Shot_Angle;   //ｚ軸を入れるよ
                GameObject Bullet = Instantiate(bullet_normal) as GameObject;   //出す弾を指定するよ
                Bullet.transform.rotation = Quaternion.Euler(Angle);    //角度を代入するよ
                Bullet.transform.position = this.transform.position;    //発射ぁ！
            }
            #endregion
        }
    }

//--------------------------------------------------------------------------------------
//追いかける処理

    private void FixedUpdate()
    {
        bullet_position = GetComponent<Transform>(); //弾についている位置を入れるよ 
        #region 敵がいるかのチェックをするよ
        if (target_position != null)
        {
            Vector3 vector3 = target_position.transform.position - bullet_position.position;    //敵と弾の位置から角度を出すよ
            rb.AddForce(vector3.normalized * bullet_Speed);     //方向の長さを1に正規化して、任意の力をAddForceで加えるよ
        }
        else if (target_position == null)
        {
            Vector3 vector3 = bullet_position.position;     //敵がいないからとりあえず自分の位置を出すよ
            rb.AddForce(vector3.normalized * bullet_Speed);     //方向の長さを1に正規化して、任意の力をAddForceで加えるよ
        }
        #endregion
        #region 速度制限
        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);     //X方向の速度を制限するよ
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);     //Y方向の速度を制限するよ
        rb.velocity = new Vector3(speedXTemp, speedYTemp);      //実際に制限した値を代入
        #endregion
    }

    //--------------------------------------------------------------------------------------

}