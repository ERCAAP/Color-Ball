using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollow : MonoBehaviour
{
    public static LeaderFollow Instance { set; get; }
   public Transform Leader;
 //  public Transform LeaderY;
    public BallManager Manager;
    public bool Duzenle;
    private void Start()
    {
        GameManager.Instance.LeaderManager.Add(this);
        Leader = GameManager.Instance.Leader;
        Manager = gameObject.GetComponentInParent<BallManager>();
        //StartCoroutine(FixLeader());
        //LeaderY = Manager.LeadY;
    }
    IEnumerator FixLeader()
    {
        yield return new WaitForSeconds(1.5f);
        Duzenle = true;
    }
    void FixedUpdate()
    {
        if (Duzenle == true)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, Leader.localPosition.x, Time.deltaTime * 20),
            gameObject.transform.localPosition.y,
            Mathf.Lerp(transform.localPosition.z, Leader.localPosition.z, Time.deltaTime * 20));
        }
        else
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, Leader.localPosition.x, Time.deltaTime * 20),
           gameObject.transform.localPosition.y,
          gameObject.transform.localPosition.z);
        }


        /*
  
     */

    }
}
