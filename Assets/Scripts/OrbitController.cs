using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [Header("Design Levers")]
    [SerializeField]
    float anchorPointMoveSpeed = 1f;
    [SerializeField]
    float controlPointMoveSpeed = 0.25f;
    [SerializeField]
    float minOrbitRadius = 3f;
    [SerializeField]
    float maxOrbitRadius = 20f;
    [Header("Anchor Points")]
    [SerializeField]
    [Tooltip("Array of GameObjects acting as Vertical Anchor Points. Order in Array as N then S.")]
    private Transform[] anchors_N_S;
    [SerializeField]
    [Tooltip("Array of GameObjects acting as Horizontal Anchor Points. Order in Array as E then W.")]
    private Transform[] anchors_E_W;
    [Header("Control Points")]
    [SerializeField]
    [Tooltip("Array of GameObjects acting as Horizontal Control Points. Order in Array as Right then Left.")]
    private Transform[] controlPoints_R_L;
    [SerializeField]
    [Tooltip("Array of GameObjects acting as Vertical Control Points. Order in Array as Up then Down.")]
    private Transform[] controlPoints_U_D;
    

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //HORIZONTAL CONTRACT
        {
            if (Vector2.Distance(anchors_E_W[0].position, anchors_E_W[1].position) >= minOrbitRadius)
            {
                HorizontalOrbitAdjustment(-Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //HORIZONTAL EXPAND
        {
            if (Vector2.Distance(anchors_E_W[0].position, anchors_E_W[1].position) <= maxOrbitRadius)
            {
                HorizontalOrbitAdjustment(Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //VERTICAL EXPAND
        {
            if (Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position) <= maxOrbitRadius)
            {
                VerticalOrbitAdjustment(Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //VERTICAL CONTRACT
        {
            if (Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position) >= minOrbitRadius)
            {
                VerticalOrbitAdjustment(-Time.deltaTime);
            }
        }
    }

    private void HorizontalOrbitAdjustment(float deltaTime)
    {
        float anchorDelta = anchorPointMoveSpeed * deltaTime;
        float cPointDelta = controlPointMoveSpeed * deltaTime;
        
        //set anchors
        anchors_E_W[0].position = new Vector2(
            anchors_E_W[0].position.x + anchorDelta, anchors_E_W[0].position.y); //set east
        anchors_E_W[1].position = new Vector2(
            anchors_E_W[1].position.x - anchorDelta, anchors_E_W[1].position.y); //set west

        //set Right control points
        controlPoints_R_L[0].position = new Vector2(
            controlPoints_R_L[0].position.x + cPointDelta, anchors_N_S[0].position.y);
        controlPoints_R_L[1].position = new Vector2(
            controlPoints_R_L[1].position.x + cPointDelta, anchors_N_S[1].position.y);
        //set Left control points
        controlPoints_R_L[2].position = new Vector2(
            controlPoints_R_L[2].position.x - cPointDelta, anchors_N_S[0].position.y);
        controlPoints_R_L[3].position = new Vector2(
            controlPoints_R_L[3].position.x - cPointDelta, anchors_N_S[1].position.y);
    }

    private void VerticalOrbitAdjustment(float deltaTime)
    {
        float anchorDelta = anchorPointMoveSpeed * deltaTime;
        float cPointDelta = controlPointMoveSpeed * deltaTime;
        
        //set anchors
        anchors_N_S[0].position = new Vector2(
            anchors_N_S[0].position.x, anchors_N_S[0].position.y + anchorDelta); //set north
        anchors_N_S[1].position = new Vector2(
            anchors_N_S[1].position.x, anchors_N_S[1].position.y - anchorDelta); //set south

        //set Up control points
        controlPoints_U_D[0].position = new Vector2(
            anchors_E_W[0].position.x, controlPoints_U_D[0].position.y + cPointDelta);
        controlPoints_U_D[1].position = new Vector2(
            anchors_E_W[1].position.x, controlPoints_U_D[1].position.y + cPointDelta);
        //set Down control points
        controlPoints_U_D[2].position = new Vector2(
            anchors_E_W[0].position.x, controlPoints_U_D[2].position.y - cPointDelta);
        controlPoints_U_D[3].position = new Vector2(
            anchors_E_W[1].position.x, controlPoints_U_D[3].position.y - cPointDelta);
    }
}