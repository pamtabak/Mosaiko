using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AmmoSpawn : NetworkBehaviour
{
    [SerializeField]
    int[] ammoQuantityArray;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Shooting playerShooting = other.GetComponent<Shooting>();

        if (playerShooting == null)
            return;

        int index = (int)Random.Range(0, this.ammoQuantityArray.Length - 1);
        playerShooting.GetAmmoFromGround(this.ammoQuantityArray[index]);

        NetworkServer.Destroy(this.gameObject);
    }
}
