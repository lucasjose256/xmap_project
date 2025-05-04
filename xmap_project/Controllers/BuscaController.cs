using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Security.Cryptography.Xml;
using xmap_project.Data;
using xmap_project.Modules;
using static System.Net.Mime.MediaTypeNames;

namespace xmap_project.Controllers
{
  //  Indexa os textos dos metadados;

//Transforma os textos em vetores numéricos com TF-IDF;

//Calcula a similaridade com o termo de busca;

 //   Retorna os resultados mais semelhantes.
    


        [ApiController]
    [Route("[controller]")]
    public class BuscaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BuscaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("inteligente")]
        public IActionResult Buscar([FromQuery] string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return BadRequest("O termo de busca não pode ser vazio.");

            var palavras = termo.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var resultados = _context.metaDados
                .Include(m => m.atividade)
                    .ThenInclude(a => a.process)
                .AsEnumerable() 
                .Where(m => palavras.Any(p =>
                    (m.dados?.ToLower().Contains(p) ?? false) ||
                    (m.ator?.ToLower().Contains(p) ?? false) ||
                    (m.lgpd?.ToLower().Contains(p) ?? false) ||
                    (m.atividade?.nomeAtividade?.ToLower().Contains(p) ?? false) ||
                    (m.atividade?.process?.name?.ToLower().Contains(p) ?? false)
                ))
                .Select(m => new
                {
                    Processo = new
                    {
                        m.atividade.process.name,
                        m.atividade.process.description,
                        m.atividade.process.urlProcess
                    },
                    Atividade = m.atividade.nomeAtividade,
                    Metadado = new
                    {
                        m.dados,
                        m.ator,
                        m.lgpd
                    }
                })
                .ToList();

            if (!resultados.Any())
                return NotFound("Nenhum resultado encontrado.");

            return Ok(resultados);
        }
        [HttpGet("inteligente-ml")]
        public IActionResult BuscarComML([FromQuery] string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return BadRequest("O termo de busca não pode ser vazio.");

            var lista = _context.metaDados
                .Include(m => m.atividade)
                    .ThenInclude(a => a.process)
                .ToList();

            var corpus = lista.Select(m => new MetaDadoInput
            {
                TextoCompleto = $"{m.dados} {m.ator} {m.lgpd} {m.atividade?.nomeAtividade} {m.atividade?.process?.name}"
            }).ToList();

            var mlContext = new MLContext();
            var data = mlContext.Data.LoadFromEnumerable(corpus);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(MetaDadoInput.TextoCompleto));
            var model = pipeline.Fit(data);
            var transformedData = model.Transform(data);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<MetaDadoInput, MetaDadoTransformado>(model);

            var termoVetorizado = predictionEngine.Predict(new MetaDadoInput { TextoCompleto = termo });


            var resultados = new List<(float score, object resultado)>();

            for (int i = 0; i < lista.Count; i++)
            {
                var vetor = mlContext.Data.CreateEnumerable<MetaDadoTransformado>(transformedData, reuseRowObject: false).ElementAt(i);
                float score = CosineSimilarity(termoVetorizado.Features, vetor.Features);

                resultados.Add((score, new
                {
                    Processo = new
                    {
                        lista[i].atividade.process.name,
                        lista[i].atividade.process.description,
                        lista[i].atividade.process.urlProcess
                    },
                    Atividade = lista[i].atividade.nomeAtividade,
                    Metadado = new
                    {
                        lista[i].dados,
                        lista[i].ator,
                        lista[i].lgpd
                    }
                }));
            }

            var melhores = resultados
                .Where(r => r.score > 0.1f)
                .OrderByDescending(r => r.score)
                .Select(r => r.resultado)
                .ToList();

            if (!melhores.Any())
                return NotFound("Nenhum resultado encontrado.");

            return Ok(melhores);
        }

        private float CosineSimilarity(float[] v1, float[] v2)
        {
            float dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                normA += v1[i] * v1[i];
                normB += v2[i] * v2[i];
            }

            return (float)(dot / (Math.Sqrt(normA) * Math.Sqrt(normB) + 1e-6));
        }

    }



}
