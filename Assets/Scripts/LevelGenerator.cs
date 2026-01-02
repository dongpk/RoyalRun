using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// trinh tao cap do
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;


    //GameObject[] chunks = new GameObject[12];
    List<GameObject> chunks = new List<GameObject>();
    private void Start()
    {
        SpawnStartingChunks();
    }
    private void Update()
    {
        MoveChunks();
    }
    void SpawnStartingChunks()
    {
        for(int i = 0;i<startingChunksAmount;i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        
        float spawnPositionZ = CaculateSpawmPosZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

        chunks.Add(newChunk);
        
    }

    private float CaculateSpawmPosZ()
    {
        float spawnPositionZ;
        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            //spawnPositionZ = transform.position.z + (i * chunkLength);
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }
    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
           
            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
                
            }
        }
    }
}
