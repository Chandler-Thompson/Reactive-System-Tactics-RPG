using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine 
{
	public bool isTesting = false;
	public LevelData levelData;
	public List<string> unitRecipes;
	public List<int> unitLevels;

	public CameraRig cameraRig;
	public Board board;
	public Transform tileSelectionIndicator;
	public Point pos;
	public Tile currentTile { get { return board.GetTile(pos); }}
	public AbilityMenuPanelController abilityMenuPanelController;
	public StatPanelController statPanelController;
	public HitSuccessIndicator hitSuccessIndicator;
	public BattleMessageController battleMessageController;
	public FacingIndicator facingIndicator;
	public Turn turn = new Turn();
	public List<Unit> units = new List<Unit>();
	public IEnumerator round;
	public ComputerPlayer cpu;

	public UnitSpawner spawner;

	void Start ()
	{

		LoadBattleData();

		if(isTesting){
			ChangeState<InitBattleTestState>();
		}else{
			ChangeState<InitBattleState>();
		}
	}

	void LoadBattleData(){
		
		string levelName = PlayerPrefsController.GetString(SavedData.CurrLevelName);
		levelData = Resources.Load("Levels/"+levelName) as LevelData;

		unitRecipes = PlayerPrefsController.GetStringList(SavedData.CurrLevelUnitRecipes);
		unitLevels = PlayerPrefsController.GetIntList(SavedData.CurrLevelUnitLevels);

	}

	//NOTE: ONLY FOR TESTING PURPOSES!!!
	public void LoadNextLevel(int score)
	{
		int currLevel = PlayerPrefsController.GetInt(SavedData.CurrLevelNum);
		List<int> levelScores = PlayerPrefsController.GetIntList(SavedData.LevelScores);
		levelScores[currLevel] = score;
		PlayerPrefsController.StoreIntList(SavedData.LevelScores, levelScores);

		GameObject.Find("Game Controller").GetComponent<GameController>().sceneController.LoadNextScene();
	}

}