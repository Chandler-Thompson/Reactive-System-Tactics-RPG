using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public Image orb1;
    public Image orb2;
    public Image orb3;
    public Image orb4;
    public Image orb5;

    public Sprite empty; // I attched these from editor
    public Sprite won;
    public Sprite lost;


    void Start()
    {
        StartCoroutine(Loading());
        List<int> levelScores = PlayerPrefsController.GetIntList(SavedData.LevelScores);

        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                if (levelScores[i] == -1)
                {
                    orb1.sprite = lost;
                }
                if (levelScores[i] == 0)
                {
                    orb1.sprite = empty;
                }
                if (levelScores[i] == 1)
                {
                    orb1.sprite = won;
                }
            }
            if (i == 1)
            {
                if (levelScores[i] == -1)
                {
                    orb2.sprite = lost;
                }
                if (levelScores[i] == 0)
                {
                    orb2.sprite = empty;
                }
                if (levelScores[i] == 1)
                { 
                    orb2.sprite = won;
                }
            }
            if (i == 2)
            {
                if (levelScores[i] == -1)
                {
                    orb3.sprite = lost;
                }
                if (levelScores[i] == 0)
                {
                    orb3sprite = empty;
                }
                if (levelScores[i] == 1)
                {
                    orb3.sprite = won;
                }
            }
            if (i == 3)
            {
                if (levelScores[i] == -1)
                {
                    orb4.sprite = lost;
                }
                if (levelScores[i] == 0)
                {
                    orb4.sprite = empty;
                }
                if (levelScores[i] == 1)
                {
                    orb4.sprite = won;
                }
            }
            if (i == 4)
            {
                if (levelScores[i] == -1)
                {
                    orb5.sprite = lost;
                }
                if (levelScores[i] == 0)
                {
                    orb5.sprite = empty;
                }
                if (levelScores[i] == 1)
                {
                    orb5.sprite = won;
                }
            }
        }

    }

    IEnumerator Loading()
    {
    	yield return new WaitForSeconds(3f);
    	GameObject.Find("Game Controller").GetComponent<GameController>().sceneController.LoadNextScene();
    }

}
