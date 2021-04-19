using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : ScriptableObject 
{
	public List<Vector3> tiles;
	public List<Vector3> playerSpawns;
	public List<Vector3> cpuSpawns;
}
