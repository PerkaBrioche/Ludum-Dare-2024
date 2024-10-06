using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkManager : MonoBehaviour
{
    public static InkManager Instance;
    public SmoothSlider SmoothSlider;
    private int ActualPalier;
    public List<int> Palier;
    public int Ink;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SmoothSlider.NewValue(Palier[ActualPalier+1], 0);
    }


    public void GainInk(int InkValue)
    {
        Ink += InkValue;
    }

    private void Update()
    {
        if (Ink > Palier[ActualPalier+1])
        {
            NewPAlier();
        }
        else if(Ink < Palier[ActualPalier])
        {
            ReducePalier();
        }
    }

    public void NewPAlier()
    {
        ActualPalier++;
        SmoothSlider.NewValue(Palier[ActualPalier+1], Ink);
        BallManager.Instance.SpawnBall();
    }

    public void ReducePalier(bool fall = false)
    {
        if (ActualPalier == 0)
        { return; }

        if (fall)
        {
            Ink = Palier[ActualPalier] -Palier[ActualPalier-1];
            print(ActualPalier + " - " +(ActualPalier-1));
        }

        ActualPalier--;
        SmoothSlider.NewValue(Palier[ActualPalier+1], Ink);
        
        if(fall){return;}
        BallManager.Instance.DestroyBall();
    }
}
