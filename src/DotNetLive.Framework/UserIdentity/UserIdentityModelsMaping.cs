using AutoMapper;
using DotNetLive.Framework.Entities;
using DotNetLive.Framework.Models;
using DotNetLive.Framework.UserIdentity.Models;
using System;

namespace DotNetLive.Framework.UserIdentity
{
    public class UserIdentityModelsMaping : Profile
    {
        public UserIdentityModelsMaping()
        {
            CreateMap<Account, ApplicationUser>()
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => s.CreatedOn == DateTime.MinValue ? null : new Occurrence(s.CreatedOn)))
                .ForMember(d => d.DeletedOn, opt => opt.MapFrom(s => s.DeletedOn == DateTime.MinValue ? null : new Occurrence(s.DeletedOn)))
                .ForMember(d => d.LockoutEndDate, opt => opt.MapFrom(s => s.LockoutEndDate == DateTime.MinValue ? null : new FutureOccurrence(s.LockoutEndDate)))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.PhoneNumber) ? null : new UserPhoneNumber(s.PhoneNumber)))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Email) ? null : new UserEmail(s.Email)))
                ;
            CreateMap<ApplicationUser, Account>()
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => s.CreatedOn == null ? DateTime.MinValue : s.CreatedOn.Instant))
                .ForMember(d => d.DeletedOn, opt => opt.MapFrom(s => s.DeletedOn == null ? DateTime.MinValue : s.DeletedOn.Instant))
                .ForMember(d => d.LockoutEndDate, opt => opt.MapFrom(s => s.LockoutEndDate == null ? DateTime.MinValue : s.LockoutEndDate.Instant))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber == null ? null : s.PhoneNumber.Value))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email == null ? null : s.Email.Value))
                ;
        }
    }
}
