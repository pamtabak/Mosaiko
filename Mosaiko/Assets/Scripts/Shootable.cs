using UnityEngine;
using UnityEngine.Networking;

public class Shootable : NetworkBehaviour
{
    private new Renderer renderer;

    private MeshFilter meshFilter;

    [SyncVar]
    public int teamId;

    public void Start()
    {
		this.renderer = this.GetComponent<Renderer> ();
		//this.meshFilter = this.GetComponent<MeshFilter> ();

		// Setting initial texture (grid`s border)
		Texture2D tempTexture = (Texture2D)Resources.Load("Textures/Grid")as Texture2D;
		this.renderer.material.mainTexture=tempTexture;
    }

    [ClientRpc]
    public void RpcShot(int teamId, Color color)
    {
        this.teamId = teamId;
        this.renderer.material.color = color;
    }
}

