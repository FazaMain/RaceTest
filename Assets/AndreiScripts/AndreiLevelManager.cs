using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndreiLevelManager : MonoBehaviour
{
    public Transform StartLine;
    public GameObject PlayerCar;

    public void RestartRace()
    {
        Debug.Log("Рестарт");
        StartCoroutine(RestartCo());

    }
    IEnumerator RestartCo()
    {
        // все на исходную (без корутины срабатывало некорректно)
        PlayerCar.SetActive(false);
        yield return null;
        PlayerCar.transform.position = StartLine.position;
        PlayerCar.transform.rotation = StartLine.rotation;
        PlayerCar.SetActive(true);
    }

}
