using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{

	string unitRecipe;
	int unitLevel;
	Tile spawnLocation;
	int numSpawns;

	bool active = false;

	int currSpawns = 0;
	GameObject currUnitInstance;

    public void init(string unitRecipe, int unitLevel, Tile spawnLocation, int numSpawns = 1)
    {
        this.unitRecipe = unitRecipe;
        this.unitLevel = unitLevel;
        this.spawnLocation = spawnLocation;
        this.numSpawns = numSpawns;
        this.active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(active && currUnitInstance == null && currSpawns < numSpawns)
        {
        	if(spawnLocation.content == null)
        	{
	        	currUnitInstance = UnitFactory.Create(unitRecipe, unitLevel);
	        	Unit unit = currUnitInstance.GetComponent<Unit>();
	        	UnitFactory.Situate(unit, spawnLocation, Directions.West);

	        	currSpawns += 1;
	        }
	        else
	        {
	        	Debug.Log("Location not empty! "+spawnLocation.content.name);
	        }
        }
    }
}
