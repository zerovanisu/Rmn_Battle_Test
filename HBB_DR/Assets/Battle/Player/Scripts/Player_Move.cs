using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系

    GameObject Syouhai;     //勝敗がついているか否かをチェックするための変数だよ
    GameObject player_l1;   //移動用キャラを格納する変数だよ
    GameObject player_l2;   //移動用キャラの格納する変数だよ
    Player_Manager_L pl;    //キャラクターの位置を代入する変数だよ
    private Rigidbody2D rd; //弾のRigidbody2Dを格納する変数だよ

//--------------------------------------------------------------------------------------
//変数系

    public int Character_Speed; //参照したキャラクタースピードの確認
    [SerializeField]
    private float player_speed; //キャラクターの速度を代入するよ
    [SerializeField]
    public float maxY, maxX, minY, minX;    //キャラクターの移動できる幅に制限をつけるよ

//--------------------------------------------------------------------------------------
//最初の準備

    void Start()
    {
        Syouhai = GameObject.Find("Game_Setting");     //勝敗Boolを取得するよ
        rd = GetComponent<Rigidbody2D>();   //弾についているRigidbody2Dを入れるよ
        #region 移動範囲の設定
        if (this.gameObject.CompareTag("Player_L1"))
        {
            maxY = -446f; minY = -725f; maxX = 1335f; minX = 460f;  //L1キャラクターの移動できる範囲だよ
        }
        else if (this.gameObject.CompareTag("Player_R1"))
        {
            maxY = 725f; minY = -730f; maxX = -460f; minX = -1335f;  //R1キャラクターの移動できる範囲だよ
        }
        else if (this.gameObject.CompareTag("Player_R2"))
        {
            maxY = 725f; minY = -730f; maxX = 1335f; minX = 460f;  //R2キャラクターの移動できる範囲だよ
        }
        else if (this.gameObject.CompareTag("Player_L2"))
        {
            maxY = 725f; minY = 446f; maxX = -460f; minX = -1335f;  //L2キャラクターの移動できる範囲だよ
        }
        if (this.gameObject.CompareTag("Player_L1") || this.gameObject.CompareTag("Player_L2"))
        {
            Character_Speed = gameObject.GetComponent<Player_Manager_L>().Character;    //キャラクターの速度を取得するよ
        }
        else if(this.gameObject.CompareTag("Player_R1"))
        {
            pl = GameObject.Find("Player_L1").GetComponent<Player_Manager_L>();    //キャラクターの速度を取得するよ
            Character_Speed = pl.Character;    //キャラクターの速度と一緒にするよ
        }
        else if (this.gameObject.CompareTag("Player_R2"))
        {
            pl = GameObject.Find("Player_L2").GetComponent<Player_Manager_L>();    //キャラクターの速度を取得するよ
            Character_Speed = pl.Character;    //キャラクターの速度と一緒にするよ
        }
        #endregion
        #region キャラクターごとのスピード設定
        //キャラクタースピード設定
        if (Character_Speed == 1)
        {
            player_speed = 500;
        }
        else if (Character_Speed == 2)
        {
            player_speed = 1500;
        }
        else if (Character_Speed == 3)
        {
            player_speed = 1000;
        }
        #endregion
    }

    //--------------------------------------------------------------------------------------

    void Update()
    {
        //勝敗がついていなかった場合移動を許可
        if (!Syouhai.GetComponent<Setting>().syouhai)
        {
            if (this.gameObject.CompareTag("Player_L1"))
            {
                MovePlayer_L1();
            }
            else if (this.gameObject.CompareTag("Player_L2"))
            {
                MovePlayer_L2();
            }
            else if (this.gameObject.CompareTag("Player_R1"))
            {
                MovePlayer_R1();
            }
            else if (this.gameObject.CompareTag("Player_R2"))
            {
                MovePlayer_R2();
            }
        }
    }

//--------------------------------------------------------------------------------------

    void MovePlayer_L1()
    {
        //L1の移動
        #region もし上矢印キーが押されたら
        if (Input.GetAxisRaw("Vertical_L1") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            playerPos.y += player_speed * Time.deltaTime; //y座標にspeedを加算

            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxY < playerPos.y)
            {
                playerPos.y = maxY; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる

        }
        #endregion
        #region もし下矢印キーが押されたら
        else if (Input.GetAxisRaw("Vertical_L1") < 0)　
        {
            Vector3 playerPos = transform.position;
            //通常移動
            playerPos.y -= player_speed * Time.deltaTime;
            //追加
            if (minY > playerPos.y)
            {
                playerPos.y = minY;
            }
            transform.position = playerPos;
        }
        #endregion
        #region もし右矢印キーが押されたら
        if (Input.GetAxisRaw("Horizontal_L1") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //通常移動
            playerPos.x += player_speed * Time.deltaTime; //y座標にspeedを加算

            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxX < playerPos.x)
            {
                playerPos.x = maxX; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし左矢印キーが押されたら
        else if (Input.GetAxisRaw("Horizontal_L1") < 0)　
        {
            Vector3 playerPos = transform.position;
            playerPos.x -= player_speed * Time.deltaTime;

            //追加
            if (minX > playerPos.x)
            {
                playerPos.x = minX;
            }
            transform.position = playerPos;
        }
        #endregion
    }

//--------------------------------------------------------------------------------------

    void MovePlayer_L2()
    {
        //L2の移動
        #region もし上矢印キーが押されたら
        if (Input.GetAxisRaw("Vertical_L2") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //通常移動
            playerPos.y += player_speed * Time.deltaTime; //y座標にspeedを加算

            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxY < playerPos.y)
            {
                playerPos.y = maxY; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし下矢印キーが押されたら
        else if (Input.GetAxisRaw("Vertical_L2") < 0)
        {
            Vector3 playerPos = transform.position;
            //通常移動
            playerPos.y -= player_speed * Time.deltaTime;

            //追加
            if (minY > playerPos.y)
            {
                playerPos.y = minY;
            }
            transform.position = playerPos;
        }
        #endregion
        #region もし右矢印キーが押されたら
        if (Input.GetAxisRaw("Horizontal_L2") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //通常移動
            playerPos.x += player_speed * Time.deltaTime; //y座標にspeedを加算
            
            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxX < playerPos.x)
            {
                playerPos.x = maxX; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし左矢印キーが押されたら
        else if (Input.GetAxisRaw("Horizontal_L2") < 0)　
        {
            Vector3 playerPos = transform.position;
            //通常移動
            playerPos.x -= player_speed * Time.deltaTime;
            //追加
            if (minX > playerPos.x)
            {
                playerPos.x = minX;
            }
            transform.position = playerPos;
        }
        #endregion
    }

//--------------------------------------------------------------------------------------

    void MovePlayer_R1()
    {
        //R1の移動
        #region もし上矢印キーが押されたら
        if (Input.GetAxisRaw("Vertical_R1") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.y += (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.y += player_speed * Time.deltaTime; //y座標にspeedを加算
            }

            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxY < playerPos.y)
            {
                playerPos.y = maxY; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし下矢印キーが押されたら
        else if (Input.GetAxisRaw("Vertical_R1") < 0)
        {
            Vector3 playerPos = transform.position;
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.y -= (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.y -= player_speed * Time.deltaTime;
            }
            //追加
            if (minY > playerPos.y)
            {
                playerPos.y = minY;
            }
            transform.position = playerPos;
        }
        #endregion
        #region もし右矢印キーが押されたら
        if (Input.GetAxisRaw("Horizontal_R1") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.x += (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.x += player_speed * Time.deltaTime; //y座標にspeedを加算
            }
            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxX < playerPos.x)
            {
                playerPos.x = maxX; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし左矢印キーが押されたら
        else if (Input.GetAxisRaw("Horizontal_R1") < 0)
        {
            Vector3 playerPos = transform.position;
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.x -= (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.x -= player_speed * Time.deltaTime;
            }
            //追加
            if (minX > playerPos.x)
            {
                playerPos.x = minX;
            }
            transform.position = playerPos;
        }
        #endregion
    }

//--------------------------------------------------------------------------------------

    void MovePlayer_R2()
    {
        //R2の移動
        #region もし上矢印キーが押されたら
        if (Input.GetAxisRaw("Vertical_R2") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.y += (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.y += player_speed * Time.deltaTime; //y座標にspeedを加算
            }

            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxY < playerPos.y)
            {
                playerPos.y = maxY; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし下矢印キーが押されたら
        else if (Input.GetAxisRaw("Vertical_R2") < 0)
        {
            Vector3 playerPos = transform.position;
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.y -= (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.y -= player_speed * Time.deltaTime;
            }
            //追加
            if (minY > playerPos.y)
            {
                playerPos.y = minY;
            }
            transform.position = playerPos;
        }
        #endregion
        #region もし右矢印キーが押されたら
        if (Input.GetAxisRaw("Horizontal_R2") > 0)
        {
            Vector3 playerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.x += (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.x += player_speed * Time.deltaTime; //y座標にspeedを加算
            }
            //追加
            //もしplayerPosのY座標がmaxY（最大Y座標）より大きくなったら
            if (maxX < playerPos.x)
            {
                playerPos.x = maxX; //PlayerPosのY座標にmaxYを代入
            }
            transform.position = playerPos; //現在の位置情報に反映させる
        }
        #endregion
        #region もし左矢印キーが押されたら
        else if (Input.GetAxisRaw("Horizontal_R2") < 0)
        {
            Vector3 playerPos = transform.position;
            //減速移動
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerPos.x -= (player_speed * 0.4f) * Time.deltaTime; //y座標にspeedを加算
            }
            //通常移動
            else
            {
                playerPos.x -= player_speed * Time.deltaTime;
            }
            //追加
            if (minX > playerPos.x)
            {
                playerPos.x = minX;
            }
            transform.position = playerPos;
        }
        #endregion
    }

//--------------------------------------------------------------------------------------

}



