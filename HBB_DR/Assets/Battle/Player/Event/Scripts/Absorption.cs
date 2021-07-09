//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorption : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    public GameObject pl;   //当たり判定を入れる場所だよ

//--------------------------------------------------------------------------------------
//変数系

    float damage;   //ダメージ変数の受け取り用だよ
    float ans;  //ダメージ計算後の受け取りようだよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        damage = 0; //ダメージ量を初期化するよ
        ans = 0;    //計算後の値を初期化するよ
    }

//--------------------------------------------------------------------------------------
//吸収時の処理

    void OnTriggerEnter2D(Collider2D BD)
    {
        damage = 0; //ダメージ量を初期化するよ
        ans = 0;    //計算後の値を初期化するよ

        if (BD.gameObject.tag == "Bullet_1" || BD.gameObject.tag == "Bullet_2" || BD.gameObject.tag == "Bullet_3")
        {
            if (BD.gameObject.GetComponent<Shot_Common>())  //触れた弾からダメージの変数があるか調べるよ
            {
                damage = BD.gameObject.GetComponent<Shot_Common>().damage;  //ダメージを与えるよ
            }
            ans = (damage / 4);     //回復する量を計算するよ
            pl.GetComponent<Player_Manager_R>().m_Player_HP += ans;     //自機（左）の体力を回復させるよ
        }
    }

//--------------------------------------------------------------------------------------

}
