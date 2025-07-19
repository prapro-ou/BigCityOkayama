// ResourceGenerator.cs

using UnityEngine;
using System.Collections;

public class ResourceGenerator : MonoBehaviour
{
    [Header("生成する資源の種類")]
    public ResourceManager.ResourcesType resourceType; // enumをインスペクターから選べるようにする

    [Header("資源生成設定")]
    [Tooltip("一度に生成する量")]
    public int amountPerInterval = 5;

    [Tooltip("生成する間隔（秒）")]
    public float interval = 2.0f;

    void Start()
    {
        StartCoroutine(GenerateResources());
    }

    private IEnumerator GenerateResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (ResourceManager.Instance != null)
            {
                // 選択された種類の資源を、設定された量だけ増やす
                ResourceManager.Instance.IncreaseResource(resourceType, amountPerInterval);
            }
        }
    }
}