using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HideUI : MonoBehaviour
{
    public GameObject UI;
    public GameObject button;

    private Boolean showing = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPressed()
    {
        if(showing == true)
        {
            showing = false;
            UI.SetActive(false);
            button.GetComponentInChildren<Text>().text = "Show";

        }
        else if(showing == false)
        {
            showing = true;
            UI.SetActive(true);
            button.GetComponentInChildren<Text>().text = "Hide";
        }
    }
}
