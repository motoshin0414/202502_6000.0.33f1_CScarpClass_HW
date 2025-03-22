using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trunset : MonoBehaviour
{
    public TextMeshProUGUI TrunOn;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI NewResultComponent = TrunOn.GetComponent<TextMeshProUGUI>();
        NewResultComponent.text =DifficultyButton.Trun.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
