using UnityEngine;
using System;

/// <summary>
/// 进度条生成类
/// </summary>

public class ProgressBarGenerator : MonoBehaviour
{
    //进度条预设，结构要求是一个物体(当作背景进度条)下有物体(其中第一个物体为进度的色块)
    public GameObject progressPrefab;
    public int fragmentAmount;
    //二维进度条长度
    public int screenWidth = 1800;

    private const int SCREEN_WIDTH = 1800;
    private const int BAR_HEIGHT = 100;
    //Z轴条是否需要拉伸
    private const float Z_AXIS_SCALE = 1.5f;
    //进度块最小长度单位
    private const int MIN_BAR_UNIT = 10;
    //进度块最大长度单位
    private const int MAX_BAR_UNIT = 100;
    //第一个块起始坐标点
    private const int ORIGIN_X = 0;
    private const int ORIGIN_Y = -50;
    private const int ORIGIN_Z = 0;

    private System.Random random;
    private int actualFragmentAmount;
    private float[] fragmentsLengthList;
    private GameObject[] barList;

    void Awake()
    {
        actualFragmentAmount = fragmentAmount * 2 - 1;
        fragmentsLengthList = new float[actualFragmentAmount];
        barList = new GameObject[actualFragmentAmount];
        random = new System.Random();

        //生成函数需要写在Awake中
        //先生成每个进度块的长度
        FragmentsGanerator();

        //根据长度依次生成进度块
        BarsGenerator();
    }

    void Update()
    {

    }

    private void FragmentsGanerator()
    {
        float amount = 0;
        for (int i = 0; i < fragmentsLengthList.Length; i++)
        {
            fragmentsLengthList[i] = i % 2 != 0 ? 
                (float)random.Next(MIN_BAR_UNIT, MAX_BAR_UNIT) * Z_AXIS_SCALE :
                (float)random.Next(MIN_BAR_UNIT, MAX_BAR_UNIT);
            amount += i % 2 == 0 ? fragmentsLengthList[i] : 0;
        }

        for (int i = 0; i < fragmentsLengthList.Length; i++)
        {
            fragmentsLengthList[i] = fragmentsLengthList[i] / amount * SCREEN_WIDTH;
        }
    }

    private void BarsGenerator()
    {
        float x = ORIGIN_X;
        float y = ORIGIN_Y;
        float z = ORIGIN_Z;

        for (int i = 0; i < actualFragmentAmount; i++)
        {
            barList[i] = (GameObject)Instantiate(progressPrefab, this.GetComponent<Transform>());
            Transform instanceTF = barList[i].GetComponent<Transform>();
            instanceTF.transform.localEulerAngles = new Vector3(0, -90 * (int)Math.Sin(Math.PI/2*i), 0);
            PositionUpdate(i, ref x, ref y, ref z);
            instanceTF.transform.localPosition = new Vector3(x, y, z);
            instanceTF.GetComponent<RectTransform>().sizeDelta = new Vector2(fragmentsLengthList[i], BAR_HEIGHT);

            Transform childBarTF = instanceTF.GetChild(0);
            childBarTF.transform.localPosition = new Vector3(0, -ORIGIN_Y, 0);
        }
    }

    private void PositionUpdate(int i, ref float x, ref float y, ref float z)
    {
        if (i != 0)
        {
            x += (i % 2) * (fragmentsLengthList[i - 1]);
            y = ORIGIN_Y;
            z = i % 2 == 0 ? z + fragmentsLengthList[i - 1] * (float)Math.Pow(-1, ((i / 2) % 2) + 1) : z;
        }
        else;
    }
}
