//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Reflect : Shot_Common
{
//--------------------------------------------------------------------------------------
//参照系 
    
    private Rigidbody2D rb; //弾のRigidbody2Dを格納する変数だよ
    public  GameObject Player;  //敵との距離を測る時に使うプレイヤーの位置を格納する変数だよ

//--------------------------------------------------------------------------------------
//変数系

    int cnt = 0;    //跳ね返る回数だよ

    private Vector2 lastVelocity;   //最終位置だよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();     //弾についているRigidbody2Dを入れるよ
        rb.AddForce(gameObject.transform.rotation * new Vector3(0, bullet_Speed * 50, 0));
    }

//--------------------------------------------------------------------------------------

    void FixedUpdate()
    {
        this.lastVelocity = this.rb.velocity;   //現在の位置を最後にいた位置に変更するよ
    }

//--------------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (cnt <= 4)
        {
            Vector2 refrectVec = Vector2.Reflect(this.lastVelocity, collision.contacts[0].normal);  //反射する角度の計算だよ
            this.rb.velocity = refrectVec;
            if (cnt == 3)
            {
                Destroy(this.GetComponent<CircleCollider2D>());
                Destroy(this.gameObject);
            }
            cnt++;
        }
    }

//--------------------------------------------------------------------------------------

}
