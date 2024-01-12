using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float _deltaX, _deltaY, _speed;
    
    private Transform _target;

    private void Update(){
        if(_target == null)
            _target = GameObject.FindGameObjectWithTag("Target").transform;

        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x + _deltaX, _target.position.y + _deltaY, -10f), Time.deltaTime * _speed);
    }
}