using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManger : MonoBehaviour
{
    public static ScaleManger Instance;

    public EnnemyData FantassinDataOriginal; // Référence vers l'original
    public EnnemyData EyesDataOriginal; // Référence vers l'original

    private EnnemyData FantassinData; // Instance temporaire
    private EnnemyData EyesData; // Instance temporaire

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        FantassinData = FantassinDataOriginal.Clone();
        EyesData = EyesDataOriginal.Clone();
        UpdateScale();
    }

    public void UpdateScale()
    {
        FantassinData.Ennemy_Life += 2;
        FantassinData.Ennemy_Damage++;        
        EyesData.Ennemy_Life += 2;
        EyesData.Ennemy_Damage++;
    }
    public EnnemyData GetFantassinData()
    {
        return FantassinData;
    }
    public EnnemyData GetEyesData()
    {
        return EyesData;
    }
    
}