using System.Collections.Generic;
using UnityEngine;


public class PlacedBlocksScript : MonoBehaviour
{
    [SerializeField] public List<GameObject>[] lines;

    void Awake()
    {
        lines = new List<GameObject>[Mathf.RoundToInt(FieldController.Height)];
    }
}
