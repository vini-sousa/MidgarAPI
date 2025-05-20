using AutoMapper;
using Midgar.Application.DTOs;
using Midgar.Application.Interfaces;
using Midgar.Domain.Entities;
using Midgar.Persistence.Interfaces;

namespace Midgar.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventPersist _eventPersist;
        private readonly IMapper _mapper;
        public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist, IMapper mapper)
        {
            _generalPersist = generalPersist;
            _eventPersist = eventPersist;
            _mapper = mapper;
        }

        public async Task<EventDTO> AddEvents(EventDTO model)
        {
            try
            {
                var eventMap = _mapper.Map<Event>(model);

                _generalPersist.Add(eventMap);

                if (await _generalPersist.SaveChangesAsync())
                {
                    var eventReturn = await _eventPersist.GetEventByIdAsync(eventMap.Id, false);

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
                var updateEvent = await _eventPersist.GetEventByIdAsync(id, false);

                if (updateEvent == null)
                    return null;

                model.Id = updateEvent.Id;

                _mapper.Map(model, updateEvent);

                _generalPersist.Update(updateEvent);

                if (await _generalPersist.SaveChangesAsync())
                {
                    var eventReturn = await _eventPersist.GetEventByIdAsync(updateEvent.Id, false);

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
                var deleteEvent = await _eventPersist.GetEventByIdAsync(eventId, false) ?? throw new Exception("Delete event not found.");
                
                _generalPersist.Delete(deleteEvent);

                return await _generalPersist.SaveChangesAsync();
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
               var allEvent = await _eventPersist.GetAllEventsAsync(includedSpeakers);

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
                var eventsByTheme = await _eventPersist.GetAllEventsByThemeAsync(theme, includedSpeakers);

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
                var eventById = await _eventPersist.GetEventByIdAsync(eventId, includedSpeakers);

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