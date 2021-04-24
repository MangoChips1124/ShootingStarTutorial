using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    [Tooltip("移动速度")]private float moveSpeed = 10.0f;

    [SerializeField]
    private float accelerationTime = 3f;

    [SerializeField]
    private float decelerationTime = 3f;

    [SerializeField]
    private float moveRotationAngle = 50;

    [SerializeField]
    private float paddingX;

    [SerializeField]
    private float paddingY;

    Rigidbody2D playerRigidbody;

    private Coroutine moveCoroutine;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerRigidbody.gravityScale = 0f;

        input.EnableGamePlay();
    }

    private void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }


    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move(Vector2 moveInput)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        Quaternion moveRotation = Quaternion.AngleAxis(moveRotationAngle * moveInput.y,Vector3.right);
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime,moveInput.normalized * moveSpeed,moveRotation));
        StartCoroutine(MovePositionLimitCoroutime());
    }

    private void StopMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime,Vector2.zero, Quaternion.identity));
        StopCoroutine(MovePositionLimitCoroutime());
    }

    //加减速控制
    private IEnumerator MoveCoroutine(float time,Vector2 moveVelocity,Quaternion moveRotation)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.fixedDeltaTime / time;
            playerRigidbody.velocity = Vector2.Lerp(playerRigidbody.velocity,moveVelocity,t/ time);
            transform.rotation = Quaternion.Lerp(transform.rotation,moveRotation,t / time);
            yield return null;
        }
    }

    //移位限制协程
    private IEnumerator MovePositionLimitCoroutime()
    {
        while (true)
        {
            transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position,paddingX,paddingY);
            yield return null;
        }
    }
}
