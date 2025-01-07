using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;      // D��man�n hareket h�z�.
    public Transform pointA; // Hareket edece�i ilk nokta.
    public Transform pointB; // Hareket edece�i ikinci nokta.

    private Vector2 targetPosition; // D��man�n hedef konumu.
    private bool movingB;           // D��man�n B noktas�na m� hareket etti�ini kontrol eden bayrak.

    private void Start()
    {
        // D��man ba�lang��ta A noktas�ndan hareket etmeli. �lk hedef B noktas�.
        targetPosition = pointB.position;
        transform.position = pointA.position; // D��man� ba�lang��ta A noktas�na yerle�tiriyoruz.
    }

    private void Update()
    {
        // D��man� hedef konuma do�ru hareket ettir.
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // D��man hedef konuma ula�t���nda, yeni hedefi belirle.
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) // Hedefe yakla�t���nda:
        {
            // E�er B'ye ula�t�ysa, hedef A; A'ya ula�t�ysa, hedef B olacak.
            targetPosition = movingB ? pointA.position : pointB.position;

            // Hareket y�n�n� de�i�tir.
            movingB = !movingB;

            // Y�n� g�rsel olarak tersine �evirmek i�in Flip fonksiyonunu �a��r.
            Flip();
        }
    }

    private void Flip()
    {
        // D��man�n x eksenindeki �l�ek de�erini ters �evirerek y�n�n� de�i�tirir.
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;                 // Y�n de�i�ikli�i i�in x �l�e�i ters �evrilir.
        transform.localScale = scaler; // Yeni �l�ek de�eri d��mana uygulan�r.
    }
}