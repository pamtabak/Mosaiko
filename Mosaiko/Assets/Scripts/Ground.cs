using System;
using UnityEngine;
using UnityEngine.Networking;

public class Ground : NetworkBehaviour
{
	void Start()
	{
        this.CreateGrid();
    }

    void CreateGrid()
    {
        Vector3 groundDimensions    = this.GetComponent<Collider>().bounds.size;
        Vector3 gridCenarioPosition = GameObject.FindGameObjectWithTag("GridCenario").transform.position;

        float y = Mathf.Ceil(gridCenarioPosition.y * 100) / 100;
        for (float i = -(groundDimensions.z / 2) - 1; i < (groundDimensions.z / 2) + 1; i++)
        {
            for (float j = -(groundDimensions.z / 2) - 1; j < (groundDimensions.z / 2) + 1; j++)
            {
                GameObject gridCell = (GameObject)Instantiate(Resources.Load("Prefabs/GridCell"), new Vector3(i, y, j), Quaternion.identity);
                if (this.isServer)
                {
                    NetworkServer.Spawn(gridCell);
                }
            }
        }
    }
}

