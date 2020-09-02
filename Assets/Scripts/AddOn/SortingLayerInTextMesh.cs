using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TextMesh의 Layer, sortingOrder를 별도로 지정해주기 위한 클래스
/// </summary>
public class SortingLayerInTextMesh : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;

    private void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }
}
