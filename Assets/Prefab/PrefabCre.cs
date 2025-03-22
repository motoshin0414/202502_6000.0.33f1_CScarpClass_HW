using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrefabCre : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab; // 要生成的 prefab
    public GameObject TextObject;
    public Vector3 startPosition = Vector3.zero; // 初始生成位置
    private int spawnCount = 0; // 計算生成次數
    public Transform parentTransform; // 父物件 (可選)

    // 呼叫此方法生成物件
    public void SpawnPrefab()
    {
        // 計算新的生成位置
        Vector3 spawnPosition = TextObject.transform.position + new Vector3(0, -49 * spawnCount, 0);//transform.position = new Vector3( x, y, z );
        // 生成物件
        GameObject newText = Instantiate(prefab, spawnPosition, Quaternion.identity, parentTransform);
        TextMeshProUGUI tmpComponent = newText.GetComponent<TextMeshProUGUI>();
        if (tmpComponent != null)
        {
            tmpComponent.text = $"A {spawnCount + 1} B"; // 設定test屬性
        }
        // 增加生成次數
        spawnCount++;
    }
}
