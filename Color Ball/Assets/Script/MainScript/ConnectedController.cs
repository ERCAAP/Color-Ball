using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedController : MonoBehaviour
{
    public GameObject Create;
    public Vector3 vec;
    public List<GameObject> Balls = new List<GameObject>();
    private void Start()
    {
        for (int i = 1; i < 50; i++)
        {
            GameObject go = Instantiate(Create);
            go.transform.parent = transform;
            vec.z -= 0.5f;
            go.transform.localPosition = vec;
            Balls.Add(go);
            go.AddComponent<ConnectedBa>();
            go.GetComponent<ConnectedBa>().connectedNode = Balls[i - 1].transform;

        }
    }
}
