using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    public float moveSpeed = 10;
    public Transform ball;
    public Transform arms;
    public Transform POV;

    public Transform PD;
    public Transform target;

    // variables
    private bool isBallInHands = true;
    private bool isBallFlying = false;
    private float t = 0;

    // Update is called once per frame
    void Update()
    {

        // walking
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(-moveHorizontal, 0.0f, -moveVertical);
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
        //transform.LookAt(transform.position + direction);

        // ball in hands
        if (isBallInHands)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // hold over head
                ball.position = POV.position;
                arms.localEulerAngles = Vector3.right * 180;

                // look towards the target
                //transform.LookAt(target.position);
            }

            // dribbling
            else
            {
                ball.position = PD.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                arms.localEulerAngles = Vector3.right * 0;
            }

            // throw ball
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isBallInHands = false;
                isBallFlying = true;
                t = 0;
            }
        }

        // ball in the air
        if (isBallFlying)
        {
            t += Time.deltaTime;
            float duration = 0.66f;
            float t01 = t / duration;

            // move to target
            Vector3 a = POV.position;
            Vector3 b = target.position;
            Vector3 pos = Vector3.Lerp(a, b, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            ball.position = pos + arc;

            // moment when ball arrives at the target
            if (t01 >= 1)
            {
                isBallFlying = false;
                ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBallInHands && !isBallFlying)
        {
            isBallInHands = true;
            ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}

