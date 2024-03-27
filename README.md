Tutorial: Síntese de Fala (Speech) com Azure e ASP.NET Core
O que é o Síntese de Fala (Speech)
A Síntese de Fala faz parte do Speech Services da Azure que é um conjunto de serviços de inteligência artificial da Microsoft projetado para fornecer recursos avançados de fala e linguagem natural em aplicativos.  Ele é uma tecnologia que converte texto em fala humana artificial. Ela permite que os computadores "falem", transformando palavras escritas em uma voz audível e natural. A síntese de fala é comumente usada em aplicativos de assistentes virtuais, sistemas de navegação por voz, leitores de tela para pessoas com deficiência visual, entre outros.
Criando o Serviço de Síntese de Fala na Azure
Acesse o portal Azure e clicar em Criar um novo recurso. Procure por “Speech”
 
Clique no item e, em seguida, aparecerá uma tela semelhante a essa.
 
Em seguida, será necessário preencher algumas informações, como:
•	Assinatura
•	Grupo de Recursos
•	Região do grupo de recursos (se estiver criando um Grupo de Recursos novo)
•	Região
•	Nome
•	Tipo de preço
Na imagem abaixo, mostro as configurações que utilizei.
 
Após o preenchimento, clicar em “Examinar + criar”, e então em “Criar”.
Em seguida aparecerá a tela da sua aplicação onde você encontra as duas chaves de acesso e a localização e região. Estas duas informações serão importantes quando precisarmos configurar a nossa API.
   

Criando o projeto Demo
Agora que configuramos a nossa aplicação, vamos para a nossa API.
Com um projeto ASP.NET Core Web API criado, vamos instalar o pacote 

“dotnet add package Microsoft.CognitiveServices.Speech” 

e criar uma Controller chamada “TextToSpeechController” que será responsável por receber o texto a ser convertido em áudio.

O papel dessa controller será simples, vamos enviar o nosso “text” para o serviço de conversão e receber o arquivo de áudio que será retornado.

using Microsoft.AspNetCore.Mvc;

namespace TextToTalk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextToSpeechController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string text)        {
            try
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
Agora vamos para o centro da nossa aplicação. 
Vamos criar um arquivo “TextToSpeechService” onde vamos implementar o nosso método com a lógica de conversão.

using Microsoft.CognitiveServices.Speech;

public static class TextToSpeechService
{
    static string speechKey = " CHAVE 1 ou CHAVE 2";
    static string speechRegion = " Localização/região";

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
 
No código acima vamos criar a variável “speechKey” onde vamos armazenar a nossa chave 1 ou chave 2 e “speechRegion” onde vamos armazena a região do serviço de Síntese de Fala da Azure.
Vamos utilizar as informações repassadas nas variáveis “speechKey”, “speechRegion” para iniciar a criação de uma configuração na variável “speechConfig”.
Agora podemos escolher o idioma e tipo de voz que desejamos. Para este tutorial nos escolhemos o idioma Português (Brasil) e a voz da “BrendaNeural”. 
Caso queira utilizar outras vozes até o momento existem 16 vozes: Francisca, Antonio, Brenda, Donato, Elza, Fabo, Giovanna, Humberto, Julio, Leika, Laticia, Manuela, Nicolau, Valerio, Yara, Thalita. Para utilizar basta colocar “pt-BR-NomeDaVozNeural”. 
Ex: "pt-BR- ManuelaNeural".
Agora vamos criar o objeto “SpeechSynthesizer” para realizar a síntese de fala, utilizando a configuração especificada e chamar o método assíncrono “SpeakTextAsync” para realizar a síntese de áudio a partir do texto fornecido.
Se a síntese estiver completa vamos retornar o áudio para a nossa controller.

Conclusão

Neste tutorial básico, nós podemos aprender como implementar a Síntese de Fala (Speech) utilizando os Speech Services da Azure e o ASP.NET Core. Seguindo os passos apresentados, podemos configurar o serviço na Azure, criar uma API para converter texto em áudio e utilizar a biblioteca “CognitiveServices.Speech” para realizar a síntese de fala. Para aqueles que desejam explorar implementações mais avançadas, recomendamos consultar a documentação oficial para descobrir funcionalidades adicionais e personalizações possíveis.
