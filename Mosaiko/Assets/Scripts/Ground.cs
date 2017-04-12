using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
	void Start()
	{
		Instantiate(Resources.Load("Prefabs/GridCell"), new Vector3(-10,0.016f,0), Quaternion.identity);
	}
}

