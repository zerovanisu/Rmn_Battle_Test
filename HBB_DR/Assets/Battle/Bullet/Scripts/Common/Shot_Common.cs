//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Common : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系
    GameObject stage;   //当たり判定となるステージ格納する変数だよ

    protected GameObject player_position;   //敵との距離を測る時に使うプレイヤーの位置を格納する変数だよ
    protected GameObject target_position;   //敵との距離を測る時に使うターゲットの位置を格納する変数だよ
    protected Transform bullet_position;    //敵との距離を測る時に使う弾の位置を格納する変数だよ
    public  GameObject bullet_normal;    //最後に飛び散る処理があるときに使う弾を格納する変数するだよ

//--------------------------------------------------------------------------------------
//変数系

    public float damage;    //弾のダメージだよ        
    public float bullet_Speed = 0;  //弾の速度だよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        stage = GameObject.Find("Stage");   //ステージのオブジェクトを入れるよ
    }

//--------------------------------------------------------------------------------------
//外に出た時の処理

    void OnTriggerExit2D(Collider2D collision)
    {
        #region ステージ外に弾が出るときの処理だよ
        if (collision.gameObject.tag == "BuckStage")
        {
            Destroy(this.gameObject);   //弾自体を消去するよ
        }
        #endregion
    }

//--------------------------------------------------------------------------------------
//接触時の処理

    void OnTriggerEnter2D(Collider2D collision)
    {
        #region ダメージ処理だよ
        if (collision.gameObject.tag == "Hit_Body_P1" || collision.gameObject.tag == "Hit_Body_P2")
        {
            
            if (collision.gameObject.GetComponent<Player_Manager_R>())//相手の体力を調べるよ
            {
                collision.gameObject.GetComponent<Player_Manager_R>().Damage(damage);   //ダメージを与えるよ
            }
            Destroy(this.gameObject);
        }
        #endregion
        #region バリアが張られていたら弾を消す処理だよ
        if (collision.gameObject.tag == "Barrier")
        {
            Destroy(this.gameObject);   //弾自体を消去するよ
        }
        #endregion
    }

//--------------------------------------------------------------------------------------

}
