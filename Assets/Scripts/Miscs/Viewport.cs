using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCanera = Camera.main;

        //将视口坐标转换为世界坐标
        Vector2 bottomLeft = mainCanera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 topRight = mainCanera.ViewportToWorldPoint(new Vector3(1, 1));

        minX = bottomLeft.x;
        minY = bottomLeft.y;

        maxX = topRight.x;
        maxY = topRight.y;
    }

    public Vector3 PlayerMoveablePosition(Vector3 playerPosition,float paddingX,float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Clamp(playerPosition.x,minX + paddingX, maxX - paddingX);
        position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);

        return position;

    }
}
