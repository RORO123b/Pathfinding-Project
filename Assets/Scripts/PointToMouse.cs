using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour
{
    void Update()
    {
        if(PauseMenuMainGameScript.GameIsPaused==false)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            if (angle <= -90 && angle > -110)
                angle = -110;
            else if (angle > -90 && angle < -70)
                angle = -70;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
