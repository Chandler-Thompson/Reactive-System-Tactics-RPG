using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public SceneController sceneController;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		//TODO: Implement full save/load system and real initializations for game instead of this
 		// "Save" battle data before being loaded by the Battle Controller
		LoadBattle1();

	}

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneController.GetCurrentScene().Equals("Transition Scene"))
        {

        }
    }

    void LoadBattle1()
    {
    	PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, 1);
    	PlayerPrefsController.StoreString(SavedData.CurrLevelName, "Level_3");

		List<string> unitRecipes = new List<string> {"Sleep Shepherd", "Sleep Sheep", "Sleep Demon", "Sleep Demon", "Sleep Demon"};
		PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes, unitRecipes);

		List<int> unitLevels = new List<int> {1,1,1,1,1};
		PlayerPrefsController.StoreIntList(SavedData.CurrLevelUnitLevels, unitLevels);
    }

    void LoadBattle2()
    {
    	PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, 2);
    	PlayerPrefsController.StoreString(SavedData.CurrLevelName, "Level_3");

    	List<string> unitRecipes = new List<string>();
    	List<int> unitLevels = new List<int>();

    	for(int i = 0; i < )

    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes);
    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitLevels);
    }

    void LoadBattle3()
    {
    	PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, 3);
    	PlayerPrefsController.StoreString(SavedData.CurrLevelName, "Level_3");

    	List<string> unitRecipes = new List<string>();
    	List<int> unitLevels = new List<int>();

    	for(int i = 0; i < )

    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes);
    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitLevels);
    }

    void LoadBattle4()
    {
    	PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, 4);
    	PlayerPrefsController.StoreString(SavedData.CurrLevelName, "Level_3");

    	List<string> unitRecipes = new List<string>();
    	List<int> unitLevels = new List<int>();

    	for(int i = 0; i < )

    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes);
    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitLevels);
    }

    void LoadBattle5()
    {
    	PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, 5);
    	PlayerPrefsController.StoreString(SavedData.CurrLevelName, "Level_3");

    	List<string> unitRecipes = new List<string>();
    	List<int> unitLevels = new List<int>();

    	for(int i = 0; i < )

    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes);
    	PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitLevels);
    }

}
