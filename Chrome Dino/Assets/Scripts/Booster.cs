using UnityEngine;

public class Booster : MonoBehaviour
{
    public enum BoosterType
    {
        DoubleScore,
        Shield
    }

    [SerializeField] public BoosterType boosterType;
    [SerializeField] private float duration = 8f;

    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    private void Update()
    {
        transform.position += GameManager.Instance.gameSpeed
            * Time.deltaTime
            * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        switch (boosterType)
        {
            case BoosterType.DoubleScore:
                GameManager.Instance.ActivateDoubleScore(duration);
                break;

            case BoosterType.Shield:
                GameManager.Instance.ActivateShield();
                break;
        }

        Destroy(gameObject);
    }
}