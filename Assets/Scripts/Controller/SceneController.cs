using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

	//GameObject transition;

	public string GetCurrentScene()
	{
		return SceneManager.GetActiveScene().name;
	}

    public void LoadScene(string sceneName)
    {
    	SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
    	if(!GetCurrentScene().Equals("Transition Scene"))
    	{
    		Debug.Log("[SceneController] Loading Transition Scene...");
    		LoadScene("Transition Scene");
    	}
    	else
    	{
    		Debug.Log("[SceneController] Loading Next Battle Scene...");
    		LoadNextBattle();
    		LoadScene("Battle Testing");
    	}
    }

    public void LoadNextBattle(){
        int currLevel = PlayerPrefsController.GetInt(SavedData.CurrLevelNum);

        //"Conversations/IntroScene", "Conversations/OutroSceneWin", "Conversations/OutroSceneLose"
        switch(currLevel)
        {
            case 0: // Battle 1 NREM Stage 1
                LoadBattle(3, 1, "NREM Stage 1", "", "", "");
                break;
            case 1: // Battle 2
                LoadBattle(10, 2, "NREM Stage 2", "", "", "");
                break;
            case 2: // Battle 3
                LoadBattle(10, 3, "NREM Stage 3", "", "", "");
                break;
            case 3: // Battle 4
                LoadBattle(10, 4, "REM Sleep", "", "", "");
                break;
            case 4: // Battle 5
                LoadBattle(10, 5, "Dreamscape", "", "", "");
                break;
            case 5:
                LoadWinningContent();
                break;
            default:
                Debug.Log("[GameController] Unrecognizable level number stored.");
                break;
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
