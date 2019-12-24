using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressBarGenerator : MonoBehaviour
{
    public GameObject progressPrefab;
    public int fragmentAmount;
    
    private float[] fragmentsList;
    private System.Random random;
    private const int SCREEN_WIDTH = 1800;

    // Start is called before the first frame update
    void Awake()
    {
        fragmentsList = new float[fragmentAmount*2-1];
        random = new System.Random();

        FragmentsGanerator();

        BarsGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FragmentsGanerator()
    {
        float amount = 0;
        for (int i=0; i<fragmentsList.Length; i++)
        {
            fragmentsList[i] = (float) random.Next();
            amount += i%2==0? fragmentsList[i] : 0;
        }

        for (int i=0; i<fragmentsList.Length; i++)
        {
            fragmentsList[i] = fragmentsList[i] / amount * SCREEN_WIDTH;
        }
    }

    private void BarsGenerator()
    {
        GameObject panel = this.GetComponent<GameObject>();

        GameObject bar = 
            GameObject.Instantiate(progressPrefab, panel.transform.position, panel.transform.rotation) as GameObject;

        bar.name = "Bar hahaha";
        bar.transform.SetParent(panel.transform);
        bar.transform.localScale = Vector3.one;
    }
}
