using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed;                      // Karakterin yatay h�z�n� belirler.
    public float jumpForce;                  // Karakterin z�plama g�c�n� belirler.

    private Rigidbody2D rb;                  // Fiziksel i�lemler i�in Rigidbody2D referans�.
    private Animator animator;               // Animasyonlar� kontrol etmek i�in Animator referans�.

    private bool facingRight;                // Karakterin y�n�n� (sa�a m�, sola m�) belirler.

    public Transform groundCheck;            // Zemin kontrol noktas�.
    public LayerMask groundLayer;            // Zemin olarak tan�mlanm�� layer.
    public float groundCheckRadius = 0.5f;   // Zemin kontrol� i�in kullan�lacak daire yar��ap�.

    private bool isJumping;                  // Z�plama durumunu takip eder.
    private bool isGrounded;                 // Karakterin zeminde olup olmad���n� takip eder.

    public int coin;                         // Toplanan coin say�s�n� tutar.
    public TextMeshProUGUI textMeshPro;      // Ekranda coin bilgisini g�stermek i�in.

    //public GameObject effect;                // Efekt objesi.

    public GameObject pauseGame;
    public GameObject continueGame;

    public GameObject PauseUI;

    public GameObject EndUI;

    //private bool isGamePause = false;

    private void Start()
    {
        // Gerekli bile�enlere eri�im sa�lan�r.
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();

        // Karakter ba�lang��ta sa�a bakar.
        facingRight = true; 

        pauseGame.gameObject.SetActive(true);
        continueGame.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Ekranda coin say�s�n� g�ncelle.
        textMeshPro.text = "TOTAL COIN : " + coin.ToString();

        // X ekseni hareket giri�i.
        float moveInput = Input.GetAxis("Horizontal"); 
        // Karakteri yatayda hareket ettir.
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0 && !facingRight)      // Oyuncu sola bak�yorsa biz sa� hareket yap�yorsak objeyi soldan sa�a d�nd�r.
            Flip();
        else if (moveInput < 0 && facingRight) 
            Flip();

        
        // Zemin kontrol�.
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

        // Z�plama i�lemi.
        if (Input .GetKeyDown(KeyCode.Space) && isGrounded == true) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            isJumping = true;
        }

        // Z�plama durumunu s�f�rla.
        if (isGrounded && isJumping) 
        {
            isJumping = false;
        }

        // Hareket animasyonlar�n� kontrol et.
        if (moveInput == 0) // Herhangi bir input de�eri yok ise - hi�bir t��a bas�lmam��sa - oyuncu hareket etmiyorsa.
        {
             animator.SetBool("Hareket", false); // Idle animasyonuna ge�.
        }
        else
        {
            animator.SetBool("Hareket", true);   // Running animasyonuna ge�.
        }

        Debug.Log("Total Coin : " + coin.ToString()); // Toplam coin bilgisini debug log'a yazd�r.

        // E�er Escape tu�una bas�ld�ysa ve EndPanel a��k de�ilse
        if (Input.GetKeyDown(KeyCode.Escape) && !EndUI.activeSelf)
        {
            PauseGame();
            PauseUI.SetActive(true);
        }
    }

    private void Flip()
    {
        // Karakterin y�n�n� de�i�tir.
        facingRight = !facingRight; 
        Vector2 scaler = transform.localScale; // Objenin scale de�eri.
        scaler.x *= -1;                        // Oyunucnun y�n� de�i�tit�inde scale de�eri de de�i�meli negatif y�nde.
        transform.localScale = scaler;         // Yeni scale de�erini oyunucunun scale de�erine ata.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // D��mana temas durumu.
        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Dikene temas ettiniz.");
            SceneManager.LoadScene(1); //Sahneyi yeniden ba�lat.
        }

        // D��mana temas durumu.
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("D��mana temas ettiniz.");
            SceneManager.LoadScene(1); //Sahneyi yeniden ba�lat.
        }
    }

    public void PauseGame()
    {
        pauseGame.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        pauseGame.gameObject.SetActive(true);
        continueGame.gameObject.SetActive(false);
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
