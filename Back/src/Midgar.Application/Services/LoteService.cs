using AutoMapper;
using Midgar.Application.DTOs;
using Midgar.Application.Interfaces;
using Midgar.Domain.Entities;
using Midgar.Persistence.Interfaces;

namespace Midgar.Application.Services
{
    public class LoteService : ILoteService
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IMapper _mapper;
        public LoteService(IGeneralRepository generalRepository, ILoteRepository loteRepository, IMapper mapper)
        {
            _generalRepository = generalRepository;
            _loteRepository = loteRepository;
            _mapper = mapper;
        }

        private async Task AddLote(int eventId, LoteDTO model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);

                lote.EventId = eventId;

                _generalRepository.Add(lote);

                await _generalRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO[]> SaveLotes(int eventId, LoteDTO[] lotesDTO)
        {
            try
            {
                var lotesByEventId = await _loteRepository.GetLotesByEventIdAsync(eventId);

                if (lotesByEventId == null)
                    return null;

                foreach (var loteDTO in lotesDTO)
                {
                    if (loteDTO.Id == 0)
                    {
                        await AddLote(eventId, loteDTO);
                    }
                    else
                    {
                        var lote = lotesByEventId.FirstOrDefault(lote => lote.Id == loteDTO.Id);

                        loteDTO.EventId = eventId;

                        _mapper.Map(loteDTO, lote);

                        _generalRepository.Update(lote);

                        await _generalRepository.SaveChangesAsync();
                    }
                }

                var loteReturn = await _loteRepository.GetLotesByEventIdAsync(eventId);

                return _mapper.Map<LoteDTO[]>(loteReturn);
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventId, int loteId)
        {
            try
            {
                var deleteLote = await _loteRepository.GetLoteByIdsAsync(eventId, loteId) ?? throw new Exception("Couldn't find the lote to delete.");
                
                _generalRepository.Delete(deleteLote);

                return await _generalRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO[]> GetLotesByEventIdAsync(int eventId)
        {
            try
            {
               var lotesByEventId = await _loteRepository.GetLotesByEventIdAsync(eventId);

                if (lotesByEventId == null)
                    return null;

                var result = _mapper.Map<LoteDTO[]>(lotesByEventId);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }

        public async Task<LoteDTO> GetLoteByIdsAsync(int eventId, int loteId)
        {
            try
            {
                var loteByIds = await _loteRepository.GetLoteByIdsAsync(eventId, loteId);

                if (loteByIds == null)
                    return null;
                
                var result = _mapper.Map<LoteDTO>(loteByIds);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }
    }
}