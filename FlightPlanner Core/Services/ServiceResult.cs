using System.Collections.Generic;
using FlightPlanner_Core.Interfaces;

namespace FlightPlanner_Core.Services
{
    public class ServiceResult
    {
        public bool Success { get; private set; }
        public IEntity Entity { get; private set; }
        public IList<string> Errors { get; private set; }
        public string FormattedErrors => string.Join(",", Errors);

        public ServiceResult(bool success)
        {
            Success = success;
            Errors = new List<string>();
        }

        public ServiceResult SetEntity(IEntity entity)
        {
            Entity = entity;
            return this;
        }

        public ServiceResult AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
    }
}