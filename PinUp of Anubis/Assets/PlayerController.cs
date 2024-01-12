using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _fallThreshold = -0.5f, _jumpThreshold = 0.5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rb;
    private PolygonCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer; // Для управления спрайтом
    private bool _isGrounded = true;
    private int _groundLayer; // Индекс слоя "Ground"

    private const string HORIZONTALAXIS = "Horizontal";
    private const string VERTICALAXIS = "Vertical";
    private const string GROUNDLAYERNAME = "Ground";

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _groundLayer = LayerMask.NameToLayer(GROUNDLAYERNAME);
    }

    private void Update() {
        float moveInput = SimpleInput.GetAxis(HORIZONTALAXIS);
        _rb.velocity = new Vector2(moveInput * _moveSpeed, _rb.velocity.y);

        // Отзеркаливаем спрайт в зависимости от направления движения
        if (moveInput != 0) {
            _spriteRenderer.flipX = moveInput < 0;
        }

        // Прыжок
        if (SimpleInput.GetAxis(VERTICALAXIS) > _jumpThreshold && _isGrounded) {
            _rb.velocity = Vector2.up * _jumpForce;
            _isGrounded = false;
            StartCoroutine(DisableCollisionTemporarily());
        }

        // Падение
        if (SimpleInput.GetAxis(VERTICALAXIS) < _fallThreshold) {
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == _groundLayer) {
            _isGrounded = true;
        }
    }

    private IEnumerator DisableCollisionTemporarily() {
        Physics2D.IgnoreLayerCollision(gameObject.layer, _groundLayer, true);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, _groundLayer, false);
    }
}
