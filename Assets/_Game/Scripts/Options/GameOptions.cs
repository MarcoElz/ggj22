using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameOptions : MonoBehaviour
{
    [SerializeField] private ScriptableRendererFeature ssao;

    private void Awake()
    {
        ActiveOptimizedMode(false);
    }

    public void ActiveOptimizedMode(bool isEnabledOptimizedMode)
    {
        EnableAO(!isEnabledOptimizedMode);
        QualitySettings.masterTextureLimit = isEnabledOptimizedMode ? 1 : 0;
    }

    private void EnableAO(bool value)
    {
        ssao.SetActive(value);
    }
}
