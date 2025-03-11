using Microsoft.EntityFrameworkCore;
using RotaDeViagemApi.Data;
using RotaDeViagemApi.DTOs;
using RotaDeViagemApi.Mappers;
using RotaDeViagemApi.Models;

namespace RotaDeViagemApi.Services
{
    public class RotaService
    {
        private readonly RotaDbContext _context;

        public RotaService(RotaDbContext context)
        {
            _context = context;
        }

        public async Task<List<RotaResponseDto>> GetAllRotasAsync()
        {
            var rotas = await _context.Rotas.ToListAsync();
            return rotas.ToDtoList();
        }

        public async Task<RotaResponseDto> GetRotaByIdAsync(int id)
        {
            var rota = await _context.Rotas.FindAsync(id);
            return rota?.ToDto();
        }

        public async Task<RotaResponseDto> CreateRotaAsync(RotaRequestDto dto)
        {
            var rota = dto.ToModel();
            _context.Rotas.Add(rota);
            await _context.SaveChangesAsync();
            return rota.ToDto();
        }

        public async Task<RotaResponseDto> UpdateRotaAsync(int id, RotaRequestDto dto)
        {
            var rota = await _context.Rotas.FindAsync(id);
            if (rota == null) return null;

            rota.Origem = dto.Origem;
            rota.Destino = dto.Destino;
            rota.Valor = dto.Valor;

            _context.Entry(rota).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return rota.ToDto();
        }

        public async Task<bool> DeleteRotaAsync(int id)
        {
            var rota = await _context.Rotas.FindAsync(id);
            if (rota == null) return false;

            _context.Rotas.Remove(rota);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ConsultaRotaResponseDto> ConsultarMelhorRotaAsync(ConsultaRotaRequestDto request)
        {
            var rotas = await _context.Rotas.ToListAsync();
            var resultado = EncontrarMelhorRota(rotas, request.Origem, request.Destino);

            if (resultado == null)
            {
                return null;
            }

            return new ConsultaRotaResponseDto
            {
                Trajeto = resultado.Value.Caminho,
                CustoTotal = resultado.Value.Custo
            };
        }


        private (List<string> Caminho, decimal Custo)? EncontrarMelhorRota(List<Rota> rotas, string origem, string destino)
        {
            var grafo = ConstruirGrafo(rotas);
            var visitados = new HashSet<string>();
            var fila = new PriorityQueue<(string No, List<string> Caminho, decimal Custo), decimal>();

            fila.Enqueue((origem, new List<string> { origem }, 0), 0);

            while (fila.Count > 0)
            {
                var (noAtual, caminhoAtual, custoAtual) = fila.Dequeue();

                if (noAtual == destino)
                {
                    return (caminhoAtual, custoAtual);
                }

                if (!visitados.Add(noAtual))
                {
                    continue;
                }

                if (grafo.TryGetValue(noAtual, out var vizinhos))
                {
                    foreach (var (vizinho, custo) in vizinhos)
                    {
                        var novoCaminho = new List<string>(caminhoAtual) { vizinho };
                        var novoCusto = custoAtual + custo;
                        fila.Enqueue((vizinho, novoCaminho, novoCusto), novoCusto);
                    }
                }
            }

            return null;
        }

        private Dictionary<string, List<(string Destino, decimal Custo)>> ConstruirGrafo(List<Rota> rotas)
        {
            var grafo = new Dictionary<string, List<(string, decimal)>>();

            foreach (var rota in rotas)
            {
                if (!grafo.ContainsKey(rota.Origem))
                {
                    grafo[rota.Origem] = new List<(string, decimal)>();
                }
                grafo[rota.Origem].Add((rota.Destino, rota.Valor));
            }

            return grafo;
        }
    }
}
