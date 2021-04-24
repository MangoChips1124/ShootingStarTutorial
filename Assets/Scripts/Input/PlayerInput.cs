using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="PlayerInput")]
public class PlayerInput : ScriptableObject,InputActions.IGamePlayActions
{
    public event UnityAction<Vector2> onMove = delegate { };//���忪ʼ�ƶ��¼�
    public event UnityAction onStopMove = delegate { };//����ֹͣ�ƶ��¼�
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
    /// �����������롣ת������ʱ������������
    /// </summary>
    public void DisableAllInput()
    {
        Debug.Log("Click -02");
        inputActios.GamePlay.Disable();
    }

    /// <summary>
    /// ����������Ҫ��ʱ������GamePlay������
    /// </summary>
    public void EnableGamePlay()
    {
        inputActios.GamePlay.Enable();
        Debug.Log("Click -01");
        Cursor.visible = false;//���������α�
        Cursor.lockState = CursorLockMode.Locked;//�������״̬
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        /*
        InputActionPhase.Disabled ����������
        InputActionPhase.Canceled �����ź�ֹͣʱ�̣����ɿ���������һ֡
        InputActionPhase.Performed ���붯����ִ�У������������ºͰ�ס�����׶Σ�
        InputActionPhase.Started ���°�������һ֡
        InputActionPhase.Waiting ���������ã���û����Ӧ�ź�����
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
