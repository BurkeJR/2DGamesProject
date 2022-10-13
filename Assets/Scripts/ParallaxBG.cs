using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    Vector2 _StartPos;
    [SerializeField] int _moveModifier;

    private void Start()
    {
        _StartPos = transform.position;
    }

    private void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, _StartPos.x + (pz.x * _moveModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, _StartPos.y + (pz.y * _moveModifier), 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}
