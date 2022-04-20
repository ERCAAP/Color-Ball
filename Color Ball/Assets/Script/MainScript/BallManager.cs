using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class BallManager : MonoBehaviour
{
    public GameObject Leader;
    public GameObject Player;
  
    public List<GameObject> PlayerBalls = new List<GameObject>();
    public List<GameObject> NewPlayerBalls = new List<GameObject>();
    public List<GameObject> ReciverBall = new List<GameObject>();
    public Vector3 LastCreatePos = Vector3.zero;
    public int TotalCharacter;
    public bool PoolOpen;
    private void Start()
    {
        GameManager.Instance.BallManager.Add(this);
        TotalCharacter = 1;
        CreateBall();

    }
    public int LastBallNumber()
    {
        int lastBall = PlayerBalls.Count - 1;
        return lastBall;
    }
    public Vector3 LastBallPos()
    {
        // sonuncu top
        Vector3 LastCubePos = PlayerBalls[LastBallNumber()].transform.localPosition;
        return LastCubePos;
    }

    public void CreateBall()
    {
        for (int i = 0; i < 20; i++)
        {
            GameManager.Instance.TotalBall += 1;
            GameManager.Instance.TotalBallText.text = GameManager.Instance.TotalBall.ToString();
            
            GameObject Go = Instantiate(Player);

            Go.transform.parent = transform;
            TotalCharacter += 1;
            LastCreatePos.z -= 0.50f;
            Go.transform.localPosition = new Vector3(0, 0, LastCreatePos.z);
            PlayerBalls.Add(Go);
         
        }
        for (int i = 1; i < PlayerBalls.Count; i++)
        {
            GameObject Go = PlayerBalls[i];
            Go.GetComponent<Renderer>().material.DOColor(GameManager.Instance.Mat[i].color, 1);
            Go.AddComponent<ConnectedBa>();
            Go.GetComponent<ConnectedBa>().connectedNode = PlayerBalls[i-1].transform;

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CreateNewbBall();
            NewPlayerBalls.Clear();
            Loop = 0;
        }
    }

 

    public Vector3 NewBallPos()
    {
        Vector3 vec = new Vector3(LastBallPos().x, LastBallPos().y,
                    LastCreatePos.z - 10);
        return vec;

    }

    public int Loop;
    public void CreateNewbBall()
    {
        while (Loop < 5)
        {
            /*
            if (ReciverBall.Count > 1)
            {
                for (int i = 0; i < 2; i++)
                {

                    GameObject Go = ReciverBall[i];
                    Go.GetComponent<Rigidbody>().isKinematic = true;
                    Go.transform.localPosition = Vector3.zero;
                    Go.GetComponent<Rigidbody>().isKinematic = false;
              
                    Go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotation;
                    Go.GetComponent<SphereCollider>().isTrigger = true;
                    Go.GetComponent<ConnectedBa>().enabled = true;
                    Go.GetComponent<ConnectedBa>().FollowTurnOff = true;
                    Go.GetComponent<ConnectedBa>().NewBallCheck = true;
                    Go.GetComponent<ConnectedBa>().connectedNode = PlayerBalls[LastBallNumber()].transform;
                 
                    TotalCharacter += 1;
                    Go.transform.localPosition = NewBallPos();
                    LastCreatePos.z -= 0.50f;
                    LastBallPos();

                    NewPlayerBalls.Add(Go);
                    PlayerBalls.Add(Go);

                    NewPlayerBalls[i].transform.DOLocalMoveZ(LastCreatePos.z, 1).OnComplete(() =>
                    NewPlayerBalls[i].transform.DOLocalMoveZ(LastBallPos().z, 0.5f)); // 
                    Loop++;
                    ReciverBall.Remove(Go);
                }
            
            }
              */

            GameObject Go = Instantiate(Player);
            Go.transform.parent = transform;
            Go.AddComponent<ConnectedBa>();
            Go.GetComponent<ConnectedBa>().FollowTurnOff = true;
            Go.GetComponent<ConnectedBa>().NewBallCheck = true;
            Go.GetComponent<ConnectedBa>().connectedNode = PlayerBalls[LastBallNumber()].transform;

            TotalCharacter += 1;
            Go.transform.localPosition = NewBallPos();
            LastCreatePos.z -= 0.50f;

            LastBallPos();

            NewPlayerBalls.Add(Go);
            PlayerBalls.Add(Go);

            NewPlayerBalls[Loop].transform.DOLocalMoveZ(LastCreatePos.z, 1).OnComplete(() =>
            NewPlayerBalls[Loop].transform.DOLocalMoveZ(LastBallPos().z, 0.5f)); // 
            Loop++;

        }
        if (Loop == 5)
        {
            StartCoroutine(ConnectedBagFıx());
        }
    }
    public IEnumerator ConnectedBagFıx()
    {
        for (int i = 0; i < NewPlayerBalls.Count; i++)
        {
            NewPlayerBalls[i].GetComponent<ConnectedBa>().FollowTurnOff = false;
            NewPlayerBalls[i].GetComponent<ConnectedBa>().NewBallCheck = false;
        }
        yield return new WaitForSeconds(0.01f);
    }
    public void AddLeader()
    {
        GameObject go = PlayerBalls[0];
        Rigidbody rb = go.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;


        go.GetComponent<SphereCollider>().isTrigger = false;
        go.AddComponent<LeaderFollow>();
        go.GetComponent<LeaderFollow>().Leader = GameManager.Instance.Leader;
        //go.GetComponent<LeaderFollow>().Duzenle = true;
        go.GetComponent<ConnectedBa>().FollowTurnOff = true;
        go.GetComponent<LeaderFollow>().Duzenle = false;

    }



}
