using UnityEngine;
using UnityEngine.Networking;

public class WeaponPositionSync : NetworkBehaviour
{
    [SerializeField]
    Transform camTransform;

    [SerializeField]
    Transform basePosition;

    [SerializeField]
    Transform weaponPivot;

    [SerializeField]
    float threshold = 3f;

    [SerializeField]
    float smoothing = 5f;

    [SyncVar]
    float pitch;

    private Vector3 lastOffset;
    private float lastSyncedPitch;

    void Start()
    {
        if (this.isLocalPlayer)
        {
            this.weaponPivot.parent = this.camTransform;
        }
        else
        {
            this.lastOffset = this.basePosition.position - this.transform.position;
        }
    }

    void Update()
    {
        if (this.isLocalPlayer)
        {
            this.pitch = this.camTransform.localRotation.eulerAngles.x;
            if (Mathf.Abs(this.lastSyncedPitch - this.pitch) >= this.threshold)
            {
                this.CmdUpdatePitch(this.pitch);
                this.lastSyncedPitch = this.pitch;
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(this.pitch, 0f, 0f);

            Vector3 currentOffset = this.basePosition.position - this.transform.position;
            this.weaponPivot.localPosition += currentOffset - this.lastOffset;
            this.lastOffset = currentOffset;

            this.weaponPivot.localRotation = Quaternion.Lerp(this.weaponPivot.localRotation,
                newRotation, Time.deltaTime * smoothing);
        }
    }

    [Command]
    void CmdUpdatePitch(float newPitch)
    {
        this.pitch = newPitch;
    }
}
