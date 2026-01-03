using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class StarTwinkleEffect : MonoBehaviour
{
    private Image starImage;

    public float minDuration = 0.5f; 
    public float maxDuration = 1.5f; 
    public float minDelay = 0f;      
    public float maxDelay = 1f; 

    void Start()
    {
        starImage = GetComponent<Image>();
        Twinkle();
    }

    void Twinkle()
    {
        float duration1 = Random.Range(minDuration, maxDuration);

        starImage.DOFade(0.3f, duration1) 
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                float duration2 = Random.Range(minDuration, maxDuration);
                starImage.DOFade(1f, duration2) 
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {
                        float randomDelay = Random.Range(minDelay, maxDelay);
                        Invoke("Twinkle", randomDelay);
                    });
            });
    }
}