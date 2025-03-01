using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IChatbotSessionRepository : IGenericRepository<ChatbotSession>
    {
        //Task<ChatbotSession> GetActiveSessionByUserIdAsync(string userId);
        Task<List<ChatbotSession>> GetAllActiveSessionsByUserIdAsync(string userId);
    }
}
