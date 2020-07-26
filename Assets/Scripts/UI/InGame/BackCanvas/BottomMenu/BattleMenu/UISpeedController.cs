﻿using UnityEngine;
using UnityEngine.UI;

public class UISpeedController : MonoBehaviour
{
    [SerializeField] private Text speedText = null;
    private float currentSpeed;

    public void Initialize()
    {
        ChangeToDefaultSpeed();

        InGameManager.instance.gameState.OnPrepare += ChangeToDefaultSpeed;
    }

    public void ChangeSpeed()
    {
        if(currentSpeed == InGameService.DEFAULT_SPEED)
        {
            currentSpeed = InGameService.DOUBLE_SPEED;
            EnemyService.SetDoubleSpeed();
        }
        else
        {
            currentSpeed = InGameService.DEFAULT_SPEED;
            EnemyService.SetDefaultSpeed();
        }

        ChangeText();
        ChangeTimeScale();
    }

    public void ChangeText()
    {
        if (currentSpeed == InGameService.DEFAULT_SPEED)
        {
            speedText.text = "X1";
        }
        else
        {
            speedText.text = "X2";
        }
    }
    
    public void ChangeToDefaultSpeed()
    {
        currentSpeed = InGameService.DEFAULT_SPEED;
        ChangeText();
        ChangeTimeScale();
    }

    public void ChangeTimeScale()
    {
        Time.timeScale = currentSpeed;
    }
}