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

    private GameplayManager gameplayManager;

    private void Start()
    {
//		GameObject scoreSliderObject         = GameObject.FindGameObjectWithTag ("ScoreSlider");
//		Vector3    scoreSliderPosition       = new Vector3 ((float)(Camera.main.pixelWidth * 0.47) , (float) (Camera.main.pixelHeight * 0.60), (float) 0);
//		scoreSliderObject.transform.position = scoreSliderPosition;

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
