using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfDrag : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Camera camera;
    LineRenderer lr;
    Vector3 camOffset = new Vector3(0, 0, 10);
    private GameObject player;
    public Rigidbody2D GolfRigid;
    [SerializeField] AnimationCurve ac;

    // Start is called before the first frame update
    void Start()
    {
       // GolfRigid = GetComponent<Rigidbody2D>();
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
            GolfRigid.AddForce(new Vector2(2 * (startPos.x - endPos.x), 2 * (startPos.y - endPos.y)), ForceMode2D.Impulse);
            //GolfRigid.AddForce(new Vector2(10,10), ForceMode2D.Impulse);
        }
    }
}
