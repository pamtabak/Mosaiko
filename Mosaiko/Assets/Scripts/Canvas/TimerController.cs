using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    [SerializeField]
    Text timer;

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

        this.timer.text = String.Format("{0:00}:{1:00}", this.gameplayManager.timer / 60,
                                                         this.gameplayManager.timer % 60);
    }
}
