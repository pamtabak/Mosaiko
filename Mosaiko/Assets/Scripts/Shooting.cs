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
    private bool shotEnabled;

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
            this.CmdShoot(this.firePosition.position, this.firePosition.forward);
        }
    }

    [Command]
    void CmdShoot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red, 5f);

        bool shotHit = Physics.Raycast(ray, out hit, this.weaponRange);

        if (shotHit)
        {
            // shot has hit something
            this.RpcProcessShotEffects(shotHit, hit.point);
        }
        else
        {
            // shot has not hit something
            this.RpcProcessShotEffects(shotHit, ray.origin + (ray.direction * this.weaponRange));
        }
    }

    [ClientRpc]
    void RpcProcessShotEffects(bool shotHit, Vector3 point)
    {
        this.shotEffectsManager.PlayShotEffects(this.weaponEnd.position, point);
    }
}
