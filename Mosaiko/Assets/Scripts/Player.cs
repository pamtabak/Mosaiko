using UnityEngine;
using UnityEngine.UI;
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

    [SyncVar]
    public int teamId;

    [SyncVar]
    public Color teamColor;

    NetworkAnimator anim;

    // Use this for initialization
    void Start()
    {
        this.mainCamera = Camera.main.gameObject;
        this.anim = this.GetComponent<NetworkAnimator>();

        this.EnablePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isLocalPlayer)
        {
            return;
        }

        anim.animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        anim.animator.SetFloat("Strafe", Input.GetAxis("Horizontal"));
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

	[ClientRpc]
	public void RpcGameOver (int winnerTeam)
	{
		Debug.Log ("GameOver");

		this.DisablePlayer ();

		GameObject gameOverObject = GameObject.FindGameObjectWithTag ("GameOver");
		Text       text           = (Text) gameOverObject.GetComponent<Text> ();

		if (winnerTeam == 0) 
		{
			// it`s a tie
			text.text  = "tie";
		}
		else if (winnerTeam == teamId) 
		{
			// you win
			text.text  = "you win";
		} 
		else 
		{
			// you lose
			text.text  = "you lose";
		}
	}
}
