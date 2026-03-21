using System.Drawing;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]

public class FishManager : MonoBehaviour
{
    [SerializeField] private GameObject Fish;
    [SerializeField] private int fishToSpawn;
    private BoxCollider box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider>();
        SpawnFish();
    }
    public void SpawnFish()
    {
        GetRandomPoint();
        Instantiate(Fish, GetRandomPoint(), Quaternion.identity);
    }
    Vector3 GetRandomPoint()
    {
        Vector3 center = box.center + transform.position;
        Vector3 size = box.size;
        return center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
    }
}
