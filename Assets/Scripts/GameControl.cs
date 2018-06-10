using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject GameAreaGO;
    public GameObject StartGameGO;

    [HideInInspector]
    public bool isAreaMoved = false;

    [HideInInspector]
    public ObjectPoolManager _OPMScript;
    private float mRotateSpeed = 100;

    private void Start()
    {
        _OPMScript = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);

            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Moved)
            {
                GameAreaGO.transform.Rotate(0f, -touch0.deltaPosition.x, 0f);
            }
        }
    }

    public void StartGame()
    {
        StartGameGO.SetActive(false);
        _OPMScript.ActivateObject(2);
        GameObject.Find("Ball_GO").GetComponent<Rigidbody>().isKinematic = false;
    }
}
