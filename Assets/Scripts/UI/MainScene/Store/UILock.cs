﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILock : MonoBehaviour
{
    [SerializeField] private Text needLevelText;

    public void SetLock(int needLevel)
    {
        needLevelText.text = "레벨 " + needLevel.ToString();
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
