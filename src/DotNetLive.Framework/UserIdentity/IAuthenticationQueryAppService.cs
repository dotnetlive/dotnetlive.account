using AutoMapper;
using DotNetLive.Framework.Data.Repositories;
using DotNetLive.Framework.Entities;
using System;

namespace DotNetLive.Framework.UserIdentity
{
    public interface IAuthenticationQueryAppService
    {
        Account GetAccountByNormalizedEmail(string normalizedEmail);
        Account GetAccountByUserName(string normalizedUserName);
        Account GetAccount(Guid userSysId);
    }

    public class AuthenticationQueryAppService : IAuthenticationQueryAppService
    {
        private IQueryRepository _articleQueryRepository;
        private IMapper _mapper;

        public AuthenticationQueryAppService(IQueryRepository articleQueryRepository, IMapper mapper)
        {
            this._articleQueryRepository = articleQueryRepository;
            this._mapper = mapper;
        }

        public Account GetAccountByNormalizedEmail(string normalizedEmail)
        {
            var selectSql = $"select t.* from account t where upper(email)=@normalizedEmail";
            var account = _articleQueryRepository.Get<Account>(selectSql, new { normalizedEmail });
            return account;
        }

        public Account GetAccountByUserName(string normalizedUserName)
        {
            var selectSql = $"select t.* from account t where upper(username)=@normalizedUserName";
            var account = _articleQueryRepository.Get<Account>(selectSql, new { normalizedUserName });
            return account;
        }

        public Account GetAccount(Guid userSysId)
        {
            var selectSql = $"select t.* from account t where sysid=@userSysId";
            var account = _articleQueryRepository.Get<Account>(selectSql, new { userSysId });
            return account;
        }
    }
}