using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GreenDoor : MonoBehaviour
{
    public static GreenDoor Instance { set; get; }
    public int ForNumber;
    private int _Whichisincreasing;

    public List<string> StringT;
    public TextMeshProUGUI Number;
    public TextMeshProUGUI WhichTime;
   

    private void Start()
    {
        Instance = this;
        HowMuchIncrase();
        // yerlerini rast gele ver
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //  CharacterPool.Insantance.DoorAnimPlay();
        }
    }
    public void HowMuchIncrase()
    {
        _Whichisincreasing = Random.Range(0, StringT.Count);
        switch (_Whichisincreasing)
        {
            case 0:
                ForNumber = Random.Range(0, 19999);
                break;
            case 1:
                ForNumber = Random.Range(0, 500);
                break;
            case 2:
                ForNumber = Random.Range(0, 500);
                break;
        }
        Number.text = "+" + ForNumber.ToString();
        WhichTime.text = StringT[_Whichisincreasing];
    }

}
