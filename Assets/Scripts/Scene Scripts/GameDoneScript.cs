using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDoneScript : MonoBehaviour
{

	public Text gameDoneText;
	public string gameWonText;
	public string gameLostText;

	public Image orb1;
    public Image orb2;
    public Image orb3;
    public Image orb4;
    public Image orb5;

    public Sprite empty; // I attched these from editor
    public Sprite won;
    public Sprite lost;

	GameController gc;
	int finalScore = 0;

	void Awake()
	{
		List<int> finalScoreList = PlayerPrefsController.GetIntList(SavedData.LevelScores);
		foreach(int score in finalScoreList)
		{
			finalScore += score;
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        if(finalScore > 0)
        {
        	gameDoneText.text = gameWonText;
        }
        else if(finalScore < 0)
        {
        	gameDoneText.text = gameLostText;
        }
        else
        {
        	gameDoneText.text = "How...how did you do this?! You didn't have any Dreams OR Nightmares...";
        }

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
                    orb3.sprite = empty;
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
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("Game Controller").GetComponent<GameController>().sceneController.Reset();
    }

    void Update()
    {
    	if(gc == null)
    	{
    		gc = GameObject.Find("Game Controller").GetComponent<GameController>();
    	}
    }

    public void MainMenu()
    {
        Debug.Log("[GameDoneScript] boop.");
        gc.sceneController.Reset();
    }
   

}
