using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : NetworkBehaviour
{
    public Vector2 MovementSpeed = new Vector2(100.0f, 100.0f);
    public new Rigidbody2D rigidbody2D;
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    public new Camera camera;
    public TMP_Text playerNameText;

    private NetworkVariable<FixedString128Bytes> playerName = new NetworkVariable<FixedString128Bytes>("playerName", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        // Setup Rigidbody for frictionless top down movement and dynamic collision
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    void Update()
    {
        playerNameText.text = playerName.Value.ToString();
        if (!IsOwner) return;
        playerName.Value = UIManager.Instance.playerNameInput.text;

        camera.gameObject.SetActive(true);

        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        rigidbody2D.MovePosition(rigidbody2D.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
    }
}
