using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIscript : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Camera camera;
    LineRenderer lr;
    Vector3 camOffset = new Vector3(0, 0, 10);
    private GameObject player;
    [SerializeField] AnimationCurve ac;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            lr.enabled = true;
            lr.positionCount = 2;
            // startPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(0, player.transform.position);
            lr.useWorldSpace = true;
            lr.widthCurve = ac;
            lr.numCapVertices = 10;
            lr.startColor = Color.red;
        }
        if (Input.GetMouseButton(0))
        {
            endPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.endColor = Color.red;
            lr.SetPosition(1, endPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }
    }
}
