using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shooting : NetworkBehaviour
{
    [SerializeField]
    float fireRate = .2f;

    [SerializeField]
    float reloadDelay = 2.2f;

    [SerializeField]
    float weaponRange = 50f;

    [SerializeField]
    Transform firePosition;

    [SerializeField]
    Transform weaponEnd;

    [SerializeField]
    ShotEffectsManager shotEffectsManager;

    [SerializeField]
    int maxAmmo = 30;

    [SerializeField]
    int maxReloadAmmo = 60;

    [SerializeField]
    AudioSource cantShotAudio;

    [SerializeField]
    AudioSource reloadAudio;

    private GameplayManager gameplayManager;

    private Text ammoUI;

    private float ellapsedTime;
    private bool shotEnabled;

    [SyncVar]
    private int reloadAmmo;
    [SyncVar]
    private int ammo;

    private bool reloading;

    // Use this for initialization
    void Start()
    {
        if (this.isLocalPlayer)
        {
            this.shotEnabled = true;

            this.ammo = this.maxAmmo;
            this.reloadAmmo = this.maxReloadAmmo;
        }

        this.gameplayManager = GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>();

        this.ammoUI = GameObject.FindGameObjectWithTag("Ammo").GetComponent<Text>();
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

            this.CmdShoot(this.firePosition.position, this.firePosition.forward, player.teamId, player.teamColor, this.ammo);
        }

        if (Input.GetKeyDown(KeyCode.R) && !this.reloading && this.ammo < this.maxAmmo)
        {
            this.reloading = true;
            this.shotEnabled = false;


            if (this.reloadAmmo > 0)
            {
                this.reloadAudio.Play();
                this.Invoke("ReloadWeapon", this.reloadDelay);
            }
            else
            {
                this.cantShotAudio.Play();
                this.reloading = false;
                this.shotEnabled = true;
            }
        }

        // refreshes ammoUI
        this.ammoUI.text = string.Format("{0}/{1}", this.ammo, this.reloadAmmo);
    }

    [Command]
    void CmdShoot(Vector3 origin, Vector3 direction, int teamId, Color teamColor, int ammo)
    {
        this.ammo = ammo;

        if (this.ammo == 0)
        {
            this.cantShotAudio.Play();
            return;
        }

        this.ammo--;

        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 15f, Color.red, 5f);

        bool shotHit = Physics.Raycast(ray, out hit, this.weaponRange);

        if (shotHit)
        {
            // shot has hit something
            this.RpcProcessShotEffects(shotHit, hit.point, teamColor);

            Shootable shotObj = hit.collider.GetComponent<Shootable>();
            Player shotPlayer = hit.collider.GetComponent<Player>();

            if (shotPlayer != null)
            {
                // shooting a player
                float topOfHeadMinusHitY = hit.collider.bounds.max.y - hit.point.y;
                if (topOfHeadMinusHitY >= 0f && topOfHeadMinusHitY <= 0.5f)
                {
                    // headshot
                    shotPlayer.RpcHeadshotEffects(teamColor);
                    Debug.Log("HEADSHOT!");
                }
                shotPlayer.RpcTookShotEffects();
                //this.gameplayManager.ScoreShotPlayer(teamId, 3);
            }


            if (shotObj != null)
            {
                this.gameplayManager.Score(shotObj.teamId, teamId);
                shotObj.RpcShot(teamId, teamColor);
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

    void ReloadWeapon()
    {
        int tmp = this.maxAmmo - this.ammo;

        if (this.reloadAmmo >= tmp)
        {
            this.ammo = this.maxAmmo;
            this.reloadAmmo -= tmp;
        }
        else
        {
            this.ammo += this.reloadAmmo;
            this.reloadAmmo = 0;
        }

        this.reloading = false;
        this.shotEnabled = true;
    }
}
