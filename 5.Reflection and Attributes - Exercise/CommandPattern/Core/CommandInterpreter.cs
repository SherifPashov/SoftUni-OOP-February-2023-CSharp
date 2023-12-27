using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] info = args
                .Split(' ',StringSplitOptions.RemoveEmptyEntries);

            string commandName = info[0];
            string[] commandArgs = info.Skip(1).ToArray();

            Type commandType = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Command not found");
            }

            ICommand commandInstance = Activator.CreateInstance(commandType) as ICommand;
            return commandInstance.Execute(commandArgs);
           
        }
    }
}
