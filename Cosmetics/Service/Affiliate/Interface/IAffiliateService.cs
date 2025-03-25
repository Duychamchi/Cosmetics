﻿using Cosmetics.DTO.Affiliate;
using Cosmetics.Enum;

namespace Cosmetics.Service.Affiliate.Interface
{
    public interface IAffiliateService
    {
        Task<AffiliateProfileDto> RegisterAffiliateAsync(int userId, AffiliateRegistrationRequestDto request);
        Task<AffiliateLinkDto> GenerateAffiliateLinkAsync(int userId, Guid productId);
        Task<AffiliateStatsDto> GetAffiliateStatsAsync(int userId, DateTime startDate, DateTime endDate);
        Task<TransactionAffiliateDTO> RequestWithdrawalAsync(int userId, WithdrawalRequestDto request);
        Task<TransactionAffiliateDTO> UpdateWithdrawalStatusAsync(Guid transactionId, WithdrawalStatus status);

        Task TrackAffiliateClickAsync(string referralCode, int? userId); 

    }
}
