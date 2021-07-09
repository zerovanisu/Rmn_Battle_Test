//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager_L : MonoBehaviour
{
    //Rigidbody2D コンポーネントを格納する変数
    private Rigidbody2D Player_L;
    private Rigidbody2D Reflect;
    //勝敗を決める変数入れるよう
    GameObject Syouhai;
    //キャラクター選択
    public int Character;
    //キャラクターの攻撃力[%]
    public float AttackPower_percent = 1;

    [SerializeField]
    Way s_Way;
    [SerializeField]
    Spiral s_Spiral;
    [SerializeField]
    Homing s_Homing;
    [SerializeField]
    Reflect s_Reflect;
    [SerializeField]
    Barrage s_Barrage;
    [SerializeField]
    Random_Homing s_Random_Homing;
    [SerializeField]
    Explosion s_Explosion;
    [SerializeField]
    Cross s_Cross;
 
//--------------------------------------------------------------------------------------
    // ゲームのスタート時の処理
    void Start()
    {
        //Rigidbody2D　コンポーネントを取得して変数　Player_L　に格納
        Player_L = GetComponent<Rigidbody2D>();
        //勝敗Bool取得
        Syouhai = GameObject.Find("Game_Setting");
    }

//--------------------------------------------------------------------------------------

    // 繰り返す処理
    void Update()
    {
        //勝敗がついていなかった場合弾を打つことを許可
        if (!Syouhai.GetComponent<Setting>().syouhai)
        {
            //プレイヤー１だった場合
            if (this.gameObject.CompareTag("Player_L1"))
            {
                if (Character == 1)
                {
                    //キャラクターが使える４つの弾
                    //１つ目　操作キー：Z　｜　操作ボタン：A
                    s_Cross.Shot8();//Cross
                                       //２つ目　操作キー：X　｜　操作ボタン：B
                    s_Homing.Shot3();//Homing
                                       //３つ目　操作キー：C　｜　操作ボタン：X
                    s_Explosion.Shot7();//Explosion
                                       //４つ目　操作キー：V　｜　操作ボタン：Y
                    s_Barrage.Shot5();//Barrage
                    //テスト

                    //必殺技

                }
                else if (Character == 2)
                {
                    //キャラクターが使える４つの弾
                    //１つ目　操作キー：Z　｜　操作ボタン：A
                    s_Spiral.Shot2();//spiral
                                       //２つ目　操作キー：X　｜　操作ボタン：B
                    s_Reflect.Shot4();//Reflect
                                       //３つ目　操作キー：C　｜　操作ボタン：X
                    s_Way.Shot1();//Way
                                       //４つ目　操作キー：V　｜　操作ボタン：Y
                    s_Random_Homing.Shot6();//Random_Homing


                    //必殺技

                }
                /*
                else if (Character == 3)
                {
                    //キャラクターが使える４つの弾
                    //１つ目　操作キー：Z　｜　操作ボタン：A

                    //２つ目　操作キー：X　｜　操作ボタン：B

                    //３つ目　操作キー：C　｜　操作ボタン：X

                    //４つ目　操作キー：V　｜　操作ボタン：Y



                    //必殺技

                }
                */
            }
            //プレイヤー２だった場合
            else if (this.gameObject.CompareTag("Player_L2"))
            {
                if (Character == 1)
                {
                    //キャラクターが使える４つの弾
                    //１つ目　操作キー：Z　｜　操作ボタン：A
                    s_Cross.Shot8();//Cross
                                    //２つ目　操作キー：X　｜　操作ボタン：B
                    s_Homing.Shot3();//Homing
                                     //３つ目　操作キー：C　｜　操作ボタン：X
                    s_Explosion.Shot7();//Explosion
                                        //４つ目　操作キー：V　｜　操作ボタン：Y
                    s_Barrage.Shot5();//Barrage

                    //テスト


                    //必殺技

                }
                else if (Character == 2)
                {
                    //キャラクターが使える４つの弾
                    //１つ目　操作キー：Z　｜　操作ボタン：A
                    s_Spiral.Shot2();//spiral
                                     //２つ目　操作キー：X　｜　操作ボタン：B
                    s_Reflect.Shot4();//Reflect
                                      //３つ目　操作キー：C　｜　操作ボタン：X
                    s_Way.Shot1();//Way
                                  //４つ目　操作キー：V　｜　操作ボタン：Y
                    s_Random_Homing.Shot6();//Random_Homing


                    //必殺技

                }
                /*
                else if (Character == 3)
                {
                    //キャラクターが使える４つの弾
                    //１つ目　操作キー：Z　｜　操作ボタン：A

                    //２つ目　操作キー：X　｜　操作ボタン：B

                    //３つ目　操作キー：C　｜　操作ボタン：X

                    //４つ目　操作キー：V　｜　操作ボタン：Y

                    //必殺技

                }
                */
            }
        }
    }
//--------------------------------------------------------------------------------------
}