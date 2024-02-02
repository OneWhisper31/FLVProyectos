using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Trivia : MonoBehaviour
{
    public ODSType typeODS;
    public TriviaSO[] triviaSO;

    [SerializeField] List<Questions> Questions { get => triviaSO[(int)typeODS].questions; }

    Questions currentQuestion;

    [SerializeField] Image izqCharacter;
    [SerializeField] Image derCharacter;

    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] TextMeshProUGUI answerText0;
    [SerializeField] TextMeshProUGUI answerText1;
    [SerializeField] TextMeshProUGUI answerText2;

    [SerializeField] DefaultButton answerButton0;
    [SerializeField] DefaultButton answerButton1;
    [SerializeField] DefaultButton answerButton2;

    [SerializeField] TriviaAnimHandler triviaAnim;

    [SerializeField] UnityEvent onEnd;
    public bool Finished { get => Questions.Count <= 0; }
    public Scenes nextScene;

    public void Start()
    {
        SetNewQuestion();
    }
    public void SetNewQuestion()
    {
        if (Questions.Count <= 0)
        {
            onEnd?.Invoke();
            return;
        }

        currentQuestion = Questions[0];
        Questions.RemoveAt(0);

        //si los necesito se usan
        //izqCharacter.sprite = currentQuestion.izqCharacter;
        //derCharacter.sprite = currentQuestion.derCharacter;

        questionText.text = currentQuestion.question;

        answerText0.text = currentQuestion.answers[0].text;
        answerText1.text = currentQuestion.answers[1].text;
        answerText2.text = currentQuestion.answers[2].text;
    }
    public void CheckAnswer(int index)
    {
        if (index >= currentQuestion.answers.Count)
            return;

        SetButtonsInteract(false);

        triviaAnim.NextQuestionAnim(currentQuestion.answers[index].isTrue);
    }
    public void SetButtonsInteract(bool value)
    {
        answerButton0.Interacteable = value;
        answerButton1.Interacteable = value;
        answerButton2.Interacteable = value;
    }
    //para los botones
    public void CheckAnswer0() => CheckAnswer(0);
    public void CheckAnswer1() => CheckAnswer(1);
    public void CheckAnswer2() => CheckAnswer(2);
}
[System.Serializable]
public struct Questions
{
    //si los necesito se usan
    //public Sprite izqCharacter;
    //public Sprite derCharacter;
    public string question;
    public List<Answer> answers;
}
[System.Serializable]
public struct Answer
{
    public string text;
    public bool isTrue;
}
