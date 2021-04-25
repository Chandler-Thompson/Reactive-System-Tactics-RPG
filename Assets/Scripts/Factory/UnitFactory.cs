using UnityEngine;
using System.IO;
using System.Collections;

public static class UnitFactory
{
	#region Public
	public static GameObject Create (string name, int level)
	{
		UnitRecipe recipe = Resources.Load<UnitRecipe>("Unit Recipes/" + name);
		if (recipe == null)
		{
			Debug.LogError("No Unit Recipe for name: " + name);
			return null;
		}
		return Create(recipe, level);
	}

	public static GameObject Create (UnitRecipe recipe, int level)
	{
		GameObject obj = InstantiatePrefab("Units/" + recipe.model);
		obj.name = recipe.name;
		obj.AddComponent<Unit>();
		AddStats(obj);
		AddLocomotion(obj, recipe.locomotion);
		obj.AddComponent<Status>();
		obj.AddComponent<Equipment>();
		AddJob(obj, recipe.job);
		AddRank(obj, level);
		obj.AddComponent<Health>();
		obj.AddComponent<Mana>();
		AddAttack(obj, recipe.attack);
		AddAbilityCatalog(obj, recipe.abilityCatalog);
		AddAlliance(obj, recipe.alliance);
		AddAttackPattern(obj, recipe.strategy);
		return obj;
	}

	public static GameObject Replace (Unit unit, string changeTo)
	{
		UnitRecipe recipe = Resources.Load<UnitRecipe>("Unit Recipes/" + changeTo);
		if (recipe == null)
		{
			Debug.LogError("No Unit Recipe for name: " + changeTo);
			return null;
		}
		return Replace(unit, recipe);
	}

	public static GameObject Replace (Unit unit, UnitRecipe changeTo)
	{
		int level = unit.transform.parent.GetComponentInChildren<Stats>()[StatTypes.LVL];
		GameObject instance = Create(changeTo, level);

		Unit newUnit = instance.GetComponent<Unit>();

		Situate(newUnit, unit.tile, unit.dir);

		GameObject bcObj = GameObject.Find("Battle Controller");
		BattleController bc = bcObj.GetComponent<BattleController>();

		bc.units.Remove(unit);

		GameObject unitGameObject = unit.transform.gameObject;
		GameObject.Destroy(unitGameObject);

		return instance;
	}

	public static void Situate (Unit unit, Tile tile, Directions facingDir)
	{
		GameObject instance = unit.gameObject;
		GameObject unitContainer = GameObject.Find("Units");
		instance.transform.SetParent(unitContainer.transform);

		unit.Place(tile);
		unit.dir = facingDir;
		unit.Match();
			
		GameObject bcObj = GameObject.Find("Battle Controller");
		BattleController bc = bcObj.GetComponent<BattleController>();
		bc.units.Add(unit);

	}
	#endregion

	#region Private
	static GameObject InstantiatePrefab (string name)
	{
		GameObject prefab = Resources.Load<GameObject>(name);
		if (prefab == null)
		{
			Debug.LogError("No Prefab for name: " + name);
			return new GameObject(name);
		}
		GameObject instance = GameObject.Instantiate(prefab);
		instance.name = instance.name.Replace("(Clone)", "");
		return instance;
	}

	static void AddStats (GameObject obj)
	{
		Stats s = obj.AddComponent<Stats>();
		s.SetValue(StatTypes.LVL, 1, false);
	}

	static void AddJob (GameObject obj, string name)
	{
		GameObject instance = InstantiatePrefab("Jobs/" + name);
		instance.transform.SetParent(obj.transform);
		Job job = instance.GetComponent<Job>();
		job.Employ();
		job.LoadDefaultStats();
	}

	static void AddLocomotion (GameObject obj, Locomotions type)
	{
		switch (type)
		{
		case Locomotions.Walk:
			obj.AddComponent<WalkMovement>();
			break;
		case Locomotions.Fly:
			obj.AddComponent<FlyMovement>();
			break;
		case Locomotions.Teleport:
			obj.AddComponent<TeleportMovement>();
			break;
		}
	}

	static void AddAlliance (GameObject obj, Alliances type)
	{
		Alliance alliance = obj.AddComponent<Alliance>();
		alliance.type = type;
	}

	static void AddRank (GameObject obj, int level)
	{
		Rank rank = obj.AddComponent<Rank>();
		rank.Init(level);
	}

	static void AddAttack (GameObject obj, string name)
	{
		GameObject instance = InstantiatePrefab("Abilities/" + name);
		instance.transform.SetParent(obj.transform);
	}

	static void AddAbilityCatalog (GameObject obj, string name)
	{
		GameObject main = new GameObject("Ability Catalog");
		main.transform.SetParent(obj.transform);
		main.AddComponent<AbilityCatalog>();

		AbilityCatalogRecipe recipe = Resources.Load<AbilityCatalogRecipe>("Ability Catalog Recipes/" + name);
		if (recipe == null)
		{
			Debug.LogError("No Ability Catalog Recipe Found: " + name);
			return;
		}

		for (int i = 0; i < recipe.categories.Length; ++i)
		{
			GameObject category = new GameObject( recipe.categories[i].name );
			category.transform.SetParent(main.transform);

			for (int j = 0; j < recipe.categories[i].entries.Length; ++j)
			{
				string abilityName = string.Format("Abilities/{0}/{1}", recipe.categories[i].name, recipe.categories[i].entries[j]);
				GameObject ability = InstantiatePrefab(abilityName);
				ability.transform.SetParent(category.transform);
			}
		}
	}

	static void AddAttackPattern (GameObject obj, string name)
	{
		Driver driver = obj.AddComponent<Driver>();
		if (string.IsNullOrEmpty(name)){
			driver.normal = Drivers.Human;
		}else{
			driver.normal = Drivers.Computer;
			GameObject instance = InstantiatePrefab("Attack Pattern/" + name);
			instance.transform.SetParent(obj.transform);
		}
	}
	#endregion
}