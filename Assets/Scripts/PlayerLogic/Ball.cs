using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [Header("Property")] [SerializeField] private float speed = 1f;
    [SerializeField] private float _upForce;
    [SerializeField] private float _firstUpForce;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _waitingTimeBetweenClick;
    [SerializeField] private float _addToRandomForce;
    [SerializeField] private float _gravityScaller;
    [SerializeField] private float _sideMovement;

    public GameObject BallRot;
    private float xtouchPoint;

    [Header("Text")] [SerializeField] private Text _totalScoreText;
    [SerializeField] private Text _currentScoreText;

    private int _currentScore;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider2D;
    private Vector3 _positionToMoveRight;
    private Vector3 _positionToMoveLeft;
    private Vector3 _positionToMoveUp;

    private RaycastHit2D _raycastHit2D;
    private bool _CanPush = true;
    private bool _IsFirstPush = true;
    private float _randomSideMovement;
    private Vector3 _mousePosition;
    private Ray _ray;
    private Camera _camera;
    private RaycastHit2D _hit;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        PushOnClick();
    }

    private void PushOnClick()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        _hit = Physics2D.Raycast(_ray.origin, _ray.direction, Mathf.Infinity);

       
        if (_hit && Input.GetMouseButtonDown(0) && GameManager.GameOver == false && _CanPush )
        {
            xtouchPoint = _hit.point.x;
          
            SetRandomPosition();
            PushUp();
        }
    }

    private void Initialize()
    {
        GameManager.GameOver = false;
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<CircleCollider2D>();
        _rigidbody.gravityScale = 0;

        _totalScoreText.text = PlayerMoney.TotalScore.ToString();

        _positionToMoveRight = new Vector3(-_sideMovement, 1);
        _positionToMoveLeft = new Vector3(_sideMovement, 1);
    }

    private void SetRandomPosition()
    {
        _randomSideMovement = Random.Range(-_sideMovement, _sideMovement);
        _positionToMoveUp = new Vector3(_randomSideMovement, 1);
    }

    IEnumerator WaitingToPush()
    {
        yield return new WaitForSeconds(_waitingTimeBetweenClick);
        _CanPush = true;
    }

    void PushUp()
    {
        _CanPush = false;

        _rigidbody.gravityScale = _gravityScaller;

        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Move ball left or right depends on click position
        if (_mousePosition.x > 1)
        {
            PushBall(_positionToMoveRight);
            _currentScore++;
        }
        else if (_mousePosition.x < -1)
        {
            PushBall(_positionToMoveLeft);
            _currentScore++;
        }
        else
        {
            PushBall(_positionToMoveUp);
            _currentScore++;
        }

        PlayerMoney.ComboCount++;
        PlayerMoney.TotalScore++;

        _currentScoreText.text = _currentScore.ToString();

        StartCoroutine(WaitingToPush());
    }

    private void PushBall(Vector3 positionToMove)
    {
        _upForce = Random.Range(_upForce, _upForce + _addToRandomForce);
        //_torqueForce = Random.Range(-_torqueForce, _torqueForce);

        if (xtouchPoint >0) // Добавил метод при котором в зависимости от косания на экране по оси Х, мяч будет лететь в противоположную сторону
        {
            _torqueForce = Random.Range(0, _torqueForce);
        }
        else
        {
            _torqueForce = Random.Range(-_torqueForce, 0);
        }

        if (_IsFirstPush)
        {
            _rigidbody.AddForce(positionToMove * (_firstUpForce * Time.fixedDeltaTime), ForceMode2D.Force);
            _IsFirstPush = false;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(positionToMove * (_upForce * Time.fixedDeltaTime), ForceMode2D.Force);
        }

        
       BallRot.GetComponent<Rigidbody2D>().AddTorque(_torqueForce * Time.fixedDeltaTime, ForceMode2D.Impulse); 
       // _rigidbody.AddTorque(_torqueForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}