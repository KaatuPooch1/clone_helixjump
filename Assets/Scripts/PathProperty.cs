using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProperty : MonoBehaviour
{
    public string _PathName;
    public int _MaxPool;

    private float mSpeed = 20.0f;
    private ObjectPoolManager mOPMScript;
    private GameControl mGCScript;

    private void Start()
    {
        mOPMScript = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();
        mGCScript = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    public void Update()
    {
        if(transform.localPosition.y > 1)
        {
            //Destroy object
            mOPMScript.DestroyObject(this.gameObject);
        }

        if(mGCScript != null)
        {
            if(mGCScript.isAreaMoved)
            {
                transform.position = Vector3.MoveTowards(transform.localPosition, new Vector3(0, transform.localPosition.y + 0.2f, 0), mSpeed * Time.deltaTime);
            }
        }
    }
}
