using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControler : MonoBehaviour
{
    public float offEntityY;

    public GameObject points;
    public float pointsSpawnTime;
    public float pointsSpeed;
    public Transform[] spawnPoints;

    public GameObject trampoline;
    public Vector2 trampolinesSpawnRange;
    public float trampolinesSpeed;
    public Transform[] spawnTrampoline;
    private GameObject[] entitys;
    private GameObject[] trampolines;
    private int previus = -1;
    void Start()
    {
        entitys = new GameObject[20];
        trampolines = new GameObject[8];
        for(int a = 0; a < entitys.Length; a++)
        {
            entitys[a] = Instantiate(points);
            entitys[a].SetActive(false);
            int id = Random.Range(0, spawnPoints.Length);
            entitys[a].transform.position = new Vector3(spawnPoints[id].position.x, spawnPoints[id].position.y, entitys[a].transform.position.z);
        }
        for(int a = 0; a < trampolines.Length; a++)
        {
            trampolines[a] = Instantiate(trampoline);
            trampolines[a].SetActive(false);
            int id = Random.Range(0,spawnTrampoline.Length);
            trampolines[a].transform.position = new Vector3(spawnTrampoline[id].position.x,spawnTrampoline[id].position.y,trampolines[a].transform.position.z);
        }
        StartCoroutine(PointsSpawn());
        StartCoroutine(TrampolineSpawn());
    }
    void Update()
    {
        for(int a = 0; a < entitys.Length; a++)
        {
            if (entitys[a].activeSelf)
            {
                if (entitys[a].transform.position.y < offEntityY)
                {
                    int id = Random.Range(0, spawnPoints.Length);
                    while (id == previus)
                    {
                        id = Random.Range(0, spawnPoints.Length);
                    }
                    previus = id;
                    entitys[a].SetActive(false);
                    entitys[a].transform.position = new Vector3(spawnPoints[id].position.x, spawnPoints[id].position.y, entitys[a].transform.position.z);
                }
                else if(entitys[a] != GameManager.instance.GetPoitEntity())
                    entitys[a].transform.position = new Vector3(entitys[a].transform.position.x, entitys[a].transform.position.y - pointsSpeed * Time.deltaTime, entitys[a].transform.position.z);
            }
        }
        for(int a = 0; a < trampolines.Length; a++)
        {
            if (trampolines[a].activeSelf)
            {
                if (trampolines[a].transform.position.y < offEntityY)
                {
                    int id = Random.Range(0, spawnTrampoline.Length);
                    trampolines[a].SetActive(false);
                    trampolines[a].transform.position = new Vector3(spawnTrampoline[id].position.x, spawnTrampoline[id].position.y, trampolines[a].transform.position.z);
                }
                else
                    trampolines[a].transform.position = new Vector3(trampolines[a].transform.position.x, trampolines[a].transform.position.y - trampolinesSpeed * Time.deltaTime, trampolines[a].transform.position.z);
            }
        }
    }
    IEnumerator PointsSpawn()
    {
        //print("FASE1");
        yield return new WaitForSeconds(pointsSpawnTime);
        bool temp = false;
        int id = 0;
        while (!temp)
        {
            //print("FASE2");
            if (!entitys[id].activeSelf)
            {
                temp = true;
                //print("FASE3");
            }
            else
                id++;
            if(!temp && id >= entitys.Length)
            {
                Debug.LogError("ERROR. Ningun point disponible");
            }
        }
       // print("FASE4");
        entitys[id].SetActive(true);
        yield return StartCoroutine(PointsSpawn());
    }
    IEnumerator TrampolineSpawn()
    {
        yield return new WaitForSeconds(Random.Range(trampolinesSpawnRange.x,trampolinesSpawnRange.y));
        bool temp = false;
        int id = 0;
        while (!temp)
        {
            if (!trampolines[id].activeSelf)
            {
                temp = true;
            }
            else
                id++;
            if(!temp && id >= trampolines.Length)
            {
                Debug.LogError("ERROR. Ningun trampolin disponible");
            }
        }
        trampolines[id].SetActive(true);
        yield return StartCoroutine(TrampolineSpawn());
    }
}
