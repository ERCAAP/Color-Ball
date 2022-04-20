using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public GameObject Game;
    public static GameManager Instance { set; get; }
    [Header("BallGrup")]
    public int TotalBall;
    public TextMeshProUGUI TotalBallText;
    public Transform Leader;
    public GameObject BallGroup;

    private bool Xcontrol;
    public float MinX;
    public float MaX;
    public List<Gradient> Color;
    public List<Material> Mat;

    [Header("PoolGrup")]
    public int PoolIncraseNumber;
    public int NeedPoolNumber;
    public List<BallManager> BallManager;
    public List<LeaderFollow> LeaderManager;
    public GameObject PollObject;
    public TextMeshProUGUI PoolIncraseNumberText;
    public TextMeshProUGUI NeedPoolText;

    public CinemachineVirtualCamera CameraMain;
    private void Awake()
    {
        Instance = this;
        NeedBall();
       CreateGrup();

    }

    public Vector3 PlayerPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        newPos.y = 0.5f;
        return newPos;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            WeidgtIncrase();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(CameraPosition());
        }
    }
    public void WeidgtIncrase()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Xcontrol == false)
            {
                MinX -= 0.5F;
                GameObject go = Instantiate(BallGroup);
                go.transform.parent = Game.transform;
                go.transform.localPosition = new Vector3(MinX, 0, 0);
                Xcontrol = true;
            }
            else if (Xcontrol == true)
            {
                MaX += 0.5F;
                GameObject go = Instantiate(BallGroup);
                go.transform.parent = Game.transform;
                go.transform.localPosition = new Vector3(MaX, 0, 0);
                Xcontrol = false;
            }

        }

    }
    public void CreateGrup()
    {
        GameObject goP = Instantiate(BallGroup);
        goP.transform.parent = Game.transform;
        goP.transform.localPosition = new Vector3(0, 0, 0);
        Xcontrol = true;
        for (int i = 0; i < 4; i++)
        {
            if (Xcontrol == false)
            {
                MinX -= 0.5F;
                GameObject go = Instantiate(BallGroup);
                go.transform.parent = Game.transform;
                go.transform.localPosition = new Vector3(MinX, 0, 0);    
                Xcontrol = true;
            }
            else if (Xcontrol == true)
            {
                MaX += 0.5F;
                GameObject go = Instantiate(BallGroup);
                go.transform.parent = Game.transform;
                go.transform.localPosition = new Vector3(MaX, 0, 0);
                Xcontrol = false;
            }
        
        }
     
    }

    public void GroupSheppFix()
    {

        if (NeedPoolNumber == PoolIncraseNumber)
        {
            Move.Instance.movespeed = 0;
          
            for (int i = 0; i < BallManager.Count; i++)
            {
                BallManager[i].AddLeader();
                Destroy(BallManager[i].ReciverBall[0].GetComponent<LeaderFollow>());
                BallManager[i].PoolOpen = false;
                // YOLU GETİR animasyonunu oynat particle effect oynat kenarda kapı acılma effecti yap ve ilerle
            }
            StartCoroutine(CallPoolFinishEvent());
        }
    }
    public IEnumerator CameraPosition()
    {
        Vector3 CameraPos = CameraMain.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        CameraMain.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 
            12.5f, 
            Mathf.Lerp(CameraPos.z,CameraPos.z-5,Time.deltaTime));
        Vector3 newPos = CameraMain.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        yield return new WaitForSeconds(0.5f);
        CameraMain.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 12.5f, Mathf.Lerp(newPos.z,newPos.z+5, Time.deltaTime));
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator CallPoolFinishEvent()
    {
        yield return new WaitForSeconds(0.5f);
        PollObject.GetComponent<PoolEvent>().FinishEvent();
        yield return new WaitForSeconds(1.55f);
        Move.Instance.movespeed = 15;
        EditLenght();

        
    }
    public void EditLenght()
    {
        for (int i = 0; i < LeaderManager.Count; i++)
        {
            LeaderManager[i].Duzenle = true;
        }
    }
    public void NeedBall()
    {
        NeedPoolNumber = 15;
        NeedPoolText.text = "/" + NeedPoolNumber.ToString();
    }
    public void VeriablesFixText()
    {
        PoolIncraseNumber += 1;
        PoolIncraseNumberText.text =PoolIncraseNumber.ToString();
        TotalBall -= 1;
        TotalBallText.text = TotalBall.ToString();
    }

}
