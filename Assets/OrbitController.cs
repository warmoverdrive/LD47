using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [SerializeField]
    private Route[] routes;
    [SerializeField]
    float anchorPointMoveSpeed = 1f;
    [SerializeField]
    float minOrbitRadius = 3f;
    [SerializeField]
    float controlPointMoveSpeed = 0.25f;

    private void Start()
    {
        if (routes == null)
        {
            Debug.LogError("ERROR: NO ROUTES FOUND IN ORBIT CONTROLLER");
        }
    }

    private void Update()
    {
        float anchorDelta = anchorPointMoveSpeed * Time.deltaTime;
        float controlPointDelta = controlPointMoveSpeed * Time.deltaTime;

        //up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            VerticalOrbitManipulation(anchorDelta, controlPointDelta);
        }
        //down
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            VerticalOrbitManipulation(-anchorDelta, -controlPointDelta);
        }
        //left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HorizontalOrbitManipulation(anchorDelta, controlPointDelta);
        }
        //right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            HorizontalOrbitManipulation(-anchorDelta, -controlPointDelta);
        }
    }

    private void VerticalOrbitManipulation(float anchorDelta, float controlPointDelta)
    {
        //up anchors
        routes[0][3].Set(routes[0][3].x, routes[0][3].y + anchorDelta);
        routes[1][0].Set(routes[1][0].x, routes[1][0].y + anchorDelta);
        //up control points
        routes[0][1].Set(routes[0][1].x, routes[0][1].y + controlPointDelta);
        routes[1][2].Set(routes[1][2].x, routes[1][2].y + controlPointDelta);
        //down anchors
        routes[2][3].Set(routes[2][3].x, routes[2][3].y - anchorDelta);
        routes[3][0].Set(routes[3][0].x, routes[3][0].y - anchorDelta);
        //down control points
        routes[3][2].Set(routes[3][2].x, routes[3][2].y - controlPointDelta);
        routes[2][1].Set(routes[2][1].x, routes[2][1].y - controlPointDelta);
    }

    private void HorizontalOrbitManipulation(float anchorDelta, float controlPointDelta)
    {
        //left anchors
        routes[0][0].Set(routes[0][0].x - anchorDelta, routes[0][0].y);
        routes[3][3].Set(routes[3][3].x - anchorDelta, routes[3][3].y);
        //right anchors
        routes[1][3].Set(routes[1][3].x + anchorDelta, routes[1][3].y);
        routes[2][0].Set(routes[2][0].x + anchorDelta, routes[2][0].y);
        //left control points
        routes[0][2].Set(routes[0][2].x - controlPointDelta, routes[0][2].y);
        routes[3][1].Set(routes[3][1].x - controlPointDelta, routes[3][1].y);
        //right control points
        routes[1][1].Set(routes[1][1].x + controlPointDelta, routes[1][1].y);
        routes[2][2].Set(routes[2][2].x + controlPointDelta, routes[2][2].y);
    }
}