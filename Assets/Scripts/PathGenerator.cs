using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public int ListValue;
    public List<GameObject> _Path_GO = new List<GameObject>();
    private List<GameObject> mActiveGO = new List<GameObject>();
    private int mMaxPath = 5;

    private float mSpeed = 30.0f;
    private GameControl mGCScript;
    private Vector3 mVelocity = Vector3.zero;
    private void Start()
    {
        mGCScript = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    public void Update()
    {
        if (mGCScript != null)
        {
            if (mGCScript.isAreaMoved == true)
            {
                //transform.position = new Vector3(0, transform.position.y + 1.6f, 0);//Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y + 1.6f, 0), mSpeed * Time.deltaTime);
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, transform.position.y + 1.6f, 0), ref mVelocity, 0.3f);
            }

            transform.rotation = mGCScript.GameAreaGO.transform.rotation;
        }

        //Destroy Object
        if(transform.position.y >= 10)
        {
            mGCScript._OPMScript.DestroyObject(gameObject);
        }
    }

    public void CreatePath()
    {
        GameObject inTemp = null;
        int inCurrentHideChild = 0;
        int inPreviousHideChild = 0;
        int inCurrentChild = 0;
        int inPreviousChild = 0;

        for (int i = 0; i < mMaxPath; ++i)
        {
            inTemp = Instantiate(_Path_GO[Random.Range(0, _Path_GO.Count)]);
            inTemp.transform.parent = transform;
            inTemp.name = "Path_" + i;

            inCurrentChild = inTemp.transform.childCount;
            inCurrentHideChild = Random.Range(0, inTemp.transform.childCount - 1);

            if(inPreviousChild == inCurrentChild)
            {
                if(inPreviousHideChild == inCurrentHideChild)
                {
                    if (inCurrentHideChild == 0)
                        inCurrentHideChild++;
                    else if(inCurrentHideChild == inCurrentChild - 1)
                    {
                        inCurrentHideChild = 0;
                    }
                    else
                    {
                        inCurrentChild--;
                    }
                        
                }
            }
     
            inTemp.transform.localPosition = new Vector3(0, -0.2f * i);

            //Setup for child
            inTemp.transform.GetChild(inCurrentHideChild).gameObject.GetComponent<MeshRenderer>().enabled = false;
            inTemp.transform.GetChild(inCurrentHideChild).gameObject.GetComponent<MeshCollider>().isTrigger = true;
            inTemp.transform.GetChild(inCurrentHideChild).gameObject.tag = "platform_miss";

            inPreviousHideChild = inCurrentHideChild;
            inPreviousChild = inCurrentChild;

            mActiveGO.Add(inTemp);
        }
    }

    public void DestroyPath()
    {
        Debug.Log("DestroyPath => "+mActiveGO.Count);
        foreach(GameObject inObj in mActiveGO)
        {
            Debug.Log("Name => "+inObj.name);
            Destroy(inObj);
        }

        mActiveGO.Clear();
        mGCScript._OPMScript.ActivateObject(1);
    }
}
