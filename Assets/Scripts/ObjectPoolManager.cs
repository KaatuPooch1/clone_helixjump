using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject _Pool_GO;
    private int mMaxPathGenerate = 6;
    private List<GameObject> mInactiveObj = new List<GameObject>();
    private List<GameObject> mActiveObj = new List<GameObject>();

    private void Start()
    {
        ObjectPoolGenerator();
    }

    private void ObjectPoolGenerator()
    {
        GameObject inTemp = null;

        for(int i = 0; i < mMaxPathGenerate; ++i)
        {
            inTemp = Instantiate(_Pool_GO);
            inTemp.transform.position = new Vector3(0, (i * -8), 0);
            inTemp.name = "Game_Arena_" + i;
            inTemp.GetComponent<PathGenerator>().CreatePath();
            inTemp.SetActive(false);
            mInactiveObj.Add(inTemp);
        }
    } 

    public void ActivateObject(int pMax)
    {
        GameObject inTemp = null;

        for(int i = 0; i < pMax; ++i)
        {
            inTemp = mInactiveObj[0];
            inTemp.SetActive(true);

            if(inTemp.transform.childCount == 0)
            {
                inTemp.GetComponent<PathGenerator>().CreatePath();
            }

            if (mActiveObj.Count > 1)
            {
                inTemp.transform.position = mActiveObj[mActiveObj.Count - 1].transform.position + new Vector3(0, -8, 0);
            }

            mActiveObj.Add(inTemp);
            
            mActiveObj[mActiveObj.Count - 1].GetComponent<PathGenerator>().ListValue = mActiveObj.Count - 1;
            mInactiveObj.Remove(inTemp);
        }
    }

    public void DestroyObject(GameObject pObj)
    {
        Debug.Log("DestroyObject => "+pObj.name);

        pObj.SetActive(false);
        pObj.transform.position = Vector3.zero;
        pObj.GetComponent<PathGenerator>().DestroyPath();
        mActiveObj.RemoveAt(pObj.GetComponent<PathGenerator>().ListValue);
        mInactiveObj.Add(pObj);
    }
}
