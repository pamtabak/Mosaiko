using UnityEngine;
using UnityEngine.Networking;

public class Shootable : NetworkBehaviour
{
    private Renderer renderer;

    private MeshFilter meshFilter;

    public void Start()
    {
        this.renderer = GetComponent<Renderer>();

        this.meshFilter = GetComponent<MeshFilter>();
    }

    public void Shot()
    {
        this.renderer.material.color = Color.red;

        var v = this.meshFilter.mesh.vertices;
    }
}

