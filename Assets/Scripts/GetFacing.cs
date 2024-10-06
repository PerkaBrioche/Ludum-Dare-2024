using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFacing : MonoBehaviour
{
    private PlayerController PlayerController;

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
    }

    public enum MyEnum
    {
        Looking_Up,
        
        Looking_UpLeft,
        Looking_Left,
        
        Looking_DownLeft,
        Looking_Down,
        
        Looking_DownRight,
        Looking_Right,
        
        Looking_UpRight,
        Looking_Null,
    }
    public int ReturnFaceIndex()
    {
        return (int)GetFace();
    }
    public MyEnum GetFace()
    {
        var PlayerVector = PlayerController.VECTOR2_movement;
        switch (PlayerVector.x, PlayerVector.y)
        {
            case (1,1):
                return MyEnum.Looking_UpRight;
            case (-1,-1):
                return MyEnum.Looking_DownLeft;
            case (1,-1):
                return MyEnum.Looking_DownRight;
            case (-1,1):
                return MyEnum.Looking_UpLeft;
            
            case (1,0):
                return MyEnum.Looking_Right;
            case (-1,0):
                return MyEnum.Looking_Left;
            case (0,1):
                return MyEnum.Looking_Up;
            case (0,-1):
                return MyEnum.Looking_Down;
        }
        return MyEnum.Looking_Null;
    }
}
