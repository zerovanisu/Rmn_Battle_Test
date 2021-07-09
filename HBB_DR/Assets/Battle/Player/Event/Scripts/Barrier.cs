//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系

    public GameObject em;   //Event_Managerの中にあるバリアのprotectionの数を減らすために参照するよ

//--------------------------------------------------------------------------------------
//バリアの処理

    private void Update()
    {
        //バリアを守る回数のProtectionが０だったら
        if (em.GetComponent<Event_Manager>().Protection == 0)
        {
            em.GetComponent<Event_Manager>().defense = false;    //バリアを解除するよ
            this.gameObject.SetActive(false);   //バリアをオフにするよ
        }
    }

//--------------------------------------------------------------------------------------
//バリアの処理

    void OnTriggerEnter2D(Collider2D BD)
    {
        if (BD.gameObject.tag == "Bullet_1" || BD.gameObject.tag == "Bullet_2" || BD.gameObject.tag == "Bullet_3")
        {

            em.GetComponent<Event_Manager>().Protection--;   //Protection（残りの守る回数）をマイナス
        }
    }

//--------------------------------------------------------------------------------------

}
