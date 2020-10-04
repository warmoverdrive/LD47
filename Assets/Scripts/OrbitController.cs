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
    float cPointMinDistFromAnchor = 1f;
    [SerializeField]
    float minOrbitRadius = 3f;
    [SerializeField]
    float maxOrbitRadius = 20f;
    [SerializeField]
    float topBoundary = 8f;
    [SerializeField]
    float bottomBoundary = -8f;
    [SerializeField]
    float rightBoundary = 14f;
    [SerializeField]
    float leftBoundary = -14f;
    [Header("Orbit Origin")]
    [SerializeField]
    private Transform orbitOrigin;
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

    private Vector3 up_vector = new Vector3(0, 1, 0);
    private Vector3 right_vector = new Vector3(1, 0, 0);

 
    private void Update()
    {
        float vertDist = Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position);
        float horDist = Vector2.Distance(anchors_E_W[0].position, anchors_E_W[1].position);

        if (Input.GetKey(KeyCode.W))
		{
           if (vertDist <= maxOrbitRadius && anchors_N_S[0].position.y <= topBoundary) AdjustNorth(Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (vertDist >= minOrbitRadius && 
                Vector2.Distance(controlPoints_U_D[0].position, anchors_E_W[0].position) >= cPointMinDistFromAnchor)
                AdjustNorth(-Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (horDist >= minOrbitRadius &&
                Vector2.Distance(controlPoints_R_L[0].position, anchors_N_S[0].position) >= cPointMinDistFromAnchor) 
                AdjustEast(-Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (horDist <= maxOrbitRadius && anchors_E_W[0].position.x <= rightBoundary) AdjustEast(Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (vertDist >= minOrbitRadius &&
                Vector2.Distance(controlPoints_U_D[2].position, anchors_E_W[0].position) >= cPointMinDistFromAnchor) 
                AdjustSouth(Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (vertDist <= maxOrbitRadius && anchors_N_S[1].position.y >= bottomBoundary) AdjustSouth(-Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (horDist <= maxOrbitRadius && anchors_E_W[1].position.x >= leftBoundary) AdjustWest(-Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (horDist >= minOrbitRadius &&
                Vector2.Distance(controlPoints_R_L[2].position, anchors_N_S[0].position) >= cPointMinDistFromAnchor)
                AdjustWest(Time.deltaTime);
        }

        AdjustOriginPoint();
    }

    private void AdjustNorth(float deltaTime)
	{
        float anchorDelta = anchorPointMoveSpeed * deltaTime;
        float cPointDelta = controlPointMoveSpeed * deltaTime;

        anchors_N_S[0].localPosition += (up_vector * anchorDelta);
        controlPoints_R_L[0].localPosition += (up_vector * anchorDelta);
        controlPoints_R_L[2].localPosition += (up_vector * anchorDelta);
        controlPoints_U_D[0].localPosition += (up_vector * cPointDelta);
        controlPoints_U_D[1].localPosition += (up_vector * cPointDelta);
    }

    private void AdjustSouth(float deltaTime)
    {
        float anchorDelta = anchorPointMoveSpeed * deltaTime;
        float cPointDelta = controlPointMoveSpeed * deltaTime;

        anchors_N_S[1].localPosition += (up_vector * anchorDelta);
        controlPoints_R_L[1].localPosition += (up_vector * anchorDelta);
        controlPoints_R_L[3].localPosition += (up_vector * anchorDelta);
        controlPoints_U_D[2].localPosition += (up_vector * cPointDelta);
        controlPoints_U_D[3].localPosition += (up_vector * cPointDelta);
    }
    private void AdjustEast(float deltaTime)
    {
        float anchorDelta = anchorPointMoveSpeed * deltaTime;
        float cPointDelta = controlPointMoveSpeed * deltaTime;

        anchors_E_W[0].localPosition += (right_vector * anchorDelta);
        controlPoints_U_D[0].localPosition += (right_vector * anchorDelta);
        controlPoints_U_D[2].localPosition += (right_vector * anchorDelta);
        controlPoints_R_L[0].localPosition += (right_vector * cPointDelta);
        controlPoints_R_L[1].localPosition += (right_vector * cPointDelta);
    }

    private void AdjustWest(float deltaTime)
    {
        float anchorDelta = anchorPointMoveSpeed * deltaTime;
        float cPointDelta = controlPointMoveSpeed * deltaTime;

        anchors_E_W[1].localPosition += (right_vector * anchorDelta);
        controlPoints_U_D[1].localPosition += (right_vector * anchorDelta);
        controlPoints_U_D[3].localPosition += (right_vector * anchorDelta);
        controlPoints_R_L[2].localPosition += (right_vector * cPointDelta);
        controlPoints_R_L[3].localPosition += (right_vector * cPointDelta);
    }

    private void AdjustOriginPoint()
    {
        float xAnchorDistance = Vector2.Distance(anchors_E_W[0].position, anchors_E_W[1].position);
        float yAnchorDistance = Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position);

        orbitOrigin.localPosition = new Vector2(
            anchors_E_W[0].position.x - (xAnchorDistance / 2),
            anchors_N_S[0].position.y - (yAnchorDistance / 2));
    }
}