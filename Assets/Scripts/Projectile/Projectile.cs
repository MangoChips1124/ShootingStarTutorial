using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ×Óµ¯ÒÆ¶¯Àà
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private Vector2 moveDirection;

    private Coroutine move;

    private void OnEnable()
    {
        move = StartCoroutine(MoveDirection());
    }

    IEnumerator MoveDirection()
    {
        while(gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
