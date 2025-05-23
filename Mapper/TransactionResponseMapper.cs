using AutoMapper;

namespace BasicPaymentGateway.Mapper
{
    public class TransactionResponseMapper  : Profile
    {
        public TransactionResponseMapper()
        {
            CreateMap<BasicPaymentGateway.Entity.Transaction, BasicPaymentGateway.Common.ResponseModel.FetchPaymentResponse>()
       .ForMember(dest => dest.payment, opt => opt.MapFrom(src => src)) 
       .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status))
       .ForMember(dest => dest.message, opt => opt.MapFrom(src => "Transaction fetched successfully")); 
            ;

            // And you'll need a mapping for the nested Payment DTO
            CreateMap<BasicPaymentGateway.Entity.Transaction, BasicPaymentGateway.Common.ResponseModel.Payment>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.ToString())) 
                .ForMember(dest => dest.customer_name, opt => opt.MapFrom(src => src.customer_Name)) 
                .ForMember(dest => dest.customer_email, opt => opt.MapFrom(src => src.customer_Email)) 
                .ForMember(dest => dest.amount, opt => opt.MapFrom(src => src.amount / 100.0)) 
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status));
        }
    }
}
