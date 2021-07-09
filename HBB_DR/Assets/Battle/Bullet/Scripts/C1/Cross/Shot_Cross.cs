//ƒ‹
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Cross : Shot_Common
{
//--------------------------------------------------------------------------------------
//•Ï”Œn

    public float rotation = 0.4f; //’e‚Ì‰ñ“]‚·‚é—Ê‚¾‚æ

    public bool is_direction;   //’e‚Ì‰ñ“]‚·‚é•ûŒü‚ğ¦‚·‚æ@trueF‰E,false:¶

//--------------------------------------------------------------------------------------
//¶‰E‚ÌˆÚ“®

    void Update()
    {
        #region is_direction‚É‡‚í‚¹‚Ä•ûŒü‚ğ•Ï‚¦‚éˆ—
        {
            if (is_direction)
            {
                transform.Rotate(new Vector3(0, 0, rotation));
            }
            else if (!is_direction)
            {
                transform.Rotate(new Vector3(0, 0, -rotation));
            }
        }
        #endregion
        transform.Translate(0, bullet_Speed * 0.01f, 0);    //’e‚ğ”­Ë‚·‚é‚æ
    }

//--------------------------------------------------------------------------------------
}
