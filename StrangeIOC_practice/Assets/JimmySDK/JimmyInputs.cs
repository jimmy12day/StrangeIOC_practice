namespace JimmySDK
{
    using UnityEngine;
    public class JimmyInputs : MonoBehaviour
    {
        public static Vector3 MousePosition()
        {
            return new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }

        public static Vector3 ScreenToWorldPosition(Camera mainCamera)
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));// Set z to 10f only when the camera projection type is NOT orthographic
        }
    }
}