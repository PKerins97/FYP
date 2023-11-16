using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    private StreamWriter csvWriter;
    void Start()
    {
        // Set the initial position randomly
        RandomizePosition();

        string filePath = Application.dataPath + "/CubeData.csv";
        csvWriter = new StreamWriter(filePath);
        csvWriter.WriteLine("Object Name,Position X,Position Y,3D Corner Position, Cube centre, Cube Orientation, Cube Scale");
    }

    void Update()
    {
        // Move the cube continuously within bounds
        float horizontalMovement = Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime;
        float verticalMovement = Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        transform.Translate(movement);

        // Rotate the cube continuously on all axes
        float rotationX = rotationSpeed * Time.deltaTime;
        float rotationY = rotationSpeed * Time.deltaTime;
        float rotationZ = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationX, rotationY, rotationZ);

        // Print the current position
        Debug.Log("Current Position: " + transform.position);
        Transform cube = this.transform;
        string cubeString = $"{"Cube"},{cube.position.x},{cube.position.y},{cube.position.z}";
        csvWriter.WriteLine(cubeString);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform corner = transform.GetChild(i);
            Debug.Log($"Corner {i + 1} Position: {corner.position}");
            string positionString = $"{"Vertex" + i + 1},{corner.position.x},{corner.position.y}";
            csvWriter.WriteLine(positionString);
        }


    }
    void OnDestroy()
    {
        // Close the CSV file when the script is destroyed
        if (csvWriter != null)
        {
            csvWriter.Close();
        }
    }

    void RandomizePosition()
    {
        // Set the cube's position to a random point within a certain range
        float randomX = Random.Range(-0f, 10f);
        float randomY = Random.Range(-0f, 10f);
        float randomZ = Random.Range(-0f, 10f);
        transform.position = new Vector3(randomX, 0f, randomZ);

        // Print the initial position
        Debug.Log("Initial Position: " + transform.position);
    }
}
