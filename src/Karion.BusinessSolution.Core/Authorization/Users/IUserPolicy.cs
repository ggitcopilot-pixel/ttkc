﻿using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Karion.BusinessSolution.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
