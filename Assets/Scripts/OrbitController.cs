using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [Header("Design Levers")]
    [SerializeField]
    float originPointMoveSpeed = 1f;
    [SerializeField]
    float anchorPointMoveSpeed = 1f;
    [SerializeField]
    float controlPointMoveSpeed = 0.25f;
    [SerializeField]
    float cPointMinDistFromAnchor = 1f;
    [SerializeField]
    float minOrbitDiameter = 3f;
    [SerializeField]
    float maxOrbitDiameter = 20f;
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
    [Header("Starting Positions")]
    public StartingPositions sPos;

    [System.Serializable]
    public class StartingPositions
    {
        public Vector3 verticalAnchorOffset = new Vector3(0, 1.5f);
        public Vector3 horizontalAnchorOffset = new Vector3(1.5f, 0);
        public Vector3 verticalCPOffset = new Vector3(0, .75f);
        public Vector3 horizontalCPOffset = new Vector3(.75f, 0);
    }

    private void Start()
    {
        InitializeOrbitPoints();
    }

    private void InitializeOrbitPoints()
    {
        anchors_N_S[0].position = orbitOrigin.position + sPos.verticalAnchorOffset;
        anchors_N_S[1].position = orbitOrigin.position - sPos.verticalAnchorOffset;
        anchors_E_W[0].position = orbitOrigin.position + sPos.horizontalAnchorOffset;
        anchors_E_W[1].position = orbitOrigin.position - sPos.horizontalAnchorOffset;

        controlPoints_U_D[0].position = orbitOrigin.position +
            sPos.verticalCPOffset + sPos.horizontalAnchorOffset;
        controlPoints_U_D[1].position = orbitOrigin.position +
            sPos.verticalCPOffset - sPos.horizontalAnchorOffset;
        controlPoints_U_D[2].position = orbitOrigin.position -
            sPos.verticalCPOffset + sPos.horizontalAnchorOffset;
        controlPoints_U_D[3].position = orbitOrigin.position -
            sPos.verticalCPOffset - sPos.horizontalAnchorOffset;

        controlPoints_R_L[0].position = orbitOrigin.position +
            sPos.horizontalCPOffset + sPos.verticalAnchorOffset;
        controlPoints_R_L[1].position = orbitOrigin.position +
            sPos.horizontalCPOffset - sPos.verticalAnchorOffset;
        controlPoints_R_L[2].position = orbitOrigin.position -
            sPos.horizontalCPOffset + sPos.verticalAnchorOffset;
        controlPoints_R_L[3].position = orbitOrigin.position -
            sPos.horizontalCPOffset - sPos.verticalAnchorOffset;
    }

    private void Update()
    {
        float anchorDelta = anchorPointMoveSpeed * Time.deltaTime;
        float cPointDelta = controlPointMoveSpeed * Time.deltaTime;

        OriginInput();
        OrbitMovement(anchorDelta, cPointDelta);
    }

    private void OrbitMovement(float anchorDelta, float cPointDelta)
    {
        float horzDist = Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position);

        //orbit V expand
        if (Input.GetKey(KeyCode.I) &&
            anchors_N_S[0].position.y <= topBoundary &&
            anchors_N_S[1].position.y >= bottomBoundary &&
            Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position) <= maxOrbitDiameter)
        {
            VerticalOrbitMovement(anchorDelta, cPointDelta);
        }
        //orbit V contract
        if (Input.GetKey(KeyCode.K) &&
            Vector2.Distance(anchors_N_S[0].position, anchors_N_S[1].position) >= minOrbitDiameter &&
            Vector2.Distance(anchors_E_W[0].position, controlPoints_U_D[0].position) >= cPointMinDistFromAnchor)
        {
            VerticalOrbitMovement(-anchorDelta, -cPointDelta);
        }
        //orbit H expand
        if (Input.GetKey(KeyCode.L) &&
            anchors_E_W[0].position.x <= rightBoundary &&
            anchors_E_W[1].position.x >= leftBoundary &&
            Vector2.Distance(anchors_E_W[0].position, anchors_E_W[1].position) <= maxOrbitDiameter)
        {
            HorizontalOrbitMovement(anchorDelta, cPointDelta);
        }
        //orbit H contract
        if (Input.GetKey(KeyCode.J) &&
            Vector2.Distance(anchors_E_W[0].position, anchors_E_W[1].position) >= minOrbitDiameter &&
            Vector2.Distance(anchors_N_S[0].position, controlPoints_R_L[0].position) >= cPointMinDistFromAnchor)
        {
            HorizontalOrbitMovement(-anchorDelta, -cPointDelta);
        }
    }

    private void VerticalOrbitMovement(float anchorDelta, float cPointDelta)
    {
        //NORTH POINTS
        anchors_N_S[0].position += Vector3.up * anchorDelta;
        controlPoints_R_L[0].position += Vector3.up * anchorDelta;
        controlPoints_R_L[2].position += Vector3.up * anchorDelta;
        //SOUTH POINTS
        anchors_N_S[1].position += Vector3.down * anchorDelta;
        controlPoints_R_L[1].position += Vector3.down * anchorDelta;
        controlPoints_R_L[3].position += Vector3.down * anchorDelta;
        //FLANK POINTS
        controlPoints_U_D[0].position += Vector3.up * cPointDelta;
        controlPoints_U_D[1].position += Vector3.up * cPointDelta;
        controlPoints_U_D[2].position += Vector3.down * cPointDelta;
        controlPoints_U_D[3].position += Vector3.down * cPointDelta;
    }

    private void HorizontalOrbitMovement(float anchorDelta, float cPointDelta)
    {
        //EAST POINTS
        anchors_E_W[0].position += (Vector3.right * anchorDelta);
        controlPoints_U_D[0].position += (Vector3.right * anchorDelta);
        controlPoints_U_D[2].position += (Vector3.right * anchorDelta);
        //WEST POINTS
        anchors_E_W[1].position += (Vector3.left * anchorDelta);
        controlPoints_U_D[1].position += (Vector3.left * anchorDelta);
        controlPoints_U_D[3].position += (Vector3.left * anchorDelta);
        //FLANK POINTS
        controlPoints_R_L[0].position += (Vector3.right * cPointDelta);
        controlPoints_R_L[1].position += (Vector3.right * cPointDelta);
        controlPoints_R_L[2].position += (Vector3.left * cPointDelta);
        controlPoints_R_L[3].position += (Vector3.left * cPointDelta);
    }

    private void OriginInput()
    {
        //origin UP
        if (Input.GetKey(KeyCode.W) && anchors_N_S[0].position.y <= topBoundary)
        {
            AdjustOriginPoint(Vector3.up);
        }
        //origin DOWN
        if (Input.GetKey(KeyCode.S) && anchors_N_S[1].position.y >= bottomBoundary)
        {
            AdjustOriginPoint(Vector3.down);
        }
        //origin LEFT
        if (Input.GetKey(KeyCode.A) && anchors_E_W[1].position.x >= leftBoundary)
        {
            AdjustOriginPoint(Vector3.left);
        }
        // origin RIGHT
        if (Input.GetKey(KeyCode.D) && anchors_E_W[0].position.x <= rightBoundary)
        {
            AdjustOriginPoint(Vector3.right);
        }
    }

    private void AdjustOriginPoint(Vector3 direction)
    {
        orbitOrigin.transform.position += direction * originPointMoveSpeed * Time.deltaTime;
    }
}