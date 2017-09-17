using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.Mappers.Implementations;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.PaymentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class MappersExtension
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IMapper<ProjectItemViewModel, Project>, ProjectItemViewModelToProjectMapper>();
            services.AddScoped<IMapper<ProjectFormViewModel, Project>, ProjectFormViewModelToProjectMapper>();
            services.AddScoped<IMapper<FinancialPurposeViewModel, FinancialPurpose>, FinancialPurposeViewModelToFinancialPurposeMapper>();
            services.AddScoped<IMapper<NewsFormViewModel, News>, NewsFormViewModelToNewsMapper>();
            services.AddScoped<IMapper<UserListItemViewModel, UserInfo>, UserListItemViewModelToUserInfoMapper>();
            services.AddScoped<IMapper<UserConfirmationViewModel, UserInfo>, UserConfirmationViewModelToUserInfoMapper>();
            services.AddScoped<IMapper<ProjectViewModel, Project>, ProjectViewModelToProjectMapper>();
            services.AddScoped<IMapper<CommentViewModel, Comment>, CommentViewModelToCommentMapper>();
            services.AddScoped<IMapper<NewsViewModel, News>, NewsViewModelToNewsMapper>();
            services.AddScoped<IMapper<ProjectEditorFormViewModel, Project>, ProjectEditorFormViewModelToProjectMapper>();
            services.AddScoped<IMapper<CommentFormViewModel, Comment>, CommentFormViewModelToCommentMapper>();
            services.AddScoped<IMapper<RatingViewModel, Rating>, RatingViewModelToRatingMapper>();
            services.AddScoped<IMapper<UserSmallViewModel, UserInfo>, UserSmallViewModelToUserInfoMapper>();
            services.AddScoped<IMapper<PaymentFormViewModel, Payment>, PaymentFormViewModelToPaymentMapper>();
        }
    }
}
