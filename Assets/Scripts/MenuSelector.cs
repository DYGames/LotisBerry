using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i < 7; ++i)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Context.userInterface.BuyTower(i + 1);
            }
        }
    }
}
