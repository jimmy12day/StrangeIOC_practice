using UnityEngine;


public class OnKeyBoardEvents : MonoBehaviour
{

    public delegate void KeyPressedAction(KeyCode key);
    public static event KeyPressedAction OnPressedAction;

    public bool isMoveLeft, isMoveRight, isJump, isRunning;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnPressedAction(KeyCode.A);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnPressedAction(KeyCode.S);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnPressedAction(KeyCode.D);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPressedAction(KeyCode.Space);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnPressedAction(KeyCode.LeftShift);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnPressedAction(KeyCode.LeftControl);
        }
    }

    private void OnEnable()
    {
        OnPressedAction += KeyBoardMethod;
        OnPressedAction += KeyBoardPrint;
    }

    private void OnDisable()
    {
        OnPressedAction -= KeyBoardMethod;
        OnPressedAction -= KeyBoardPrint;

    }

    public void KeyBoardMethod(KeyCode key)
    {
        if (key == KeyCode.D)
            isMoveRight = true;
        else
            isMoveRight = false;

        if (key == KeyCode.A)
            isMoveLeft = true;
        else
            isMoveLeft = false;

        if (key == KeyCode.Space)
            isJump = true;
        else
            isJump = false;

        if (key == KeyCode.LeftShift)
            isRunning = true;
        else
            isRunning = false;
    }

    public void KeyBoardPrint(KeyCode key)
    {
        Debug.Log(key);
    }
}
