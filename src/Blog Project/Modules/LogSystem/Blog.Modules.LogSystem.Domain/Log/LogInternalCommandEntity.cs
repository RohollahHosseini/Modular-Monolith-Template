using Blog.BuildingBlocks.Model;
using System;

namespace Blog.Modules.LogSystem.Domain.Log
{
    public sealed class LogInternalCommandEntity: InternalCommandBaseEntity
    {


        public static LogInternalCommandEntity CreateInternamCommand(Guid Id,DateTime EnqueueDate, string Type, string Data, DateTime? ProcessedDate,string? Error=default)
        {
            LogInternalCommandEntity logInternalCommandEntity = new()
            {
                Id = Id,
                EnqueueDate = EnqueueDate,
                Type = Type,
                Data = Data,
                ProcessedDate = ProcessedDate,
                Error = Error
            };
            

            return logInternalCommandEntity;
        }

    }
}
