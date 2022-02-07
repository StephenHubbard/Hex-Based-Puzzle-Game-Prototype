using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] public int currentAvailablePopulation;
    [SerializeField] private int totalPopulation;

    [SerializeField] private TMP_Text populationText;

    private void Update() {
        UpdatePopText();
    }

    private void UpdatePopText() {
        populationText.text = currentAvailablePopulation.ToString() + " / " + totalPopulation.ToString();
    }

    public void IncreasePopulation() {
        currentAvailablePopulation++;
        totalPopulation++;
    }

    public void UseOnePopulation() {
        currentAvailablePopulation--;
    }

    public int GetCurrentPopulation() {
        return currentAvailablePopulation;
    }
}
