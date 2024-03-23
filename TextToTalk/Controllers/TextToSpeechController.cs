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
