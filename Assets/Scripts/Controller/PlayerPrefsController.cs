using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

	public static void StoreInt(SavedData dataKey, int saveData)
	{
		PlayerPrefs.SetInt(dataKey.Value, saveData);
	}

	public static int GetInt(SavedData dataKey)
	{
		return PlayerPrefs.GetInt(dataKey.Value);
	}

	public static void StoreString(SavedData dataKey, string saveData)
	{
		PlayerPrefs.SetString(dataKey.Value, saveData);
	}

	public static string GetString(SavedData dataKey)
	{
		return PlayerPrefs.GetString(dataKey.Value);
	}

	public static void StoreStringList(SavedData dataKey, List<string> saveData)
	{

		string dataString = "";
		foreach (string element in saveData){
			dataString += element+",";
		}

		dataString = dataString.Remove(dataString.Length-1, 1);//remove trailing ','

		PlayerPrefs.SetString(dataKey.Value, dataString);
	}

    public static List<string> GetStringList(SavedData dataKey)
    {
    	return PlayerPrefs.GetString(dataKey.Value).Split(',').ToList();
    }

    public static void StoreIntList(SavedData dataKey, List<int> saveData)
	{

		string dataString = "";
		foreach (int element in saveData){
			dataString += ""+element+",";
		}

		dataString = dataString.Remove(dataString.Length-1, 1);//remove trailing ','

		PlayerPrefs.SetString(dataKey.Value, dataString);
	}

    public static List<int> GetIntList(SavedData dataKey)
    {
    	List<string> dataStringList = GetStringList(dataKey);
		List<int> dataIntList = dataStringList.Select(s => int.Parse(s)).ToList();
		return dataIntList;
    }
}
