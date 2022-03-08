using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable_Objects/Movement/Settings")]
public class MoveSettings : ScriptableObject
{
    public float speed { get { return _speed; } protected set { _speed = value; } }
    [SerializeField] private float _speed = 25.0f;

    public float jumpForce { get { return _jumpForce; } protected set { _jumpForce = value; } }
    [SerializeField] private float _jumpForce = 9.0f;
    
    public float antiBump { get { return _antiBump; } protected set { _antiBump = value; } }
    [SerializeField] private float _antiBump = 4.5f;

    public float gravity { get { return _gravity; } protected set { _gravity = value; } }
    [SerializeField] private float _gravity = -9.81f;
    
    public float crouchTimer { get { return _crouchTimer; } protected set { _crouchTimer = value; } }
    [SerializeField] private float _crouchTimer = 0f;
}
