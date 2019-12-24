using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalProgressBar : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float totalProgress;

    private Transform[] barList;
    private Transform[] childBarList;
    private float totalLength;
    private float[] lengthList;
    private bool flag = true;

    // Start is called before the first frame update
    private void Start()
    {
        totalProgress = 0;
        totalLength = 0;

        int childCount = this.transform.childCount;
        barList = new Transform[childCount];
        childBarList = new Transform[childCount];
        lengthList = new float[childCount];

        for (int i=0; i<barList.Length; i++)
        {
            //print(this.transform.GetChild(i).name);
            barList[i] = this.transform.GetChild(i);
            float tempLength = barList[i].GetComponent<RectTransform>().rect.width;
            totalLength += tempLength;
            lengthList[i] = tempLength;
            childBarList[i] = barList[i].GetChild(0);
        }

        print(totalLength);
    }

    private void Update()
    {
        ProgressRenderer();
        if ((totalProgress > 100 && flag) || (totalProgress < 0 && !flag))
        {
            flag = !flag;
        }
        if (flag)
        {
            totalProgress += 7.5f * Time.deltaTime;
        }
        else
        {
            totalProgress -= 7.5f * Time.deltaTime;
        }
    }

    private void ProgressRenderer()
    {
        
        float tempProgress = totalProgress * totalLength / 100;
        print(tempProgress);

        for (int i=0; i<barList.Length; i++)
        {
            if (tempProgress == 0)
            {
                SetWidth(i, tempProgress);
            }
            if (tempProgress <= barList[i].GetComponent<RectTransform>().rect.width)
            {
                SetWidth(i, tempProgress);
                tempProgress = 0;
            }
            else
            {
                SetWidth(i, lengthList[i]);
                tempProgress -= lengthList[i];
            }
        }
    }

    private void SetWidth(int i, float tempProgress)
    {
        childBarList[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, tempProgress);
    }

}
