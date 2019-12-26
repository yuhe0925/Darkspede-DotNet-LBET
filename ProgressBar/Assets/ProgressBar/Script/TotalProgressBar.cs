using UnityEngine;

/// <summary>
/// 进度条染色类
/// </summary>

public class TotalProgressBar : MonoBehaviour
{
    //进度条总进度
    [Range(0.0f, 100.0f)]
    public float totalProgress;

    //所有进度条section的集合
    private Transform[] barList;
    //所有进度条色块的集合
    private Transform[] childBarList;
    //总长度
    private float totalLength;
    //所有进度条块长度的集合
    private float[] lengthList;
    //进度前进还是后退， true为前进
    private bool flag = true;
    //进度条速度, Demo用
    private const float VELOCITY = 7.5f;

    private void Start()
    {
        //总共有多少个进度条子物体，初始化用
        int childCount = this.transform.childCount;

        totalProgress = 0;
        totalLength = 0;

        barList = new Transform[childCount];
        childBarList = new Transform[childCount];
        lengthList = new float[childCount];

        for (int i=0; i<barList.Length; i++)
        {
            barList[i] = this.transform.GetChild(i);
            float tempLength = barList[i].GetComponent<RectTransform>().rect.width;
            totalLength += tempLength;
            lengthList[i] = tempLength;
            childBarList[i] = barList[i].GetChild(0);
        }
    }

    private void Update()
    {
        ProgressRenderer();

        //进度条随时间前进或后退，Demo用
        if ((totalProgress > 100 && flag) || (totalProgress < 0 && !flag))
        {
            flag = !flag;
        }
        if (flag)
        {
            totalProgress += VELOCITY * Time.deltaTime;
        }
        else
        {
            totalProgress -= VELOCITY * Time.deltaTime;
        }
    }

    private void ProgressRenderer()
    {
        float tempProgress = totalProgress * totalLength / 100;

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
