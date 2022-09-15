using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float time=2f;
    public int index;
    public int ObjInstance;
    private bool Check;
    public Transform[] SpawnPoint;
    public GameObject[] Obstacles;




    // Start is called before the first frame update
    void Start()
    {
        Check=true;
       

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Check)
        {
            StartCoroutine(CreateObstacle(time));
        }
       
    }

    IEnumerator CreateObstacle(float time)
    {
        Check=false;
        yield return new WaitForSeconds(2f);
        index=Random.Range(0, SpawnPoint.Length);
        for (var i = 0; i < 3; i++)
        {
            Instantiate(Obstacles[0], SpawnPoint[index].position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        Check=true;
    }
}
