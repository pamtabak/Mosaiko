using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
	void Start()
	{	
		GameObject groundObject = GameObject.FindGameObjectWithTag ("Wall");
		Vector3 groundDimensions = groundObject.GetComponent<Collider> ().bounds.size;

		float y = 0.016f;
		for (int i = (int)-groundDimensions.z/2; i < (int)groundDimensions.z/2; i++) 
		{
			float x = (float) i;
			for (int j = (int)-groundDimensions.z/2; j < (int)groundDimensions.z/2; j++) 
			{
				float z = (float)j;
				Instantiate(Resources.Load("Prefabs/GridCell"), new Vector3(x,y,z), Quaternion.identity);
			}
		}
		//Instantiate(Resources.Load("Prefabs/GridCell"), new Vector3(-10,0.016f,0), Quaternion.identity);
		//Instantiate(Resources.Load("Prefabs/GridCell"), new Vector3(-5,0.016f,0), Quaternion.identity);
	}
}

