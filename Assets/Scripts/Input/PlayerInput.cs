using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="PlayerInput")]
public class PlayerInput : ScriptableObject,InputActions.IGamePlayActions
{
    public event UnityAction<Vector2> onMove = delegate { };//定义开始移动事件
    public event UnityAction onStopMove = delegate { };//定义停止移动事件
    InputActions inputActios;

    void OnEnable()
    {
        Debug.Log("Click -04");
        inputActios = new InputActions();

        inputActios.GamePlay.SetCallbacks(this);
    }

    void OnDisable()
    {
        Debug.Log("Click -03");
        DisableAllInput();
    }

    /// <summary>
    /// 禁用所有输入。转场动画时需禁用鼠标输入
    /// </summary>
    public void DisableAllInput()
    {
        Debug.Log("Click -02");
        inputActios.GamePlay.Disable();
    }

    /// <summary>
    /// 其他类在需要的时候启用GamePlay动作表
    /// </summary>
    public void EnableGamePlay()
    {
        inputActios.GamePlay.Enable();
        Debug.Log("Click -01");
        Cursor.visible = false;//隐藏鼠标的游标
        Cursor.lockState = CursorLockMode.Locked;//锁定鼠标状态
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        /*
        InputActionPhase.Disabled 动作表被禁用
        InputActionPhase.Canceled 输入信号停止时刻，即松开按键的那一帧
        InputActionPhase.Performed 输入动作已执行（包含按键按下和按住两个阶段）
        InputActionPhase.Started 按下按键的那一帧
        InputActionPhase.Waiting 动作表被启用，但没有相应信号输入
        */
        if (context.phase == InputActionPhase.Performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onStopMove.Invoke();
        }
    }

}
