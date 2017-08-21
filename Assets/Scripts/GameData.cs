using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField]
    private int _Money;
    public int Money
    {
        get
        {
            return _Money;
        }
        set
        {
            Context.userInterface.MoneyText.text = value.ToString();
            _Money = value;
        }
    }
    private void Start()
    {
        Context.gameData = this;
        Money = 1000;
    }
}
