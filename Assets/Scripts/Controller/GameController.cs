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
		PlayerPrefsController.StoreString(SavedData.CurrLevelName, "Level_3");

		List<string> unitRecipes = new List<string> {"Sleep Shepherd", "Sleep Sheep", "Sleep Demon", "Sleep Demon", "Sleep Demon"};
		PlayerPrefsController.StoreStringList(SavedData.CurrLevelUnitRecipes, unitRecipes);

		List<int> unitLevels = new List<int> {1,1,1,1,1};
		PlayerPrefsController.StoreIntList(SavedData.CurrLevelUnitLevels, unitLevels);

	}

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
