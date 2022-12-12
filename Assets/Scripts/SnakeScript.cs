using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * zigzag movement: https://answers.unity.com/questions/923659/zigzag-movement-whilst-moving-forward.html
 */

public class SnakeScript : EnemyScript
{
    Vector2 pos; // keeps track of the linear path to the target
    Vector2 axis; // axis that the sine movement is on
    Vector2 moveDir;

    float frequency = 10.0f; // speed of sine movement
    float magnitude = 0.5f; // size of sine movement

    protected override void UpdateTarget()
    {
        base.UpdateTarget();

        pos = transform.position;
        Vector2 targetPos = _target.transform.position;

        // do the same calculations as Vector2.MoveTowards to get the distance the snake should move
        float num = targetPos.x - _transform.position.x;
        float num2 = targetPos.y - _transform.position.y;
        float num3 = num * num + num2 * num2;
        float num4 = (float)Mathf.Sqrt(num3);

        moveDir = new Vector2(num / num4, num2 / num4);
        axis = moveDir * new Vector2(-1, 0);
        axis = axis.normalized;
    }


    protected override void DoMovement()
    {
        // if the snake is close to the plant, stop zigzag and move directly to plant
        if (Vector2.Distance(_transform.position, _target.transform.position) < 1.5)
        {
            base.DoMovement();
        }
        else
        {
            pos += moveDir * Time.deltaTime * _speed;
            _transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        }
    }

}
