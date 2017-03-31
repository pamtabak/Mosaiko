using System.Collections;
using UnityEngine;

public class ShotEffectsManager : MonoBehaviour
{
    [SerializeField]
    AudioSource shotAudio;

    [SerializeField]
    LineRenderer laserShot;

    [SerializeField]
    float shotDuration = .07f;

    public void PlayShotEffects(Vector3 origin, Vector3 point)
    {
        this.laserShot.SetPosition(0, origin);
        this.laserShot.SetPosition(1, point);

        this.StartCoroutine(this.ShotEffect());
    }

    private IEnumerator ShotEffect()
    {
        this.shotAudio.Play();

        this.laserShot.enabled = true;
        yield return new WaitForSeconds(this.shotDuration);
        this.laserShot.enabled = false;
    }
}
