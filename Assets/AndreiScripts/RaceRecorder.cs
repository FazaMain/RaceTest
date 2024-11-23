using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRecorder : MonoBehaviour
{
    // сначала хлтел записать трассу через input но потом передумал. ћожно еще добавить list дл€ состо€ни€ колес, но это пока лишнее
    List<Vector3> positionLog = new List<Vector3>();
    List<Quaternion> rotationLog = new List<Quaternion>();
    List<Vector3> NewPositionLog = new List<Vector3>();
    List<Quaternion> NewRotationLog = new List<Quaternion>();
    bool isRecording = false;

    public GameObject Ghost;

    public void ToggleRecord()
    {
        // тогглим запись
        isRecording = !isRecording;
        if (isRecording)
        {
            //очищаем основу
            positionLog.Clear();
            rotationLog.Clear();
        }
        else
        {
            // дублируем в новую
            NewPositionLog = new List<Vector3>(positionLog);
            NewRotationLog = new List<Quaternion>(rotationLog);
        }
    }
    public void LaunchGhost()
    {
        Ghost.SetActive(true);
        // «апускаем призрака
        Debug.Log("¬ыпускаю бычка!");
        StartCoroutine(ReplayMovement());
    }
    void Update()
    {
        if (isRecording)
        {
            // «акидываем в листы
            positionLog.Add(transform.position);
            rotationLog.Add(transform.rotation);
        }
    }

    IEnumerator ReplayMovement()
    {
        // ну собственно Replay, идем по очереди в листе и обновл€ем позицию
        for (int i = 0; i < NewPositionLog.Count; i++)
        {
            if (Ghost != null)
            {
                Ghost.transform.position = NewPositionLog[i];
                Ghost.transform.rotation = NewRotationLog[i];
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
