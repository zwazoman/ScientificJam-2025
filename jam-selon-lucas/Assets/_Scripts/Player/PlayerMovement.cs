using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _playerSpeed;

    private void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        transform.position = new Vector2(transform.position.x + horizontalAxis * _playerSpeed * Time.deltaTime, transform.position.y + verticalAxis * _playerSpeed * Time.deltaTime);
    }
}
