using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed;                      // Karakterin yatay hýzýný belirler.
    public float jumpForce;                  // Karakterin zýplama gücünü belirler.

    private Rigidbody2D rb;                  // Fiziksel iþlemler için Rigidbody2D referansý.
    private Animator animator;               // Animasyonlarý kontrol etmek için Animator referansý.

    private bool facingRight;                // Karakterin yönünü (saða mý, sola mý) belirler.

    public Transform groundCheck;            // Zemin kontrol noktasý.
    public LayerMask groundLayer;            // Zemin olarak tanýmlanmýþ layer.
    public float groundCheckRadius = 0.5f;   // Zemin kontrolü için kullanýlacak daire yarýçapý.

    private bool isJumping;                  // Zýplama durumunu takip eder.
    private bool isGrounded;                 // Karakterin zeminde olup olmadýðýný takip eder.

    public int coin;                         // Toplanan coin sayýsýný tutar.
    public TextMeshProUGUI textMeshPro;      // Ekranda coin bilgisini göstermek için.

    //public GameObject effect;                // Efekt objesi.

    public GameObject pauseGame;
    public GameObject continueGame;

    public GameObject PauseUI;

    public GameObject EndUI;

    //private bool isGamePause = false;

    private void Start()
    {
        // Gerekli bileþenlere eriþim saðlanýr.
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();

        // Karakter baþlangýçta saða bakar.
        facingRight = true; 

        pauseGame.gameObject.SetActive(true);
        continueGame.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Ekranda coin sayýsýný güncelle.
        textMeshPro.text = "TOTAL COIN : " + coin.ToString();

        // X ekseni hareket giriþi.
        float moveInput = Input.GetAxis("Horizontal"); 
        // Karakteri yatayda hareket ettir.
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0 && !facingRight)      // Oyuncu sola bakýyorsa biz sað hareket yapýyorsak objeyi soldan saða döndür.
            Flip();
        else if (moveInput < 0 && facingRight) 
            Flip();

        
        // Zemin kontrolü.
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

        // Zýplama iþlemi.
        if (Input .GetKeyDown(KeyCode.Space) && isGrounded == true) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            isJumping = true;
        }

        // Zýplama durumunu sýfýrla.
        if (isGrounded && isJumping) 
        {
            isJumping = false;
        }

        // Hareket animasyonlarýný kontrol et.
        if (moveInput == 0) // Herhangi bir input deðeri yok ise - hiçbir týþa basýlmamýþsa - oyuncu hareket etmiyorsa.
        {
             animator.SetBool("Hareket", false); // Idle animasyonuna geç.
        }
        else
        {
            animator.SetBool("Hareket", true);   // Running animasyonuna geç.
        }

        Debug.Log("Total Coin : " + coin.ToString()); // Toplam coin bilgisini debug log'a yazdýr.

        // Eðer Escape tuþuna basýldýysa ve EndPanel açýk deðilse
        if (Input.GetKeyDown(KeyCode.Escape) && !EndUI.activeSelf)
        {
            PauseGame();
            PauseUI.SetActive(true);
        }
    }

    private void Flip()
    {
        // Karakterin yönünü deðiþtir.
        facingRight = !facingRight; 
        Vector2 scaler = transform.localScale; // Objenin scale deðeri.
        scaler.x *= -1;                        // Oyunucnun yönü deðiþtitðinde scale deðeri de deðiþmeli negatif yönde.
        transform.localScale = scaler;         // Yeni scale deðerini oyunucunun scale deðerine ata.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Düþmana temas durumu.
        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Dikene temas ettiniz.");
            SceneManager.LoadScene(1); //Sahneyi yeniden baþlat.
        }

        // Düþmana temas durumu.
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Düþmana temas ettiniz.");
            SceneManager.LoadScene(1); //Sahneyi yeniden baþlat.
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
