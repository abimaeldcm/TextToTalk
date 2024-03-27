# Tutorial: Síntese de Fala (Speech) com Azure e ASP.NET Core

## O que é o Síntese de Fala (Speech)
A Síntese de Fala faz parte do Speech Services da Azure que é um conjunto de serviços de inteligência artificial da Microsoft projetado para fornecer recursos avançados de fala e linguagem natural em aplicativos. Ele é uma tecnologia que converte texto em fala humana artificial. Ela permite que os computadores "falem", transformando palavras escritas em uma voz audível e natural. A síntese de fala é comumente usada em aplicativos de assistentes virtuais, sistemas de navegação por voz, leitores de tela para pessoas com deficiência visual, entre outros.

## Criando o Serviço de Síntese de Fala na Azure
1. Acesse o portal Azure e clique em Criar um novo recurso.
   ![image](https://github.com/abimaeldcm/TextToTalk/assets/119269116/3942bd69-b89b-4f8d-ad65-f930cda230fa)

3. Procure por “Speech” e clique no item correspondente.
   ![image](https://github.com/abimaeldcm/TextToTalk/assets/119269116/63b82f6e-25d2-4f0e-b220-ba60aed06792)

5. Preencha as informações necessárias, como assinatura, grupo de recursos, região do grupo de recursos (se estiver criando um novo), região, nome e tipo de preço.
   ![image](https://github.com/abimaeldcm/TextToTalk/assets/119269116/4390655e-5691-4338-8a73-b17dbb125a04)

7. Clique em “Examinar + criar” e, em seguida, em “Criar”.

8. Na tela da sua aplicação, encontre as duas chaves de acesso e a localização e região. Estas informações serão importantes para configurar a nossa API.
   ![image](https://github.com/abimaeldcm/TextToTalk/assets/119269116/a96b8044-bb3d-44b9-8c88-1a580e12934a)


## Criando o projeto Demo
1. Com um projeto ASP.NET Core Web API criado, instale o pacote `Microsoft.CognitiveServices.Speech`.
2. Crie uma Controller chamada `TextToSpeechController` que será responsável por receber o texto a ser convertido em áudio.

```csharp
using Microsoft.AspNetCore.Mvc;

namespace TextToTalk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextToSpeechController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string text)
        {
            try
            {
                var audioData = await TextToSpeechService.TextToVoiceService(text);
                return File(audioData, "audio/wav");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
```

3. Crie um arquivo `TextToSpeechService` onde implementará o método com a lógica de conversão.

```csharp
using Microsoft.CognitiveServices.Speech;

public static class TextToSpeechService
{
    static string speechKey = "CHAVE 1 ou CHAVE 2";
    static string speechRegion = "Localização/região";

    public static async Task<byte[]> TextToVoiceService(string text)
    {
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

        speechConfig.SpeechSynthesisVoiceName = "pt-BR-BrendaNeural";

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
```

4. Para utilizar outras vozes, basta substituir `SpeechSynthesisVoiceName` por uma das seguintes vozes: Francisca, Antonio, Brenda, Donato, Elza, Fabo, Giovanna, Humberto, Julio, Leika, Laticia, Manuela, Nicolau, Valerio, Yara, Thalita. Exemplo: `"pt-BR-ManuelaNeural"`.

## Conclusão
Neste tutorial básico, aprendemos como implementar a Síntese de Fala (Speech) utilizando os Speech Services da Azure e o ASP.NET Core. Configuramos o serviço na Azure, criamos uma API para converter texto em áudio e utilizamos a biblioteca `CognitiveServices.Speech` para realizar a síntese de fala. Para explorar implementações mais avançadas, consulte a documentação oficial para descobrir funcionalidades adicionais e personalizações possíveis.
