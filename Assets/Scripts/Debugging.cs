using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public GameObject cubePrefab; // Префаб куба, установите его в редакторе Unity
    private GameObject visualizationParent; // Объект, который будет содержать визуализацию

    void Awake()
    {
        visualizationParent = new GameObject("MatrixVisualization");
        visualizationParent.transform.parent = transform;
        visualizationParent.transform.Translate(new Vector3(-11.5f, 9.5f, 0));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            VisualizeMatrix(new Vector3(-15, -10, 0));
        }

    }

    private void VisualizeMatrix(Vector3 startPosition)
    {
        bool[,] matrix = FieldController.GetMatrix();
        foreach (Transform child in visualizationParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j])
                {
                    Vector3 position = new Vector3(startPosition.x + i, startPosition.y + j, startPosition.z);
                    Instantiate(cubePrefab, position, Quaternion.identity, visualizationParent.transform);
                }
            }
        }
    }
}
