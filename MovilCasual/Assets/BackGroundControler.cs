using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackGroundControler : MonoBehaviour
{
    public float speed;
    public GameObject[] Layers1;
    public Transform start;
    public Transform end;
    private float count = 1;
    private float saveSpeed;
    // Update is called once per frame
    void Update()
    {
        saveSpeed = speed;
        for (int a = 0; a < Layers1.Length; a++)
        {
            Layers1[a].transform.position = new Vector3(Layers1[a].transform.position.x, Layers1[a].transform.position.y - speed * Time.deltaTime, Layers1[a].transform.position.z) ;
            if(Layers1[a].transform.position.y == end.position.y)
            {
                Layers1[a].transform.position = new Vector3(start.position.x, start.position.y, Layers1[a].transform.position.z);
            }
            else if(Layers1[a].transform.position.y < end.position.y)
            {
                float dif = Mathf.Abs(Layers1[a].transform.position.y) - Mathf.Abs(end.position.y);
                Layers1[a].transform.position = new Vector3(start.position.x,start.position.y - dif, Layers1[a].transform.position.z);
            }
            if(a == count)
            {
                count = a + 2;
                speed *= 0.8f;
            }
        }
        count = 1;
        speed = saveSpeed;
    }
}
