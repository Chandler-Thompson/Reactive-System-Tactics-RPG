using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
    	yield return new WaitForSeconds(3f);
    	GameObject.Find("Game Controller").GetComponent<GameController>().sceneController.LoadNextScene();
    }

}
