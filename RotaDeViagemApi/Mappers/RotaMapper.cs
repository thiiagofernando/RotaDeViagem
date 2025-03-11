using RotaDeViagemApi.DTOs;
using RotaDeViagemApi.Models;

namespace RotaDeViagemApi.Mappers
{
    public static class RotaMapper
    {
        public static Rota ToModel(this RotaRequestDto dto)
        {
            return new Rota
            {
                Origem = dto.Origem,
                Destino = dto.Destino,
                Valor = dto.Valor
            };
        }

        public static RotaResponseDto ToDto(this Rota model)
        {
            return new RotaResponseDto
            {
                Id = model.Id,
                Origem = model.Origem,
                Destino = model.Destino,
                Valor = model.Valor
            };
        }

        public static List<RotaResponseDto> ToDtoList(this IEnumerable<Rota> models)
        {
            return models.Select(x => x.ToDto()).ToList();
        }
    }

}
