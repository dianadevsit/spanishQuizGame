using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    private Game game;
    private Answer answer;
    private UnityEngine.UI.Image buttonImage;
    private AudioClip audioClip;
    // Start is called before the first frame update
    void Awake()
    {
        buttonImage = GetComponent<UnityEngine.UI.Image>();
        game = FindObjectOfType<Game>();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => game.CheckAnswer(answer.answerOptionText));
    }
    public void SetAnswerButton(Answer answer)
    {
        this.answer = answer;
        transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = answer.answerOptionButtonText;
        if (answer.answerOptionImage != null)
        {
            buttonImage.sprite = answer.answerOptionImage;
            buttonImage.type = UnityEngine.UI.Image.Type.Simple;
            buttonImage.color = Color.white;
        }

        if (answer.answerOptionSound != null)
        {
            gameObject.AddComponent<AudioSource>();
            audioClip = answer.answerOptionSound;
        }
        
    }
}
