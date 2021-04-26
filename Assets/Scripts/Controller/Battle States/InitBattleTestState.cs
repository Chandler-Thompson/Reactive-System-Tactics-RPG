using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitBattleTestState : BattleState 
{

	List<Tile> playerLocations;
	List<Tile> cpuLocations;

	public override void Enter ()
	{
		base.Enter ();
		StartCoroutine(Init());
	}
	
	IEnumerator Init ()
	{
		board.Load( levelData );

		playerLocations = new List<Tile>(board.playerSpawns.Values);
		cpuLocations = new List<Tile>(board.cpuSpawns.Values);

		Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
		SelectTile(p);
		SpawnUnits();
		AddVictoryCondition();

		owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
		Debug.Log("[InitBattleTestState] Finished initializing steps...yielding...");
		yield return null;
		
		if(PlayerPrefsController.GetString(SavedData.IntroConvo).Equals(""))
		{
			Debug.Log("[InitBattleTestState] Moving to SelectUnitState...");
			owner.ChangeState<SelectUnitState>();
		}
		else
		{
			Debug.Log("[InitBattleTestState] Moving to CutSceneState...");
			owner.ChangeState<CutSceneState>();
		}
	}
	
	void SpawnUnits()
	{

		if (unitRecipes.Count != unitLevels.Count)
			Debug.LogError("[InitBattleTestState] (SpawnUnits): Number of recipes does not equal number of levels given.");

		GameObject unitContainer = new GameObject("Units");
		unitContainer.transform.SetParent(owner.transform);
		
		for (int i = 0; i < unitRecipes.Count; ++i)
		{

			int level = unitLevels[i];

			GameObject instance = UnitFactory.Create(unitRecipes[i], level);
			Alliances allianceType = instance.GetComponent<Alliance>().type;
			Tile spawnTile = SpawnUnit(allianceType);
			
			if(allianceType == Alliances.Hero)
			{
				instance.transform.SetParent(unitContainer.transform);

				Unit unit = instance.GetComponent<Unit>();
				unit.Place( spawnTile );
				unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
				unit.Match();
				
				units.Add(unit);
			}
			else
			{
				//TODO: Dont need to be making instance just to check alliance then destroying it...
				Destroy(instance); // instance not needed, as we are making a spawner instead
				GameObject spawnerInstance = Instantiate(owner.spawner.gameObject);
				UnitSpawner spawner = spawnerInstance.GetComponent<UnitSpawner>();
				spawner.init(unitRecipes[i], level, spawnTile, 1);
			}
			
		}
		
		SelectTile(units[0].tile.pos);
	}

	private Tile SpawnUnit(Alliances alliance)
	{
		Tile spawnTile = null;

		if(alliance == Alliances.Hero){
			if(playerLocations.Count != 0){
				spawnTile = playerLocations[0];
				playerLocations.RemoveAt(0);
			}else{
				Debug.LogError("[InitBattleTestState] (SpawnUnit): No more locations to spawn player\'s unit.");
			}
		}else{
			if(cpuLocations.Count != 0){
				spawnTile = cpuLocations[0];
				cpuLocations.RemoveAt(0);
			}else{
				Debug.LogError("[InitBattleTestState] (SpawnUnit): No more locations to spawn cpu\'s unit.");
			}
		}
		return spawnTile;
	}
	
	void AddVictoryCondition ()
	{
		//DefeatAllEnemiesVictoryCondition vc = owner.gameObject.AddComponent<DefeatAllEnemiesVictoryCondition>();
		
		SleepScapeVictoryCondition vc = owner.gameObject.AddComponent<SleepScapeVictoryCondition>();
		foreach(Unit unit in units)
		{
			if (unit.transform.gameObject.name.Equals("Sleep Shepherd"))
			{
				vc.hero = unit;
				return;
			}
		}
		
		
		// DefeatTargetVictoryCondition vc = owner.gameObject.AddComponent<DefeatTargetVictoryCondition>();
		// Unit enemy = units[ units.Count - 1 ];
		// vc.target = enemy;
		// Health health = enemy.GetComponent<Health>();
		// health.MinHP = 10;
	}
}