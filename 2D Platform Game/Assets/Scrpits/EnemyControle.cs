using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;      // Düþmanýn hareket hýzý.
    public Transform pointA; // Hareket edeceði ilk nokta.
    public Transform pointB; // Hareket edeceði ikinci nokta.

    private Vector2 targetPosition; // Düþmanýn hedef konumu.
    private bool movingB;           // Düþmanýn B noktasýna mý hareket ettiðini kontrol eden bayrak.

    private void Start()
    {
        // Düþman baþlangýçta A noktasýndan hareket etmeli. Ýlk hedef B noktasý.
        targetPosition = pointB.position;
        transform.position = pointA.position; // Düþmaný baþlangýçta A noktasýna yerleþtiriyoruz.
    }

    private void Update()
    {
        // Düþmaný hedef konuma doðru hareket ettir.
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Düþman hedef konuma ulaþtýðýnda, yeni hedefi belirle.
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) // Hedefe yaklaþtýðýnda:
        {
            // Eðer B'ye ulaþtýysa, hedef A; A'ya ulaþtýysa, hedef B olacak.
            targetPosition = movingB ? pointA.position : pointB.position;

            // Hareket yönünü deðiþtir.
            movingB = !movingB;

            // Yönü görsel olarak tersine çevirmek için Flip fonksiyonunu çaðýr.
            Flip();
        }
    }

    private void Flip()
    {
        // Düþmanýn x eksenindeki ölçek deðerini ters çevirerek yönünü deðiþtirir.
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;                 // Yön deðiþikliði için x ölçeði ters çevrilir.
        transform.localScale = scaler; // Yeni ölçek deðeri düþmana uygulanýr.
    }
}