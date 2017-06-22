using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreSliderController : MonoBehaviour
{
    [SerializeField]
    Image teamOneScore;

    [SerializeField]
    Image teamTwoScore;

    [SerializeField]
    Text teamOneScorePoints;

    [SerializeField]
    Text teamTwoScorePoints;

    private GameplayManager gameplayManager;

    private void Start()
    {
        this.teamOneScore.color = TeamColors.TEAM_ONE_COLOR;
        this.teamTwoScore.color = TeamColors.TEAM_TWO_COLOR;
    }

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

        this.teamOneScorePoints.text = this.gameplayManager.teamOneScore.ToString();
        this.teamTwoScorePoints.text = this.gameplayManager.teamTwoScore.ToString();

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
