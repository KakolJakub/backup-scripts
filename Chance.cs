using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Chance
{
	public static bool Proc(int percentageValue)
	{
		bool success=false;
		int chance=Random.Range (1, 101);

		if (chance <=percentageValue)
		{
			success = true;
			Debug.Log("Success!"+chance);
		}
		else if (chance>percentageValue)
		{
			success=false;
			Debug.Log("Failure!"+chance);
		}
		return success;
	}
}
