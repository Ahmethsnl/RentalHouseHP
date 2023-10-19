using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager myGameManager;

    private int myMoneyAmount;

    private void Awake()
    {
        myGameManager = this;
    }

    public bool IsThereAnyMoney(int amount)
    {
        return myMoneyAmount >= amount;
    }
}
