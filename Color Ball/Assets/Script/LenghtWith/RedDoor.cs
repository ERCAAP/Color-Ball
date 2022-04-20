using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RedDoor : MonoBehaviour
{
    public static RedDoor Instance { set; get; }


    public int IncraseOrDecrease;
    private int IncraseWeight;

    public TextMeshProUGUI Number;
 
    private void Start()
    {
        Instance = this;
        HowMuchIncrase();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
    public void HowMuchIncrase()
    {
        IncraseOrDecrease = Random.Range(0, 2);
        switch (IncraseOrDecrease)
        {
            case 0:
                IncraseWeight = Random.Range(0, 19999);
                Number.text = "-" + IncraseWeight.ToString();
                break;
            case 1:
                IncraseWeight -= Random.Range(0, 500);
                Number.text = "4" + IncraseWeight.ToString();
                break;
 
        }
        Number.text = "-" + IncraseWeight.ToString();
    }
 


  
}
