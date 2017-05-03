using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour
{
    [SerializeField]
    float fireRate = .3f;

    [SerializeField]
    float weaponRange = 50f;

    [SerializeField]
    Transform firePosition;

    [SerializeField]
    Transform weaponEnd;

    [SerializeField]
    ShotEffectsManager shotEffectsManager;

    private float ellapsedTime;
    private bool  shotEnabled;

    // Use this for initialization
    void Start()
    {
        if (this.isLocalPlayer)
        {
            this.shotEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.shotEnabled)
        {
            return;
        }

        this.ellapsedTime += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && this.ellapsedTime > this.fireRate)
        {
            this.ellapsedTime = 0f;

            Player player = this.GetComponentInParent<Player>();

            this.CmdShoot(this.firePosition.position, this.firePosition.forward, player.teamId, player.teamColor);
        }
    }

    [Command]
    void CmdShoot(Vector3 origin, Vector3 direction, int teamId, Color teamColor)
    {
        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 15f, Color.red, 5f);

        bool shotHit = Physics.Raycast(ray, out hit, this.weaponRange);

        if (shotHit)
        {
            // shot has hit something
            this.RpcProcessShotEffects(shotHit, hit.point, teamColor);
            
			Shootable shotObj = hit.collider.GetComponent<Shootable>();
			if (shotObj != null) 
			{
				shotObj.RpcShot(teamColor);
			}
        }
        else
        {
            // shot has not hit something
            this.RpcProcessShotEffects(shotHit, ray.origin + (ray.direction * this.weaponRange), teamColor);
        }
    }

    [ClientRpc]
    void RpcProcessShotEffects(bool shotHit, Vector3 point, Color color)
    {
        this.shotEffectsManager.PlayShotEffects(this.weaponEnd.position, point, color);
    }
}
