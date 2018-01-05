using UnityEngine;

public enum GameState { DEFAULT, MITOSIS, PULL, REVERSE, LEFT, RIGHT }

public class TestGameManager : MonoBehaviour
{
    public GameObject player;
    public GameState currentState = GameState.DEFAULT;
    public KeyCode keyPressed;
    PlayerController playerController;

    public delegate void StateEvent(GameState stateMessage);
    public static event StateEvent OnStateChanged;

    public void RaiseState(GameState _stateMessage)
    {
        if (OnStateChanged != null)
        {
            OnStateChanged(_stateMessage);
        }
    }

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        RaiseState(currentState);
    }

    //OnGUI is called multipe times each frame
    private void OnGUI()
    {
        Event evt = Event.current;
        if (evt.isKey)
        {
            keyPressed = evt.keyCode;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentState = GameState.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentState = GameState.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentState = GameState.DEFAULT;
        }
        if (Input.GetMouseButtonDown(1))
        {
            currentState = GameState.MITOSIS;
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentState = GameState.PULL;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentState = GameState.REVERSE;
        }
    }


    private void OnEnable()
    {
        OnStateChanged += playerController.ReceivedState;
    }
    private void OnDisable()
    {
        OnStateChanged -= playerController.ReceivedState;
    }
}
