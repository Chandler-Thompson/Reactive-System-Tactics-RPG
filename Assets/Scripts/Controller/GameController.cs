using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public SceneController sceneController;

	bool loaded = true;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		//TODO: Implement full save/load system and real initializations for game instead of this
 		// "Save" battle data before being loaded by the Battle Controller

	}

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("tick");
        if(sceneController.GetCurrentScene().Equals("Transition Scene") && !loaded)
        {
        	int currLevel = PlayerPrefsController.GetInt(SavedData.CurrLevelNum);

            //"Conversations/IntroScene", "Conversations/OutroSceneWin", "Conversations/OutroSceneLose"
        	switch(currLevel)
        	{
                case 0: // Battle 1
                    LoadBattle(3, 2, "Level_3", "", "", "");
                    break;
        		case 1: // Battle 2
        			LoadBattle(10, 2, "Battle_2", "", "", "");
        			break;
        		case 2: // Battle 3
        			LoadBattle(10, 3, "Battle_3", "", "", "");
        			break;
    			case 3: // Battle 4
    				LoadBattle(10, 4, "Battle_4", "", "", "");
    				break;
    			case 4: // Battle 5
    				LoadBattle(10, 5, "Battle_5", "", "", "");
    				break;
    			case 5:
    				LoadWinningContent();
    				break;
    			default:
    				Debug.Log("[GameController] Unrecognizable level number stored.");
                    break;
        	}
        }
        else
        {
        	loaded = false;
        }
    }

    void LoadBattle(int numSleepDemons, int levelNum, string levelName, string introConvo, string outroConvoWin, string outroConvoLose)
    {
    	PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, levelNum);
    	PlayerPrefsController.StoreString(SavedData.CurrLevelName, levelName);

    	List<string> unitRecipes = new List<string>();
    	List<int> unitLevels = new List<int>();

    	unitRecipes.Add("Sleep Shepherd");
    	unitRecipes.Add("Sleep Sheep");

    	unitLevels.Add(1);
    	unitLevels.Add(1);

    	for(int i = 0; i < numSleepDemons; i++)
    	{
    		unitRecipes.Add("Sleep Demon");
    		unitLevels.Add(1);
    	}

    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes, unitRecipes);
    	PlayerPrefsController.StoreIntList(SavedData.CurrLevelUnitLevels, unitLevels);

    	PlayerPrefsController.StoreString(SavedData.IntroConvo, introConvo);
    	PlayerPrefsController.StoreString(SavedData.OutroConvoWin, outroConvoWin);
        PlayerPrefsController.StoreString(SavedData.OutroConvoLose, outroConvoLose);
    }

    void LoadWinningContent()
    {
    	//Nothing as of yet...
    }

}
