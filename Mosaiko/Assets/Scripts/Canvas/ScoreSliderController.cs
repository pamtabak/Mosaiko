using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSliderController : MonoBehaviour
{
    [SerializeField]
    Image teamOneScore;

    [SerializeField]
    Image teamTwoScore;

    private GameplayManager gameplayManager;

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameplayManager") == null)
        {
            return;
        }

        if (this.gameplayManager == null)
        {
            this.gameplayManager = GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>();
        }

        if (this.gameplayManager.teamOneScore + this.gameplayManager.teamTwoScore == 0)
        {
            this.teamOneScore.rectTransform.localScale = new Vector3(.5f, 1, 1);
        }
        else
        {
            this.teamOneScore.rectTransform.localScale = new Vector3((float)this.gameplayManager.teamOneScore / (float)(this.gameplayManager.teamOneScore + this.gameplayManager.teamTwoScore), 1, 1);
        }
    }
}
