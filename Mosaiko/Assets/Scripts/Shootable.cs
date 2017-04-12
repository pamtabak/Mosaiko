using UnityEngine;
using UnityEngine.Networking;

public class Shootable : NetworkBehaviour
{
    private Renderer renderer;

    private MeshFilter meshFilter;

	//private GameObject cube = null;

    public void Start()
    {
//		cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
//		cube.AddComponent<Shootable> ();
//		cube.transform.position = new Vector3 (-0.55F, 2.21F, -3.54F);
		this.renderer = this.GetComponent<Renderer> ();
		this.meshFilter = this.GetComponent<MeshFilter> ();
    }

    public void Shot()
    {
		Debug.Log ("shot");
		var mesh = this.meshFilter.mesh;
		Debug.Log(mesh.name + " has " + mesh.subMeshCount + " submeshes!");
        this.renderer.material.color = Color.red;

        var v = this.meshFilter.mesh.vertices;
    }
}

