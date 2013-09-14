using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Contracts;
using Domain.Contracts;
using Domain.DataContracts;

namespace Application.Impl
{
    public class RatingReviewsManagementService : IRatingReviewsManagementService
    {
        #region --- private members ---

        private IMonumentRatingRepository MonumentRatingRepository;
        private IMonumentReviewsRepository MonumentReviewsRepository;
        private ITouristRepository TouristRepository;

        #endregion
        
        #region --- constructors ---
        public RatingReviewsManagementService(IMonumentRatingRepository monumentRatingRepository, IMonumentReviewsRepository monumentReviewsRepository, ITouristRepository touristRepository)
        {
            MonumentRatingRepository = monumentRatingRepository;
            MonumentReviewsRepository = monumentReviewsRepository;
            TouristRepository = touristRepository;
        }

        #endregion


        public bool PostRate(string userName, string hotSpotID, double rate, out string errorMessage)
        {
            var tourist = TouristRepository.GetFilteredElements(tourist1 => tourist1.UserName == userName).FirstOrDefault();
            if (tourist != null)
            {
                if (!MonumentRatingRepository.GetFilteredElements(ratings => ratings.Tourist_ID == tourist.ID && ratings.hotSpotID == hotSpotID).Any())
                {
                    try
                    {
                        MonumentRatingRepository.Add(new Monument_Ratings()
                                                         {
                                                             Tourist_ID = tourist.ID,
                                                             hotSpotID = hotSpotID,
                                                             Rate = rate
                                                         });
                        MonumentRatingRepository.UnitOfWork.Commit();
                        errorMessage = string.Empty;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        return false;
                    }
                    //catch (InvalidOperationException ioex)
                    //{
                    //    errorMessage = ioex.Message;
                    //    return false;
                    //}
                }
                else
                {
                    errorMessage = "User has already rated this monument, User Name: " + userName;
                    return false;
                }
            }
            else
            {
                errorMessage = "No User Is Existed For User Name: " + userName;
                return false;
            }
        }

        public bool PostReview(string userName, string hotSpotID, string userReview , out string errorMessage)
        {
            var tourist = TouristRepository.GetFilteredElements(tourist1 => tourist1.UserName == userName).FirstOrDefault();
            if (tourist != null)
            {
                try
                {
                    MonumentReviewsRepository.Add(new Monuments_Reviews()
                    {
                        Tourist_ID = tourist.ID,
                        hotSpotID = hotSpotID,
                        Review = userReview,
                    });
                    MonumentReviewsRepository.UnitOfWork.Commit();
                    errorMessage = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                errorMessage = "No User Is Existed For User Name: " + userName;
                return false;
            }
        }
    }
}
