using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriviaAnimHandler : MonoBehaviour
{
    [SerializeField] Introduction introduction;
    [SerializeField] Transform[] introductionElements;

    [SerializeField] Transform triviaObj,button,triviaQuestion,triviaAnswer1, triviaAnswer2, triviaAnswer3,singGood,singBad;

    [SerializeField] Trivia trivia;

    

    public void MakeTransition()
    {
        triviaObj.localScale = Vector3.zero;
        triviaObj.gameObject.SetActive(true);

        trivia.Initialize();

        button.DOScale(0, 0.5f)
            .OnComplete(() => {


                introductionElements[0].gameObject.SetActive(false);
                for (int i = 1; i < introductionElements.Length-1; i++)
                    introductionElements[i].DOScale(0, 0.1f);
                introductionElements[introductionElements.Length - 1].DOScale(0, 0.3f)

                .OnComplete(() => triviaObj.DOScale(1, 0.5f)
                .OnComplete(() =>
                {
                    introduction.enabled=false;
                    for (int i = 0; i < introductionElements.Length - 1; i++)
                        introductionElements[i].gameObject.SetActive(false);

                    button.gameObject.SetActive(false);
                    trivia.SetButtonsInteract(true);
                }));

            });
    }
    public void NextQuestionAnim(bool answerWereRight)
    {//achica las tres respuestas juntas, dspues achica la pregunta, dspues setea las nuevas y la despliega uno por uno

        if (answerWereRight)
            singGood.DOScale(1, 0.5f).OnComplete(()=> StartCoroutine(NextAnim()));
        else
            singBad.DOScale(1, 0.5f).OnComplete(() => StartCoroutine(NextAnim()));
    }
    public IEnumerator NextAnim()
    {
        if (trivia.Finished)
        {
            yield return new WaitForSecondsRealtime(3f);
            trivia.SetNewQuestion();
            yield break;
        }

        yield return new WaitForSecondsRealtime(2f);
        singGood.DOScale(0, 0.5f);
        singBad.DOScale(0, 0.5f);
        yield return new WaitForSecondsRealtime(1f);


        triviaAnswer2.DOScale(0, 0.5f);
        triviaAnswer3.DOScale(0, 0.5f);
        triviaAnswer1.DOScale(0, 0.5f)
            .OnComplete(() => triviaQuestion.DOScale(0, 0.5f)
                .OnComplete(() => {

                    trivia.SetNewQuestion();

                triviaQuestion.DOScale(1, 0.5f)
                    .OnComplete(() => triviaAnswer1.DOScale(1, 0.5f)
                    .OnComplete(() => triviaAnswer2.DOScale(1, 0.5f)
                    .OnComplete(() => triviaAnswer3.DOScale(1, 0.5f)
                    .OnComplete(() => trivia.SetButtonsInteract(true)
                    ))));
                }));
    }
}
