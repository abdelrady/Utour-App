using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts
{
    public interface IRatingReviewsManagementService
    {
        bool PostRate(string userName , string hotSpotID , double rate, out string errorMessage);
        bool PostReview(string userName , string hotSpotID , string userReview, out string errorMessage);
    }
}
