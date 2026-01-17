using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBackend.Services.IAServices
{
        public interface IAService
    {
        Task<string> GetAiResponseAsync(string prompt);
    }
}
