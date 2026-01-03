using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class CardUI : MonoBehaviour
{
    public Sprite frontImage; // mặt trước
    public Sprite backImage;  // mặt sau

    private Image image;

    [HideInInspector] public bool isFlipped = false; // true = đang hiển thị mặt trước
    [HideInInspector] public bool isMatched = false;

    public UnityEvent<CardUI> onCardClicked;

    private void Awake()
    {
        image = GetComponent<Image>();
        // Không set image = backImage ở Awake nếu bạn muốn show front trước.
        // Chúng ta sẽ set trong Start()
    }

    private void Start()
    {
        // Hiện mặt trước đầu tiên (không cho click khi đang hiện)
        isFlipped = true;
        image.sprite = frontImage;

        // Sau 1 giây, lật xuống mặt sau (Flip sẽ chuyển isFlipped -> false)
        StartCoroutine(ShowFrontThenBack(1f));
    }

    private IEnumerator ShowFrontThenBack(float wait)
    {
        yield return new WaitForSeconds(wait);
        // Lật xuống (Flip() sẽ chuyển isFlipped thành false và set sprite = backImage)
        Flip();
    }

    public void OnClick()
    {
        if (isMatched) return;   // đã ghép xong
        if (isFlipped) return;   // đang là mặt trước (hoặc đang khoá), chỉ cho phép click khi đang là mặt sau (isFlipped == false)

        Flip();
        onCardClicked?.Invoke(this);
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        image.sprite = isFlipped ? frontImage : backImage;
    }

    public void Hide()
    {
        isMatched = true;
        gameObject.SetActive(false);
    }
}
