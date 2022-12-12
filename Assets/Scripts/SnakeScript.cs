using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : EnemyScript
{
    public GameObject _tempPrefab;

    GameObject _tempRight;
    //GameObject _tempLeft;

    Transform _tempRightTransform;

    Vector2 _targetPos;
    Vector2 _rightOffsetPoint;
    //Vector2 _leftOffsetPoint;

    float _lastSwitch;
    bool returnTarget;

    protected override void Start()
    {
        base.Start();
        _lastSwitch = Time.time;
        returnTarget = true;
    }

    protected override void UpdateTarget()
    {
        // call parent
        base.UpdateTarget();
        Vector2 targetPos = _target.transform.position;

        // calculate offset points for target

        /*
        float shortSide = Vector2.Distance(targetPos, _transform.position);
        float longSide = shortSide * Mathf.Sqrt(3);
        float hypotenuse = 2 * shortSide;

        float rightY = ((shortSide * shortSide) + (hypotenuse * hypotenuse) - (longSide * longSide)) / (2 * shortSide);
        float rightX = Mathf.Sqrt((hypotenuse * hypotenuse) + (rightY * rightY));
        _rightOffsetPoint = new Vector3(rightX, rightY, 0);

        float leftX = targetPos.x - Mathf.Abs(Mathf.Abs(targetPos.x) - Mathf.Abs(rightX));
        float leftY = targetPos.y - Mathf.Abs(Mathf.Abs(targetPos.y) - Mathf.Abs(rightY));
        _leftOffsetPoint = new Vector3(leftX, leftY, 0);

        // replace this with tempLeft and tempRight
        Instantiate(temp, _rightOffsetPoint, Quaternion.identity);
        Instantiate(temp, _leftOffsetPoint, Quaternion.identity);
        */

        float xDif = Mathf.Abs(Mathf.Abs(targetPos.x) - Mathf.Abs(_transform.position.x));
        float yDif = Mathf.Abs(Mathf.Abs(targetPos.y) - Mathf.Abs(_transform.position.y));

        //float xDif = _transform.position.x < targetPos.x ? 3 : -3;
        //float yDif = _transform.position.y < targetPos.y ? 3 : -3;

        if (xDif > yDif)
        {
            _rightOffsetPoint = new Vector2(targetPos.x + (_transform.position.x < targetPos.x ? 3 : -3), targetPos.y);
        }
        else
        {
            _rightOffsetPoint = new Vector2(targetPos.x, targetPos.y + (_transform.position.y < targetPos.y ? 3 : -3));
        }
        



        CleanUp();
        _tempRight = Instantiate(_tempPrefab, _rightOffsetPoint, Quaternion.identity);
        _tempRightTransform = _tempRight.transform;

        //_leftOffsetPoint = new Vector2(targetPos.x - (yDif > 2 ? 3 : 0), targetPos.y - (xDif > 2 ? 3 : 0));
        //Instantiate(temp, _leftOffsetPoint, Quaternion.identity);

    }

    // we alternate movement towards 2 points that are next to the target to make serpentine movement 
    protected override Vector2 GetTargetPos()
    {
        Vector2 targetPos = _target.transform.position;
        if (Vector2.Distance(_transform.position, targetPos) < 4)
        {
            return base.GetTargetPos();
        }

        if (Time.time - _lastSwitch > 1)
        {
            _lastSwitch = Time.time;
            returnTarget = !returnTarget;
            
        }

        if (returnTarget)
        {
            return base.GetTargetPos();
        }
        else
        {
            return _rightOffsetPoint;
        }

    }

    public void CleanUp()
    {
        Destroy(_tempRight);
        //Destory(_tempLeft);
    }
}
