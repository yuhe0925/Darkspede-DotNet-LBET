using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class NewProgressBar : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float progress;

    private RectTransform progressBarTF;
    private RectTransform parentRT;

    public void Start()
    {
        progressBarTF = this.GetComponent<RectTransform>();
        parentRT = progressBarTF.parent.GetComponent<RectTransform>();
        progress = 0;
    }

    public void Update()
    {
        WidthSet(progress);
    }

    private void WidthSet(float progress)
    {
        float tempWidth = progress * parentRT.rect.width / 100;
        progressBarTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempWidth);
    }
}*/
