﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimitFPS : MonoBehaviour
{

    public int target = 60;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }

    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
    }
}