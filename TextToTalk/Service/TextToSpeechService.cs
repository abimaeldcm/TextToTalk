using Microsoft.CognitiveServices.Speech;

public static class TextToSpeechService
{
    // Este exemplo requer variáveis de ambiente chamadas "SPEECH_KEY" e "SPEECH_REGION"
    static string speechKey = "CHAVE";
    static string speechRegion = "eastus";

    public static async Task<byte[]> TextToVoiceService(string text)
    {
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

        // O idioma da voz que fala.
        speechConfig.SpeechSynthesisVoiceName = "pt-BR-BrendaNeural";

        // use the default speaker as audio output.
        using (var synthesizer = new SpeechSynthesizer(speechConfig))
        {
            var result = await synthesizer.SpeakTextAsync(text);

            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                return result.AudioData;
            }
            else
            {
                throw new Exception($"Falha ao sintetizar áudio: {result.Reason}");
            }
        }        
    }
}
