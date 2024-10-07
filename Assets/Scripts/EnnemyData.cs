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
    public float knockbackForce;

    // Méthode pour cloner ce ScriptableObject
    public EnnemyData Clone()
    {
        EnnemyData clone = ScriptableObject.CreateInstance<EnnemyData>();
        clone.Ennemy_Life = this.Ennemy_Life;
        clone.Ennemy_AttackWindow = this.Ennemy_AttackWindow;
        clone.Ennemy_Damage = this.Ennemy_Damage;
        clone.Ennemy_Speed = this.Ennemy_Speed;
        clone.knockbackForce = this.knockbackForce;
        return clone;
    }
}