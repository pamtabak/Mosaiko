using UnityEngine;
using UnityEngine.Networking;

public class Shootable : NetworkBehaviour
{
    private Renderer renderer;

    private MeshFilter meshFilter;

    public void Start()
    {
		this.renderer = this.GetComponent<Renderer> ();
		this.meshFilter = this.GetComponent<MeshFilter> ();

		// Setting initial texture (grid`s border)
		Texture2D tempTexture = (Texture2D)Resources.Load("Textures/Grid")as Texture2D;
		this.renderer.material.mainTexture=tempTexture;
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

