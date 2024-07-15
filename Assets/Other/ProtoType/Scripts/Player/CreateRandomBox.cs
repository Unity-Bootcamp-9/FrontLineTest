using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRandomBox : MonoBehaviour
{
    public Button createButton;
    public GameObject boxPrefab;
    public Transform player;

    private void Awake()
    {
        player = Camera.main.transform;
        createButton.onClick.AddListener(SpawnObjects);
    }

    void SpawnObjects()
    {
        Vector3 randomPosition = GetRandomPosition();

        Instantiate(boxPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        // ���� ��ġ�� �������� 10x10x10 ���� ���� ���� ��ġ ����
        float x = player.position.x + Random.Range(-5f, 5f);
        float y = player.position.y + Random.Range(-5f, 5f);
        float z = player.position.z + Random.Range(-5f, 5f);

        return new Vector3(x, y, z);
    }
}
