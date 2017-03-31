using UnityEngine;
using UnityEngine.Networking;

using Assets.Scripts.Utils;

public class Player : NetworkBehaviour
{
    [SerializeField]
    ToggleEvent onToggleShared;

    [SerializeField]
    ToggleEvent onToggleLocal;

    [SerializeField]
    ToggleEvent onToggleRemote;

    GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        this.mainCamera = Camera.main.gameObject;

        this.EnablePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisablePlayer()
    {
        this.onToggleShared.Invoke(false);

        if (this.isLocalPlayer)
        {
            this.mainCamera.SetActive(true);
            this.onToggleLocal.Invoke(false);
        }
        else
        {
            this.onToggleRemote.Invoke(false);
        }
    }

    void EnablePlayer()
    {
        this.onToggleShared.Invoke(true);

        if (this.isLocalPlayer)
        {
            this.mainCamera.SetActive(false);
            this.onToggleLocal.Invoke(true);
        }
        else
        {
            this.onToggleRemote.Invoke(true);
        }
    }
}
