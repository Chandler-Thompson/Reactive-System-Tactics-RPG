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
        PlayerPrefsController.StoreInt(SavedData.CurrLevelNum, 0);

	}

}
