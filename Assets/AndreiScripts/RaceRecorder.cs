using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRecorder : MonoBehaviour
{
    // ������� ����� �������� ������ ����� input �� ����� ���������. ����� ��� �������� list ��� ��������� �����, �� ��� ���� ������
    List<Vector3> positionLog = new List<Vector3>();
    List<Quaternion> rotationLog = new List<Quaternion>();
    List<Vector3> NewPositionLog = new List<Vector3>();
    List<Quaternion> NewRotationLog = new List<Quaternion>();
    bool isRecording = false;

    public GameObject Ghost;

    public void ToggleRecord()
    {
        // ������� ������
        isRecording = !isRecording;
        if (isRecording)
        {
            //������� ������
            positionLog.Clear();
            rotationLog.Clear();
        }
        else
        {
            // ��������� � �����
            NewPositionLog = new List<Vector3>(positionLog);
            NewRotationLog = new List<Quaternion>(rotationLog);
        }
    }
    public void LaunchGhost()
    {
        Ghost.SetActive(true);
        // ��������� ��������
        Debug.Log("�������� �����!");
        StartCoroutine(ReplayMovement());
    }
    void Update()
    {
        if (isRecording)
        {
            // ���������� � �����
            positionLog.Add(transform.position);
            rotationLog.Add(transform.rotation);
        }
    }

    IEnumerator ReplayMovement()
    {
        // �� ���������� Replay, ���� �� ������� � ����� � ��������� �������
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
