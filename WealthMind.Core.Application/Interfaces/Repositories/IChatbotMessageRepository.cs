using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IChatbotMessageRepository : IGenericRepository<ChatbotMessage>
    {
        Task<List<ChatbotMessage>> GetBySessionIdAsync(string sessionId);
    }
}
