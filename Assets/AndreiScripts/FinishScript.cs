using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    UIController UIC;
    private void Start()
    {
        UIC = FindObjectOfType<UIController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // ����� �� ���� 
        if (other.CompareTag("Player"))
        {
            UIC.RaceEnd();
            Debug.Log("��������");
        }
    }


}
