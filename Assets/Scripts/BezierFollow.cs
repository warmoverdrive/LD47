using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    public Route[] routes;
    private int currentRoute;
    private float tParam;
    private Vector2 charPosition;
    [SerializeField]
    private float speedModifier = 0.5f;
    private bool coroutineAllowed;

    private void Start()
    {
        currentRoute = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    private void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(ExecuteRoute(currentRoute));
        }
    }

    private IEnumerator ExecuteRoute(int routeNumber)
    {
        coroutineAllowed = false;

        while (tParam < 1)
        {
            Vector2 p0 = routes[routeNumber][0];
            Vector2 p1 = routes[routeNumber][1];
            Vector2 p2 = routes[routeNumber][2];
            Vector2 p3 = routes[routeNumber][3];

            tParam += Time.deltaTime * speedModifier;

            charPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = charPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        currentRoute += 1;

        if (currentRoute > routes.Length - 1)
            currentRoute = 0;

        coroutineAllowed = true;
    }

    
}
