using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private Rigidbody mRigidbody;
    private int mCrossedFloor;
    private GameControl mGCScript;

    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mGCScript = GameObject.Find("GameControl").GetComponent<GameControl>();

        mCrossedFloor = 0;
    }

    private void OnCollisionEnter(Collision pOther)
    {
       // Debug.Log("Collider "+pOther.collider.tag);
        if (pOther.collider.tag.Equals("platform"))
        {
            mRigidbody.velocity = new Vector3(0, 4, 0);
        }
    }

    private void OnTriggerEnter(Collider pOther)
    {
        Debug.Log("Trigger "+pOther.tag);

        if(pOther.tag.Equals("platform_miss"))
        {
            //Move Cylinder
            mRigidbody.isKinematic = true;
            GetComponent<SphereCollider>().isTrigger = true;
            mGCScript.isAreaMoved = true;
        }
        else if (pOther.tag.Equals("platform"))
        {
            //Move Cylinder
            mGCScript.isAreaMoved = false;
            mRigidbody.isKinematic = false;
            GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}
