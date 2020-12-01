using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceBehaviour : MonoBehaviour
{

    public AllScoreBehaviour score;
    [Header("Basic UI")]
    public Text buildingName;
    public GameObject background;

    [Header("Buildings upgrade")]
    public Text moneyLabel;
    public Text peopleLabel, electroLabel;

    [Header("Building Mode Buttons")]
    public GameObject bcg;

    [Header("DeV")]
    public Text message;


    public void MessageString(string text)
    {
        message.text += '\n' + text;
    }
    // Start is called before the first frame update
    public void QuitBuildMenu()
    {
        bcg.SetActive(false);
    }

    public void EnterBuildMenu()
    {
        bcg.SetActive(true);
    }

    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
