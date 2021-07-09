//ル
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{
//--------------------------------------------------------------------------------------
//参照系 

    [SerializeField]
    GameObject Barrier; //バリアのオブジェクトを入れるよ
    [SerializeField] 
    GameObject Absorption;  //吸収のオブジェクトを入れるよ

//--------------------------------------------------------------------------------------
//変数系

    public int barrier_number;    //バリアで守る回数を指定できるよ
    public int Protection;  //バリアで守る回数だよ
    
    public bool B_Switch;   //バリアがオンになっているか見るやつだよ
    public bool HPSwitch;   //吸収がオンになっているか見るやつだよ
    public bool defense = false;    //バリアをオンにするかどうかを操作するやつだよ

//--------------------------------------------------------------------------------------
//バリアと吸収のオンオフの処理

    void Update()
    {
        //バリアに関する処理をしているよ
        if(defense == true)
        {
            if(Protection <= 0)
            {
                Barrier.SetActive(false);   //バリアをオフにするよ
            }
        }
        else if (B_Switch)
        {
            if (Barrier.activeSelf == false)
            {
                Protection = barrier_number; //barrie_numberに入っている数字を正式に守る回数にするよ
                Barrier.SetActive(true);    //バリアをオンにするよ
                defense = true;     //バリアをオンにするよ
            }
        }
        else if (!B_Switch)
        {
            if (Barrier.activeSelf == true)
            {
                Barrier.SetActive(false);   //バリアをオフにするよ
            }
        }
        //吸収に関する処理をしているよ
        if (HPSwitch)
        {
            if (Absorption.activeSelf == false)
            {
                Absorption.SetActive(true); //吸収をオンにするよ
            }
        }
        else if (!HPSwitch || Protection == 0)  
        {
            if (Absorption.activeSelf == true)
            {
                Absorption.SetActive(false);    //吸収をオフにするよ
            }
        }
    }

//--------------------------------------------------------------------------------------

}
