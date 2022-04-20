using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float movespeed;
    public Vector3 RotateVec;
    public static Move Instance { set; get; }

    private void Start()
    {
        Instance = this;
    }
    private void LateUpdate()
    {
        transform.Translate(new Vector3(0, 0, movespeed * Time.deltaTime));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(RotateVec), 2 * Time.deltaTime);
    }

}