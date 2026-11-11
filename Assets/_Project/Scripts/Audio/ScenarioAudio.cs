using UnityEngine;
using VRTraining.Core.Events;
using VRTraining.Scenario.Data;
using VRTraining.Scenario.Events;

namespace VRTraining.Audio
{
    // звуки сценария: правильно, ошибка, конец сценария
    // слушает события и сам ни к кому не обращается
    [RequireComponent(typeof(AudioSource))]
    public class ScenarioAudio : MonoBehaviour
    {
        private AudioSource audioSource;
        private AudioClip successClip;
        private AudioClip errorClip;
        private AudioClip finishClip;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            // две ноты вверх звучат как "верно", две низкие как "ошибка"
            successClip = ToneGenerator.Create("Success", new[] { 660f, 880f }, 0.12f);
            errorClip = ToneGenerator.Create("Error", new[] { 220f, 165f }, 0.2f);
            finishClip = ToneGenerator.Create("Finish", new[] { 523f, 659f, 784f, 1047f }, 0.15f);
        }

        private void OnEnable()
        {
            EventBus<StepFinishedEvent>.Subscribe(OnStepFinished);
            EventBus<GroupFinishedEvent>.Subscribe(OnGroupFinished);
            EventBus<ScenarioFinishedEvent>.Subscribe(OnScenarioFinished);
        }

        private void OnDisable()
        {
            EventBus<StepFinishedEvent>.Unsubscribe(OnStepFinished);
            EventBus<GroupFinishedEvent>.Unsubscribe(OnGroupFinished);
            EventBus<ScenarioFinishedEvent>.Unsubscribe(OnScenarioFinished);
        }

        private void OnStepFinished(StepFinishedEvent stepFinished)
        {
            if (stepFinished.Step.Status == StepStatus.Success)
                audioSource.PlayOneShot(successClip);
            else
                audioSource.PlayOneShot(errorClip);
        }

        private void OnGroupFinished(GroupFinishedEvent groupFinished)
        {
            // нарушение порядка тоже ошибка
            if (groupFinished.SequenceBroken)
                audioSource.PlayOneShot(errorClip);
        }

        private void OnScenarioFinished(ScenarioFinishedEvent scenarioFinished)
        {
            // чуть позже, чтобы не наложилось на звук последнего шага
            audioSource.clip = finishClip;
            audioSource.PlayDelayed(0.5f);
        }
    }
}
