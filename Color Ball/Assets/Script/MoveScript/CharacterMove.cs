using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum HangiSpor
{
   Basketball=0,
   Futbol = 1,
}
public class CharacterMove : MonoBehaviour
{

    public bool GoMerkez;
   // public HangiSpor Spor;
    public static CharacterMove Instance { set; get; }

    private Vector3 PosFirst;

    public float ClampX;
    public float ClampY;
    public float ClampZ;
    public Vector3 Vec;
    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -ClampX, ClampX),(Mathf.Clamp(transform.localPosition.y, 0, ClampY)), 0);
        Vec = transform.localPosition;
    }
    // karakter x ekseninde hareketini kontrol ediyor
    private void OnMouseDown()
    {
        PosFirst = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

    }
    private void OnMouseDrag()
    {
      transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - PosFirst);
    }


   
    }
   

