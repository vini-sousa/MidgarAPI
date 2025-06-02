using AutoMapper;
using Midgar.Application.DTOs;
using Midgar.Application.Interfaces;
using Midgar.Domain.Entities;
using Midgar.Persistence.Interfaces;

namespace Midgar.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventService(IGeneralRepository generalRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _generalRepository = generalRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDTO> AddEvents(EventDTO model)
        {
            try
            {
                var eventMap = _mapper.Map<Event>(model);

                _generalRepository.Add(eventMap);

                if (await _generalRepository.SaveChangesAsync())
                {
                    var eventReturn = await _eventRepository.GetEventByIdAsync(eventMap.Id, false);

                    return _mapper.Map<EventDTO>(eventReturn);
                }

                return null;
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDTO> UpdateEvents(int id, EventDTO model)
        {
            try
            {
                var updateEvent = await _eventRepository.GetEventByIdAsync(id, false);

                if (updateEvent == null)
                    return null;

                model.Id = updateEvent.Id;

                _mapper.Map(model, updateEvent);

                _generalRepository.Update(updateEvent);

                if (await _generalRepository.SaveChangesAsync())
                {
                    var eventReturn = await _eventRepository.GetEventByIdAsync(updateEvent.Id, false);

                    return _mapper.Map<EventDTO>(eventReturn);
                }

                return null;
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvents(int eventId)
        {
            try
            {
                var deleteEvent = await _eventRepository.GetEventByIdAsync(eventId, false) ?? throw new Exception("Delete event not found.");
                
                _generalRepository.Delete(deleteEvent);

                return await _generalRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDTO[]> GetAllEventsAsync(bool includedSpeakers = false)
        {
            try
            {
               var allEvent = await _eventRepository.GetAllEventsAsync(includedSpeakers);

                if (allEvent == null)
                    return null;

                var result = _mapper.Map<EventDTO[]>(allEvent);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }

        public async Task<EventDTO[]> GetAllEventsByThemeAsync(string theme, bool includedSpeakers = false)
        {
            try
            {
                var eventsByTheme = await _eventRepository.GetAllEventsByThemeAsync(theme, includedSpeakers);

                if (eventsByTheme == null)
                    return null;

                var result = _mapper.Map<EventDTO[]>(eventsByTheme);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }

        public async Task<EventDTO> GetEventByIdAsync(int eventId, bool includedSpeakers = false)
        {
            try
            {
                var eventById = await _eventRepository.GetEventByIdAsync(eventId, includedSpeakers);

                if (eventById == null)
                    return null;
                
                var result = _mapper.Map<EventDTO>(eventById);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }
    }
}