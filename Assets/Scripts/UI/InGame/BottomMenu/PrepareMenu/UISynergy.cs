﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISynergy : MonoBehaviour
{
    [SerializeField] private Image synergyImage = null;

    public void SetImage(Sprite sprite)
    {
        synergyImage.sprite = sprite;
    }
}