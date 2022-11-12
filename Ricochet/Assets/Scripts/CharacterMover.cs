using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private Transform _rightLook;
    [SerializeField] private Transform _leftLook;
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private Transform _target;

    private Camera _cam;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _cam = Camera.main;
        Input.multiTouchEnabled = false;
    }

    private void LookAt(Transform look)
    {
        _transform.LookAt(look);
    }

    private void onDrag()
    {
        if (Input.GetMouseButton(0))
        {
            var screenPos = Input.mousePosition;
            screenPos.z = 10f;

            var targetPos = _cam.ScreenToWorldPoint(screenPos);
            targetPos.z = 0;
            _target.position = targetPos;
        }
    }

    private void Update()
    {
        onDrag();
    }

    private void FixedUpdate()
    {
        LookAt(_lookPoint.position.x > _transform.position.x ? _rightLook : _leftLook);
    }


}

