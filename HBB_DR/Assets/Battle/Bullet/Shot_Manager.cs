//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Manager : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 
    public List<GameObject> BulletList; //弾をリスト化するよ

    //スタートをplayer位置に
    public GameObject player;
    //ターゲットを敵の位置に
    public GameObject target;
    public Vector3 distance;

    public GameObject prefab;  //プレハブの親を参照するよ
//--------------------------------------------------------------------------------------
//変数系

    public float total_cool_time = 3;//全体のクールタイムを決める

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        //追いかける対象と角度を測るスタート地点を決めるよ
        #region 対象設定
        if (this.gameObject.CompareTag("Player_L1"))
        {
            player = GameObject.Find("Player_L1");
            target = GameObject.Find("Hit_Body_P2");
            prefab = GameObject.Find("Right_Panel");
        }
        else if (this.gameObject.CompareTag("Player_L2"))
        {
            player = GameObject.Find("Player_L2");
            target = GameObject.Find("Hit_Body_P1");
            prefab = GameObject.Find("Left_Panel");
        }
        #endregion
    }
}