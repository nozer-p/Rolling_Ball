using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 tapPos;

    [SerializeField] private float maxDeadZone;
    private Vector3 direction;
    private float delta = 0;

    private bool isSwiping;
    private bool isMobile;

    private Player ball;
    [SerializeField] private LineRenderer line;
    [SerializeField] private float offsetLine;
    [SerializeField] private float lineOnY;
    [SerializeField] private float offset;

    private void Start()
    {
        //Time.timeScale = 0.3f;
        ball = FindObjectOfType<Player>();
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }

        if (isSwiping)
        {
            CheckSwipe();
        }
    }

    private void CheckSwipe()
    {
        direction = -Input.mousePosition + (Vector3)tapPos;
            
        if (direction.magnitude > maxDeadZone)
        {
            direction = direction.normalized * maxDeadZone;
        }

        delta = direction.magnitude / offsetLine;

        line.SetPosition(0, new Vector3(ball.gameObject.transform.position.x, lineOnY, ball.gameObject.transform.position.z));
        line.SetPosition(1, new Vector3(ball.gameObject.transform.position.x - direction.x / offsetLine, lineOnY, ball.gameObject.transform.position.z - direction.y / offsetLine));
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        ball.ChangeDirection(direction);
        ball.ChangeDelta(delta);
        delta = 0f;
        direction = Vector3.zero;
        line.SetPosition(0, new Vector3(ball.gameObject.transform.position.x, lineOnY, ball.gameObject.transform.position.z));
        line.SetPosition(1, new Vector3(ball.gameObject.transform.position.x, lineOnY, ball.gameObject.transform.position.z));
        tapPos = Vector2.zero;
    }
}