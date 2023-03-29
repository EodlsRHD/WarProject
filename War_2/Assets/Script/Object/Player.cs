using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerBehaviour
{
    idle,
    walk,
    run,
    attack,
    die
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private GameObject _cameraArm = null;

    [SerializeField]
    private eCharacterType _characterType = eCharacterType.Non;

    private ePlayerBehaviour _playerBehaviour = ePlayerBehaviour.idle;

    public eCharacterType characterType
    {
        get { return _characterType; }
    }

    [Header("Property")]
    [SerializeField]
    private CharacterController _controller = null;

    [SerializeField]
    private Animator _animator = null;

    [Header("Info")]
    [SerializeField]
    private float _maxPower = 0f;

    [SerializeField]
    private float _movePower = 0f;

    private Vector3 _moveDir = Vector3.zero;

    private float _gravity = -0.98f;

    [SerializeField]
    private float _cameraSensitive = 0f;

    private float _cameraUpAngle = 60f;

    private float _cameraDownAngle = 310f;

    private void Start()
    {
        StartCoroutine(PlayerBehaviour());
    }

    private IEnumerator PlayerBehaviour()
    {
        switch(_playerBehaviour)
        {
            case ePlayerBehaviour.idle:

                break;

            case ePlayerBehaviour.walk:

                break;

            case ePlayerBehaviour.run:

                break;

            case ePlayerBehaviour.attack:

                break;

            case ePlayerBehaviour.die:

                break;
        }

        yield return null;
    }

    private void Update()
    {
        Move();
        View();

        MouseClick();
    }

    private void Move()
    {
        if(_controller.isGrounded == true)
        {
            MoveDetect();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _moveDir.y += _maxPower * 2f;
            }
        }
        else
        {
            _moveDir.y += _gravity;
        }

        _controller.Move(_moveDir * Time.deltaTime);
    }

    private void MoveDetect()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            _playerBehaviour = ePlayerBehaviour.idle;

            _movePower = 0f;

            _moveDir = Vector3.zero;
            return;
        }

        _playerBehaviour = ePlayerBehaviour.walk;

        Dash();

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 lookForward = _cameraArm.transform.forward.normalized;
        Vector3 lookRight = _cameraArm.transform.right.normalized;

        _moveDir = ((lookForward * moveInput.y) + (lookRight * moveInput.x)).normalized * _movePower;
    }

    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerBehaviour = ePlayerBehaviour.run;


            _movePower += (_maxPower * 0.1f);

            if (_movePower > _maxPower + (_maxPower * 0.5f))
            {
                _movePower = _maxPower + (_maxPower * 0.5f);
            }

            return;
        }

        _movePower = _maxPower;
    }

    private void View()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * _cameraSensitive, Input.GetAxisRaw("Mouse Y") * _cameraSensitive);
        Vector3 camAngle = _cameraArm.transform.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        float y = camAngle.y + mouseDelta.x;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, _cameraUpAngle);
        }
        if (x > 180f)
        {
            x = Mathf.Clamp(x, _cameraDownAngle, 361f);
        }

        _cameraArm.transform.rotation = Quaternion.Euler(x, y, camAngle.z);
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(this.transform.position, ray.direction, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Fild"))
                {
                    return;
                }

                if (hit.collider.CompareTag("Building"))
                {
                    return;
                }

                if (hit.collider.CompareTag("Unit"))
                {
                    return;
                }
            }
        }
    }
}
