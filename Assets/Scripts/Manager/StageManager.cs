﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.DataSheetLab;

public class StageManager : MonoBehaviour
{
    public delegate void OnChangedWaveDelegate();
    public OnChangedWaveDelegate OnChangedWave;

    // 스테이지 데이터를 관리해주는 매니저
    public StageSheet stageDatas = null;
    
    public int currentStage = 1;
    public StageData currentStageData = null;
    public int currentWave = 1;

    public void Initialize()
    {
        currentStageData = stageDatas[currentStage - 1];


        
        // 웨이브 하나씩 가져오기.
        //foreach(var waveData in stageDatas[0].waveData.Sheet)
        //{
        //    Debug.Log(waveData.waveNum);
        //}
    }

    public float GetRelativePercentageByStage()
    {
        float totalWave = GameManager.instance.stageManager.currentStageData.totalWave;
        float currentWave = GameManager.instance.stageManager.currentWave;

        return currentWave / totalWave;
    }

    public void IncreaseWave(int increaseValue)
    {
        currentWave = Mathf.Clamp(currentWave + increaseValue, 1, currentStageData.totalWave);
        OnChangedWave();
    }

    public bool IsFinalWave()
    {
        return currentWave == currentStageData.totalWave;
    }

}
