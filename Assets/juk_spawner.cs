using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class juk_spawner : MonoBehaviour
{
    // Префабы объектов для спавна
    public GameObject[] spawnPrefabs;

    // Интервал спавна
    public float spawnInterval = 2f;
    public int max_count_in_zone;

    public List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnObjects",0f, spawnInterval); // Запускаем спавн объектов с интервалом
    }

    public void Update()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null ) 
            {
                objects.RemoveAt(i);
            }
        }
    }

    void SpawnObjects()
    {
        if(objects.Count < max_count_in_zone) 
        {
            MeshCollider collider = GetComponent<MeshCollider>();
            // Генерируем случайную точку внутри выбранного `Box Collider`
            Vector3 randomPosition = GetRandomPointInMesh(collider);
            // Создаем объект в случайной позиции
            objects.Add(Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)], randomPosition + new Vector3(0,0.1f,0), Quaternion.identity));
        }
    }

    // Функция для получения случайной точки внутри `Box Collider`
    Vector3 GetRandomPointInMesh(MeshCollider collider)
    {
        // Получаем доступ к мешу
        Mesh mesh = collider.sharedMesh;

        // Получаем массив вершин и треугольников
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        // Генерируем случайный треугольник
        int triangleIndex = Random.Range(0, triangles.Length / 3) * 3;

        // Получаем вершины треугольника
        Vector3 v0 = vertices[triangles[triangleIndex]];
        Vector3 v1 = vertices[triangles[triangleIndex + 1]];
        Vector3 v2 = vertices[triangles[triangleIndex + 2]];

        // Генерируем случайные barycentric координаты
        float r1 = Random.value;
        float r2 = Random.value;
        if (r1 + r2 > 1)
        {
            r1 = 1 - r1;
            r2 = 1 - r2;
        }

        // Интерполируем между вершинами треугольника
        Vector3 randomPoint = v0 + r1 * (v1 - v0) + r2 * (v2 - v0);

        // Преобразуем точку в мировое пространство
        randomPoint = collider.transform.TransformPoint(randomPoint);

        return randomPoint;
    }
}
