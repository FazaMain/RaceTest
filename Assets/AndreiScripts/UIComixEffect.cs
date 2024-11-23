using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

[System.Serializable]
public class ComixEffect
{
    public string EffectName;
    public GameObject[] FrontEffectArray;
    public GameObject[] BackEffectArray;
    public GameObject[] SecondBackEffectArray;
}
public class UIComixEffect : MonoBehaviour
{
    bool Braking;
    bool Gass;
    [HideInInspector]
    public bool ShakeDone;

    public float CycleTime;
    public CinemachineImpulseSource CIS;


    public ComixEffect[] EffectsArray;
    UIController UIC;
    private void Start()
    {
        UIC = FindObjectOfType<UIController>();
        EffectCleaner(0);
        EffectCleaner(1);
    }
    void Shake()
    {
        //Небольшой шейк для живости)
        if (!ShakeDone)
        {
            CIS.GenerateImpulse();
        }
        ShakeDone = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && UIC.Race)
        {
            Shake();
            GassEffectToggler(true);
        }
        if (Input.GetKeyUp(KeyCode.W) && UIC.Race)
        {
            GassEffectToggler(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && UIC.Race)
        {
            BreakEffectToggler(true);
        }
        //if (Input.GetKeyUp(KeyCode.Space) && UIC.Race)
        //{
        //    BreakEffectToggler(false);
        //}
    }
    void EffectCleaner(int i)
    {
        for (int u = 0; u < EffectsArray[i].FrontEffectArray.Length; u++)
        {
            EffectsArray[i].FrontEffectArray[u].SetActive(false);
            EffectsArray[i].BackEffectArray[u].SetActive(false);
            EffectsArray[i].SecondBackEffectArray[u].SetActive(false);
        }
    }
    public void GassEffectToggler(bool b)
    {
        Gass = b;
        if (b)
        {
            StartCoroutine(GasEffectCo());
        }
        else
        {
            EffectCleaner(0);
        }
    }
    
    IEnumerator GasEffectCo()
    {
        int Cycleint = 0;
        while (Gass)
        { 
            Cycleint += 1;
            int i = Random.Range(0, 6);
            int x = Random.Range(0, 6);
            int y = Random.Range(0, 6);
            yield return null;
            EffectCleaner(0);
            EffectsArray[0].FrontEffectArray[i].SetActive(true);
            EffectsArray[0].BackEffectArray[x].SetActive(true);
            EffectsArray[0].SecondBackEffectArray[y].SetActive(true);
            if(Cycleint > 7)
            {
                Gass = false;
                EffectCleaner(0);
            }
            yield return new WaitForSeconds(CycleTime);
        }
    }
    public void BreakEffectToggler(bool b)
    {
        Braking = b;
        if (b)
        {
            StartCoroutine(BreakEffectCo());
        }
        else
        {
            EffectCleaner(1);
        }
    }
    IEnumerator BreakEffectCo()
    {
        int Cycleint = 0;
        while (Braking)
        {
            Cycleint += 1;
            int i = Random.Range(0, 6);
            int x = Random.Range(0, 6);
            int y = Random.Range(0, 6);
            yield return null;
            EffectCleaner(1);
            EffectsArray[1].FrontEffectArray[i].SetActive(true);
            EffectsArray[1].BackEffectArray[x].SetActive(true);
            EffectsArray[1].SecondBackEffectArray[y].SetActive(true);
            if (Cycleint > 5)
            {
                Braking = false;
                EffectCleaner(1);
            }
            yield return new WaitForSeconds(CycleTime);
        }
    }
}
