using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement

    void Update()
    {
        // Get input from the keyboard
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            float moveX = 0f;
            float moveZ = 0f;

            if (keyboard.wKey.isPressed) moveZ += 1f;
            if (keyboard.sKey.isPressed) moveZ -= 1f;
            if (keyboard.dKey.isPressed) moveX += 1f;
            if (keyboard.aKey.isPressed) moveX -= 1f;

            // Normalize movement and apply speed
            Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
