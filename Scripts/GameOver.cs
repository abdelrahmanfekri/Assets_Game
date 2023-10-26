using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// tmp text
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text score_text;
    void Start()
    {
        string data = PlayerPrefs.GetString("data");
        score_text.text = "Score: " + data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
