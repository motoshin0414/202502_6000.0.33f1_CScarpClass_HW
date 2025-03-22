using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrefabCre : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab; // �n�ͦ��� prefab
    public GameObject TextObject;
    public Vector3 startPosition = Vector3.zero; // ��l�ͦ���m
    private int spawnCount = 0; // �p��ͦ�����
    public Transform parentTransform; // ������ (�i��)

    // �I�s����k�ͦ�����
    public void SpawnPrefab()
    {
        // �p��s���ͦ���m
        Vector3 spawnPosition = TextObject.transform.position + new Vector3(0, -49 * spawnCount, 0);//transform.position = new Vector3( x, y, z );
        // �ͦ�����
        GameObject newText = Instantiate(prefab, spawnPosition, Quaternion.identity, parentTransform);
        TextMeshProUGUI tmpComponent = newText.GetComponent<TextMeshProUGUI>();
        if (tmpComponent != null)
        {
            tmpComponent.text = $"A {spawnCount + 1} B"; // �]�wtest�ݩ�
        }
        // �W�[�ͦ�����
        spawnCount++;
    }
}
