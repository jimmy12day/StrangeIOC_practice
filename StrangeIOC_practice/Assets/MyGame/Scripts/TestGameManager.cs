using UnityEngine;

public enum GameState { DEFAULT, MITOSIS, PULL, REVERSE }

public class TestGameManager : MonoBehaviour
{
    public GameObject player;
    public GameState currentState;
    public KeyCode keyPressed;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        UpdateState();
    }

    private void OnGUI()
    {
        Event evt = Event.current;
        if (evt.isKey)
        {
            //if (evt.type == EventType.KeyDown)
            //{
            keyPressed = evt.keyCode;
            Debug.Log(evt.keyCode);
            //}
        }
    }
    private void UpdateState()
    {
        //if (Input.anyKeyDown)
        //{
        //    Debug.Log(Input.inputString);
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentState = GameState.MITOSIS;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentState = GameState.DEFAULT;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentState = GameState.PULL;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentState = GameState.REVERSE;
        }
    }

}

