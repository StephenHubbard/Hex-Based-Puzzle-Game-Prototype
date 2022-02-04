using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    private int currentTurn = 1;

    [SerializeField] private ResourceManager resourceManager;


    public void EndTurnButtonFn() {
        currentTurn++;
        resourceManager.EndTurnResourceCalc();
    }
}
