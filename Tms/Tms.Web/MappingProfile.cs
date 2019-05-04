using AutoMapper;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Extensions;
using Tms.ApplicationCore.Models;
using Tms.Web.Email.Model;
using Tms.Web.Helpers;
using Tms.Web.ViewModels;

namespace Tms.Web
{
	public class MappingProfile: Profile
    {
        public MappingProfile()
        {
			CreateMap<SecurityRoleDetail, SecurityRoleViewModel>()
				.ForMember(dest => dest.Actions, opt => opt.MapFrom(src => SecurityHelper.GetPermFlags(src.PermFlag)))
				;

			CreateMap<SecurityRoleDetail, SecurityUserRoleViewModel>()
				.ForMember(dest => dest.Actions, opt => opt.MapFrom(src => SecurityHelper.GetPermFlags(src.PermFlag)))
				;

			CreateMap<SecurityUserRoleViewModel, SecurityRole>()
				.ForMember(dest => dest.PermFlag, opt => opt.MapFrom(src => SecurityHelper.GetPerm(src.Actions)))
				.ForMember(dest => dest.Practice, opt => opt.MapFrom(src => "Audit"))
				;

			CreateMap<SecurityEmployeeDelegationViewModel, SecurityEmployeeDelegation>()
				;

			CreateMap<EmailQueue, EmailLog>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
			;

			CreateMap<BaseEmailViewModel, EmailQueue>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.BCCList, opt => opt.MapFrom(src => src.BCCList.ToDelimited(";")))
			.ForMember(dest => dest.CCList, opt => opt.MapFrom(src => src.CCList.ToDelimited(";")))
			.ForMember(dest => dest.ToList, opt => opt.MapFrom(src => src.ToList.ToDelimited(";")))
			.ForMember(dest => dest.BatchId, opt => opt.Ignore())
			.ForMember(dest => dest.Body, opt => opt.Ignore())
			.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
			.ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
			.ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
			.ForMember(dest => dest.LastUpdatedDateTime, opt => opt.Ignore())
			;
		}
    }
}
