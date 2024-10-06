using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DATA", fileName = "EnnemyData")]
public class EnnemyData : ScriptableObject
{
    public float Ennemy_Life;
    public float Ennemy_AttackWindow;
    public int Ennemy_Damage;
    public float Ennemy_Speed;
    public float knockbackForce ;
}
