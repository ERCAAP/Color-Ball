using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class SpherMotor : MonoBehaviour
{


    public static SpherMotor Instance { set; get; }
    public float Ydelay = 0.25f;
    public float movementDelay = 0.25f;
    public GameObject Player;

    public List<GameObject> PlayerBalls = new List<GameObject>();
    public List<GameObject> Xsurusu = new List<GameObject>();
    public List<GameObject> NewBalls = new List<GameObject>();

    public Stack<GameObject> ReciverBal = new Stack<GameObject>();
    public bool AnaTakipKapat;
    public int character;
    public Vector3 pos = Vector3.zero;

    public bool TakipKapat;
    public bool ZFllow;
    private void Start()
    {
     
        // burda başlangıcta bir sanıyelıgıne en sürü liderini takip etsin 1 saniye sonra globaldekine donsun ama eger ekrana 
        // basılıysa globaldekını takie devam etsin

        Instance = this;
        CreateBall();
    }
  
    private void FixedUpdate()
    {
    //    MoveListElement();

       if (Input.GetButtonUp("Fire1"))
        {
            NewBall();
            NewBalls.Clear();
            TakipKapat = false;
        }

    }

    public void CreateBall() 
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject Go = Instantiate(Player);
            Go.transform.parent = transform;
            pos.z -= 0.50f;
            character += 1;
            PlayerBalls.Add(Go);
            Xsurusu.Add(Go);
            gameObject.transform.GetChild(1).localPosition = new Vector3(0, 0, pos.z);
            PlayerBalls[character].transform.localPosition = new Vector3(0, 0, pos.z);
            Go.AddComponent<ConnectedBa>();
            Go.GetComponent<ConnectedBa>().connectedNode= PlayerBalls[i].transform;
        }
    } 
    
    private void MoveListElement()
    {
       if (AnaTakipKapat == false && TakipKapat==false)
            {
                for (int i = 1; i < PlayerBalls.Count; i++) // eskiler burayı 
                {
                    Vector3 pos = PlayerBalls[i].transform.localPosition;
                    pos.x = Xsurusu[i - 1].transform.localPosition.x;
                    pos.x = Mathf.Clamp(pos.x, -4, 4);
                    pos.y = PlayerBalls[i - 1].transform.localPosition.y;
                    pos.z = PlayerBalls[i - 1].transform.localPosition.z;
                    pos.z -= 0.5f;
                    PlayerBalls[i].transform.DOLocalMoveY(pos.y, Ydelay);
                    Xsurusu[i].transform.DOLocalMoveX(pos.x, movementDelay);
                    PlayerBalls[i].transform.DOLocalMoveZ(pos.z, Ydelay);
                }
            }
       else if(AnaTakipKapat==true && TakipKapat==false)
            {
                for (int i = 1; i < Xsurusu.Count; i++) // eskiler burayı 
                {
                    Vector3 pos = PlayerBalls[i].transform.localPosition;
                    pos.x = Xsurusu[i - 1].transform.localPosition.x;
                    pos.x = Mathf.Clamp(pos.x, -4, 4);
                    pos.y = PlayerBalls[i - 1].transform.localPosition.y;
                    PlayerBalls[i].transform.DOLocalMoveY(pos.y, Ydelay);
                    Xsurusu[i].transform.DOLocalMoveX(pos.x, movementDelay);
                }
            }
        else if(TakipKapat==true)
            {
            for (int i = 1; i < PlayerBalls.Count; i++) // eskiler burayı 
            {
                Vector3 pos = PlayerBalls[i].transform.localPosition;
                pos.x = Xsurusu[i - 1].transform.localPosition.x;
                pos.x = Mathf.Clamp(pos.x, -4, 4);
                pos.y = PlayerBalls[i - 1].transform.localPosition.y;
                pos.z = PlayerBalls[i - 1].transform.localPosition.z;
                pos.z -= 0.5f;
                PlayerBalls[i].transform.DOLocalMoveY(pos.y, Ydelay);
                Xsurusu[i].transform.DOLocalMoveX(pos.x, movementDelay);
                if (ZFllow == false)
                {
                    PlayerBalls[i].transform.DOLocalMoveZ(pos.z, 0.5f);
                }
            }
        }
    }


    private int Loop;

    public Vector3 NewBallPos()
    {
        Vector3 vec = new Vector3(Xsurusu[Xsurusu.Count - 1].transform.localPosition.x,
                    PlayerBalls[0].transform.localPosition.y,
                    pos.z - 10);
        return vec;

    }
    public Vector3 LastBallPos()
    {
        int lastBall = PlayerBalls.Count - 1; // sonuncu top
        Vector3 LastCubePos = PlayerBalls[lastBall].transform.localPosition;
        return LastCubePos;
    }

    public void NewBall() // kaset yarat
    {
        AnaTakipKapat = true;
        while (Loop < 5)
        {
            if (ReciverBal.Count > 0)
            {
                GameObject go = ReciverBal.Pop();
                go.SetActive(true);
                go.transform.parent = transform;
                pos.z -= 0.5f;

                go.transform.localPosition = NewBallPos();

                character += 1;
              

                NewBalls.Add(go);
                Xsurusu.Add(go);
                PlayerBalls.Add(go);

                // burda kaseti gitmesi gerektiği yere götüyüroyuz
                NewBalls[Loop].transform.DOLocalMoveZ(pos.z, 1).OnComplete(() =>
                NewBalls[Loop].transform.DOLocalMoveZ(LastBallPos().z - 0.5f, 0.005f)); // 
                Loop++;
            }
            else
            {
                GameObject go = Instantiate(Player);
                go.transform.parent = transform;
                pos.z -= 0.5f;
                go.transform.localPosition = NewBallPos();

                character += 1;
                int lastBall = PlayerBalls.Count - 1; // sonuncu top
                Vector3 LastCubePos = PlayerBalls[lastBall].transform.localPosition;

                NewBalls.Add(go);
                Xsurusu.Add(go);
                PlayerBalls.Add(go);

                // burda kaseti gitmesi gerektiği yere götüyüroyuz
                NewBalls[Loop].transform.DOLocalMoveZ(pos.z, 1).OnComplete(() =>
                NewBalls[Loop].transform.DOLocalMoveZ(LastBallPos().z- 0.5f, 0.005f)); // 
                Loop++;
     
            }
      
        }
        Invoke(nameof(TidyHerd),1f);
    }

    public void TidyHerd()
    {
        AnaTakipKapat = false;
        Loop = 0;
    }
    
    public IEnumerator PoolShot()
    {
        for (int i = 1; i < 10; i++) // eskiler burayı 
        {
            GameObject go = PlayerBalls[i];

      
            yield return new WaitForSeconds(0.5f);
            go.GetComponent<ConnectedBa>().FollowTurnOff = true;
            go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            go.GetComponent<Rigidbody>().AddForce(0, 2, 15, ForceMode.Impulse);
            go.GetComponent<SphereCollider>().isTrigger = false;

        }
    }
    public void PoolShoot()
    {
        StartCoroutine(PoolShot());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Leader")
        {
            Move.Instance.movespeed = 0;
            PoolShoot();
        }
    }

}


