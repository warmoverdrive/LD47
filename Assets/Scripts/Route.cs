using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private GameObject markerPrefab;
    [SerializeField]
    private Transform orbitOrigin;
    [Header("Anchor/Control Points")]
    [SerializeField]
    public Transform[] controlPoints;
    private Vector2 markerPos;
    private List<GameObject> dashes = new List<GameObject>();

    public Vector2 this[int index]
    {
        get { return controlPoints[index].position; }
        set { controlPoints[index].position = value; }
    }

    private void Start()
    {
        for (float t = 0; t <= 1; t += 0.15f)
        {
            markerPos = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            dashes.Add(Instantiate(markerPrefab, markerPos, RotateAroundOrigin(markerPos), transform));
            
        }
    }

    private void Update()
    {
        int i = 0;

        for (float t = 0; t <= 1; t += 0.15f)
        {
            markerPos = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            dashes[i].transform.position = markerPos;
            dashes[i].transform.rotation = RotateAroundOrigin(markerPos);
            i++;
        }
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            markerPos = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t , 3) * controlPoints[3].position;

            Gizmos.DrawSphere(markerPos, 0.1f);
        }

        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
    }

    private Quaternion RotateAroundOrigin(Vector3 newPos)
    {
        Vector3 difference = orbitOrigin.position - newPos;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
    }
}
