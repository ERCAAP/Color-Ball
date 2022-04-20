using System.Collections;
using UnityEngine;
using DG.Tweening;
public class ConnectedBa : MonoBehaviour
{
    public Transform connectedNode;
    public bool FollowTurnOff;
    public bool NewBallCheck;
    BallManager Manager;
    private void Start()
    {
        Manager = gameObject.GetComponentInParent<BallManager>();

    }
    private void FixedUpdate()
    {
        if (FollowTurnOff == false&&Manager.PoolOpen==false)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, connectedNode.localPosition.x, Time.deltaTime * 20),
             Mathf.Lerp(transform.localPosition.y, connectedNode.localPosition.y, Time.deltaTime * 20),
            Mathf.Lerp(transform.localPosition.z, connectedNode.localPosition.z-0.5f, Time.deltaTime * 20));
        }
        else if(NewBallCheck==true && Manager.PoolOpen == false)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, connectedNode.localPosition.x, Time.deltaTime * 20),
            Mathf.Lerp(transform.localPosition.y, connectedNode.localPosition.y, Time.deltaTime * 20),
         transform.localPosition.z);
        }
    }
    public IEnumerator FollowFix()
    {
        yield return new WaitForSeconds(2f);
        FollowTurnOff = false;
        NewBallCheck = true; 
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pool" && GameManager.Instance.NeedPoolNumber > GameManager.Instance.PoolIncraseNumber)
        {

            GameManager.Instance.PollObject = other.gameObject; // burda eşitlememiizin sebebi GameManagerde kullanmak için 
            GameManager.Instance.VeriablesFixText();

            Move.Instance.movespeed = 0;
            GameObject Go = Manager.PlayerBalls[0];
            Rigidbody rb = Go.GetComponent<Rigidbody>();

            Manager.PoolOpen = true;
            Go.GetComponent<ConnectedBa>().enabled = false;
            rb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            rb.AddForce(Random.Range(-3, 3), Random.Range(-5, -5), Random.Range(450,750),ForceMode.Force);
            Go.GetComponent<SphereCollider>().isTrigger = false;

            Manager.PlayerBalls.Remove(Go);
            Manager.LastCreatePos.z += 0.5f;
            Manager.TotalCharacter-=1;
            Manager.ReciverBall.Add(Go);

            Move.Instance.movespeed = 1;
            GameManager.Instance.GroupSheppFix();
        }
    else if (other.gameObject.tag == "Finish")
        {
            GameManager.Instance.PollObject = other.gameObject; // burda eşitlememiizin sebebi GameManagerde kullanmak için 
            GameManager.Instance.VeriablesFixText();

            Move.Instance.movespeed = 0;
            GameObject Go = Manager.PlayerBalls[0];
            Rigidbody rb = Go.GetComponent<Rigidbody>();

            Manager.PoolOpen = true;
            Go.GetComponent<ConnectedBa>().enabled = false;
            rb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            rb.AddForce(Random.Range(-3, 3), 0, Random.Range(2, 15), ForceMode.VelocityChange);
            Go.GetComponent<SphereCollider>().isTrigger = false;

            Manager.PlayerBalls.Remove(Go);

        }
    }
 


}
