﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using geniikw.DataSheetLab;

public class UIWorldStage : MonoBehaviour
{
    [SerializeField] private Image image = null;

    public StageData stageData { get; set; }

    public void SetStageData(StageData newStageData)
    {
        stageData = newStageData;

        SetImage(stageData.worldImage);
    }

    public void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("No Image");
        }
    }
}