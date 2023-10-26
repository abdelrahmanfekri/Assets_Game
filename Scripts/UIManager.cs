using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ui text
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // create ui text variables
    public TMP_Text score_text;
    public TMP_Text red_energy_text;
    public TMP_Text green_energy_text;
    public TMP_Text blue_energy_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateScore(int score)
    {
        score_text.text = "Score: " + score;
    }

    void updateRedEnergy(int energy)
    {
        red_energy_text.text = "Red Energy: " + energy;
    }

    void updateGreenEnergy(int energy)
    {
        green_energy_text.text = "Green Energy: " + energy;
    }

    void updateBlueEnergy(int energy)
    {
        blue_energy_text.text = "Blue Energy: " + energy;
    }

}
