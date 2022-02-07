using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShrineManager : MonoBehaviour
{
    [SerializeField] private int currentShrines;
    [SerializeField] private int totalShrinesNeeded;
    [SerializeField] private int shrineTimerTurnsLeft = 10;
    [SerializeField] private TMP_Text ShrinesAmountText;
    [SerializeField] private TMP_Text ShrinesCountdownText;

    private void Update() {
        UpdateText();
    }

    private void UpdateText() {
        ShrinesAmountText.text = currentShrines.ToString() + " / " + totalShrinesNeeded.ToString();
        ShrinesCountdownText.text = "Turns Left: " + shrineTimerTurnsLeft.ToString();
    }

    public void InceaseCurrentShrineCount() {
        currentShrines++;
    }

    public void InceaseTotalShrinesNeeded(int amount) {
        totalShrinesNeeded+= amount;
    }

    public int GetCurrentShrines() {
        return currentShrines;
    }

    public void ResetShrineTimer() {
        print("wizard tower placed");
        shrineTimerTurnsLeft += 10;
    }

    public void EndTurnShrineCounterReduce() {
        shrineTimerTurnsLeft--;
    }
}
