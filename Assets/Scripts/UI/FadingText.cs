using System.Collections;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    private TextMeshPro text;
    public float fadeTime;
    public float moveSpeed;
    public float delayBeforeFade;
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        StartCoroutine(FadeText());
    }
    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.up;
    }
    private IEnumerator FadeText()
    {
        yield return new WaitForSeconds(delayBeforeFade);
        float timer = 0;
        Color originalColor = text.color;
        while(timer < fadeTime)
        {
            timer += Time.deltaTime;
            text.color = Color.Lerp(originalColor, Color.clear, timer / fadeTime);
        }
        Destroy(gameObject);
    }
}
