using UnityEngine;

namespace VRTraining.Audio
{
    // готовые звуки брать нельзя, поэтому сигналы генерю кодом
    // сигнал это просто несколько нот подряд
    public static class ToneGenerator
    {
        private const int SampleRate = 44100;

        public static AudioClip Create(string clipName, float[] frequencies, float noteDuration, float volume = 0.4f)
        {
            int samplesPerNote = Mathf.RoundToInt(SampleRate * noteDuration);
            float[] samples = new float[samplesPerNote * frequencies.Length];

            for (int note = 0; note < frequencies.Length; note++)
            {
                for (int i = 0; i < samplesPerNote; i++)
                {
                    float time = (float)i / SampleRate;
                    float wave = Mathf.Sin(2f * Mathf.PI * frequencies[note] * time);

                    // плавно гасим ноту к концу, иначе слышны щелчки
                    float fade = 1f - (float)i / samplesPerNote;

                    samples[note * samplesPerNote + i] = wave * fade * volume;
                }
            }

            AudioClip clip = AudioClip.Create(clipName, samples.Length, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}
