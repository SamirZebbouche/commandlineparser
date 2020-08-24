using System.Collections.Generic;
using System.Linq;
using MGR.CommandLineParser.Extensibility.Command;
using Microsoft.EntityFrameworkCore;

namespace MGR.CommandLineParser.Command.OracleProcedure
{
    internal class CommandTypeProvider : ICommandTypeProvider
    {
        private readonly OracleSystemDataContext _dbContext;

        public CommandTypeProvider(OracleSystemDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ICommandType> GetAllCommandTypes()
        {
            var procedures = _dbContext.Procedures.Include(procedure => procedure.Parameters).ToList();
            return procedures.Select(MapProcedureToCommandType);
        }
        public ICommandType GetCommandType(string commandName)
        {
            var procedure = _dbContext.Procedures.Include(procedure => procedure.Parameters).Where(procedure => procedure.Name == commandName).SingleOrDefault();
            if (procedure != null)
            {
                return MapProcedureToCommandType(procedure);
            }
            return null;
        }

        private static CommandType MapProcedureToCommandType(Procedure procedure) => new CommandType(new CommandMetadata(procedure.Name),
                procedure.Parameters.Where(parameter => parameter.Direction.HasFlag(Direction.In)).Select(parameter => new CommandOptionMetadata(
                    new OptionDisplayInfo(parameter.Name),
                    !parameter.HasDefaultValue,
                    parameter.DefaultValue)),
                procedure.Parameters.Where(parameter => parameter.Direction.HasFlag(Direction.Out));
    }
}
