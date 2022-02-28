using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateHandPoker : PlayerPoker5
{
    private int heartsSum;
    private int diamondSum;
    private int clubSum;
    private int spadesSum;
    public GameObject[] currentHand;

    private void FixedUpdate() { 
        currentHand = GetHand();
    }

    public void EvaluateHand()
    {
        //GetNumberOfSuit();
    }

}
