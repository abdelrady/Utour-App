using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Application.Contracts;
using ITI.Common.HotSpotsInfo;
using Domain.DataContracts;
using Domain.Contracts;
using ITI.Common.HotSpotsInfo.LayerClasses;
using System.Configuration;

namespace Application.Impl
{
    public class HotSpotsManagementService : IHotSpotsManagementService
    {

        #region --- private members ---

        private ILayerHotSpotsRepository LayerHotSpotsRepository;
        private IMonumentPhotosRepository MonumentPhotosRepository;
        private IMonumentRatingRepository MonumentRatingRepository;
        private IMonumentReviewsRepository MonumentReviewsRepository;
        private IMonumentVideosRepository MonumentVideosRepository;

        #endregion

        #region --- constructors ---

        public HotSpotsManagementService(ILayerHotSpotsRepository layerHotSpotsRepository,
                                         IMonumentPhotosRepository monumentPhotosRepository,
                                         IMonumentRatingRepository monumentRatingRepository,
                                         IMonumentReviewsRepository monumentReviewsRepository,
                                         IMonumentVideosRepository monumentVideosRepository)
        {
            LayerHotSpotsRepository = layerHotSpotsRepository;
            MonumentPhotosRepository = monumentPhotosRepository;
            MonumentRatingRepository = monumentRatingRepository;
            MonumentReviewsRepository = monumentReviewsRepository;
            MonumentVideosRepository = monumentVideosRepository;
        }

        #endregion

        #region --- public Methods ---

        public LayerInfo RetrieveSurroundingHotSpots(LayerQueryParam layerQueryParams)
        {
            var hotSpotsInDB = GetHotSpotsInDb(layerQueryParams);
            //var hotSpotsInDB = GetHotSpotsInDbOptimized(layerQueryParams);
            List<HotSpots> hotSpotsList = MapDBHotSpotsToList(hotSpotsInDB);
            return new LayerInfo
                       {
                           //layer = "Utour_Layer"
                           layer = ConfigurationManager.AppSettings["LayerName"],
                           //it should be modified to reflect the real issues
                           //not only return 0 to the layer
                           errorCode = "0",
                           errorString = "Ok",
                           hotspots = hotSpotsList.ToArray()
                       };
        }

        #endregion

        #region --- Private Methods ---

        private List<HotSpots> MapDBHotSpotsToList(IEnumerable<layerhotspot> hotSpotsInDB)
        {
            var hotSpotsList = new List<HotSpots>();

            foreach (layerhotspot hotSpot in hotSpotsInDB)
            {
                layerhotspot spot = hotSpot;

                var hotSpotReviewsList = GetHotSpotReviewsList(spot, 0, 5);

                var hotSpotRate =
                    MonumentRatingRepository.GetFilteredElements(ratings => ratings.hotSpotID == spot.Id);

                var hotSpotPhotosList = GetHotSpotPhotosList(spot);

                var videosList = GetHotSpotVideosList(spot);

                //to be modified to add data to empty null properties
                hotSpotsList.Add(new HotSpots
                                     {
                                         id = hotSpot.Id,
                                         imageURL = hotSpot.imageurl != null?
                                             (hotSpot.imageurl.StartsWith("http")
                                                 ? hotSpot.imageurl
                                                 : ConfigurationManager.AppSettings["SiteUrl"] + "/" +
                                                   hotSpot.imageurl.Replace('\\', '/'))
                                                   :string.Empty,
                                         text =
                                             GetHotSpotText(hotSpotReviewsList, hotSpotPhotosList, videosList,
                                                                 hotSpot, hotSpotRate),
                                         anchor = GetHotSpotAnchor(hotSpot),
                                         biwStyle = hotSpot.biwStyle,
                                         showSmallBiw = hotSpot.showSmallBiw.HasValue && hotSpot.showSmallBiw.Value > 0,
                                         inFocus = false,
                                         showBiwOnClick =
                                             hotSpot.showBiwOnClick.HasValue && hotSpot.showBiwOnClick.Value > 0,
                                         transform = null,
                                         @object = null,
                                         animations = null,
                                         actions = GetDefaultHotSpotsActions(hotSpot)
                                     });
            }
            return hotSpotsList;
        }

        private Anchor GetHotSpotAnchor(layerhotspot hotSpot)
        {
            return new Anchor
                       {
                           geolocation = new GeoLocation
                                             {
                                                 alt = (hotSpot.alt.HasValue) ? hotSpot.alt.Value.ToString(CultureInfo.InvariantCulture) : "0",
                                                 lat = (hotSpot.lat.HasValue) ? hotSpot.lat.Value.ToString(CultureInfo.InvariantCulture) : "0.0",
                                                 lon = (hotSpot.lon.HasValue) ? hotSpot.lon.Value.ToString(CultureInfo.InvariantCulture) : "0.0",
                                             }
                       };
        }

        private Text GetHotSpotText(List<review> hotSpotReviewsList, List<Photo> hotSpotPhotosList,
                                    List<video> videosList, layerhotspot hotSpot,
                                    IEnumerable<Monument_Ratings> hotSpotRate)
        {
            if(hotSpotRate!=null)
            {
                var hotSpotRateLst = hotSpotRate.ToList();
                var hotsportRateAvg = hotSpotRateLst.Average(ratings => ratings.Rate);
                return new Text
                           {
                               title = hotSpot.title,
                               description = hotSpot.description,
                               footnote = hotSpot.footnote,
                               longdescription = hotSpot.Long_Description,
                               rate =
                                   hotSpotRateLst.Any()
                                       ? hotsportRateAvg!=null?hotsportRateAvg.Value.ToString(
                                           CultureInfo.InvariantCulture) : 0.ToString(CultureInfo.InvariantCulture)
                                       : 0.ToString(CultureInfo.InvariantCulture),
                               reviews = hotSpotReviewsList.ToArray(),
                               photos = hotSpotPhotosList.ToArray(),
                               videos = videosList.ToArray()
                           };
            }
            return new Text();
        }

        private Actions[] GetDefaultHotSpotsActions(layerhotspot hotSpot)
        {
            return new[]
                       {
                           new Actions
                               {
                                   label = "See More Photos",
                                   //uri = "local:Text:photos",
                                   uri = ConfigurationManager.AppSettings["SiteUrl"].ToString(CultureInfo.InvariantCulture) +
                                         "/ViewMonumentPhotos.aspx?hotSpotID=" + hotSpot.Id,
                                   activityType = ActivityType.Info,
                                   contentType = MimeTypes.text_plain
                               },
                           new Actions
                               {
                                   label = "See More Videos",
                                   //uri = "local:Text:videos",
                                   uri =
                                       ConfigurationManager.AppSettings["SiteUrl"].ToString(CultureInfo.InvariantCulture) +
                                       "/ViewMonumentVideos.aspx?hotSpotID=" +
                                       hotSpot.Id.ToString(CultureInfo.InvariantCulture),
                                   //It should be ActivityType.Video
                                   activityType = ActivityType.Info,
                                   contentType = MimeTypes.text_plain
                               },
                           new Actions
                               {
                                   label = "Listen To Audio",
                                   uri = hotSpot.Monument_Audio,
                                   activityType = ActivityType.Audio,
                                   //It should be MimeTypes.audio_mp4 or any audio format
                                   contentType = MimeTypes.text_plain
                               },
                           new Actions
                               {
                                   label = "More Info",
                                   uri = "",
                                   activityType = ActivityType.Info,
                                   contentType = MimeTypes.text_plain
                               },
                           new Actions
                               {
                                   label = "Post Review",
                                   uri = "",
                                   activityType = ActivityType.Info,
                                   contentType = MimeTypes.text_plain
                               }

                       };
        }

        /// <summary>
        /// used to retrieve Reviews for specific hotspot
        /// </summary>
        /// <param name="spot">hotspot object used to retrieve reviews for</param>
        /// <param name="from">used to skip number of reviews</param>
        /// <param name="length">used to take specific number of reviews</param>
        /// <returns>List of review object</returns>
        private List<review> GetHotSpotReviewsList(layerhotspot spot, int from, int length)
        {
            var hotSpotReviews =
                MonumentReviewsRepository.GetFilteredElementsWith(reviews => reviews.hotSpotID == spot.Id,
                                                                  reviews => reviews.Tourist).Skip(from).Take(length);
            var hotSpotReviewsList = new List<review>();
            hotSpotReviews.ToList().ForEach(reviews => hotSpotReviewsList.Add(
                new review
                    {
                        username = reviews.Tourist.UserName,
                        usercomment = reviews.Review
                    }
                                                           ));
            return hotSpotReviewsList;
        }

        /// <summary>
        /// used to retrieve Photos for specific hotspot
        /// </summary>
        /// <param name="spot">hotspot object used to retrieve reviews for</param>
        /// <returns>List of Photo object</returns>
        private List<Photo> GetHotSpotPhotosList(layerhotspot spot)
        {
            var hotSpotPhotos = MonumentPhotosRepository.GetFilteredElements(photos => photos.hostSpotID == spot.Id);
            var hotSpotPhotosList = new List<Photo>();

            hotSpotPhotos.ToList().ForEach(
                photos => hotSpotPhotosList.Add(
                    new Photo
                        {
                            id = photos.ID.ToString(CultureInfo.InvariantCulture),
                            description = photos.Description,
                            contentType = GetImageContentType(photos.ImageUrl),
                            url = photos.ImageUrl
                        }
                              )
                );
            return hotSpotPhotosList;
        }

        /// <summary>
        /// used to retrieve videos for specific hotspot
        /// </summary>
        /// <param name="spot">hotspot object used to retrieve reviews for</param>
        /// <returns>List of video object</returns>
        private List<video> GetHotSpotVideosList(layerhotspot spot)
        {
            var videos = MonumentVideosRepository.GetFilteredElements(video => video.hostSpotID == spot.Id);
            var videosList = new List<video>();
            videos.ToList().ForEach(monumentsVideo => videosList.Add(
                new video
                    {
                        id = monumentsVideo.ID.ToString(CultureInfo.InvariantCulture),
                        description = monumentsVideo.Description,
                        contentType = GetVideoContentType(monumentsVideo.video),
                        length = monumentsVideo.VideoLength,
                        url = monumentsVideo.video
                    }
                                                          ));
            return videosList;
        }

        /// <summary>
        /// used to retrieve content type of any video file
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        private string GetVideoContentType(string ext)
        {
            return MimeTypes.video_mp4;
        }

        /// <summary>
        /// virtual method , you have to override it ... 
        /// modify the GetImageContentType() Method to retrieve the image content type
        /// based on image url(extension) http://domain.com/path/image.ext
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        private string GetImageContentType(string imageUrl)
        {
            return MimeTypes.image_jpeg;
        }

        private IEnumerable<layerhotspot> GetHotSpotsInDb(LayerQueryParam layerQueryParams)
        {
            Expression<Func<layerhotspot, bool>> filter = layerhotspot => layerhotspot.poiType == "geo";
            Func<layerhotspot, bool> filterByDistance = (
                                                            layerhotspot => MesuringDistanceAlgorithms.
                                                                                GetDistanceBetweenPoints(
                                                                                    Convert.ToDouble(
                                                                                        layerQueryParams.lat),
                                                                                    Convert.ToDouble(
                                                                                        layerQueryParams.lon),
                                                                                    Convert.ToDouble(
                                                                                        layerhotspot.lat.HasValue
                                                                                            ? layerhotspot.lat.Value
                                                                                            : 0),
                                                                                    Convert.ToDouble(
                                                                                        layerhotspot.lon.HasValue
                                                                                            ? layerhotspot.lon.Value
                                                                                            : 0))
                                                                            <=
                                                                            (Convert.ToDouble(layerQueryParams.radius))
                                                        );
            //+Convert.ToDouble(layerQueryParams.accuracy)

            var hotSpotsInDB = LayerHotSpotsRepository.GetFilteredElements(filter: filter).Where(filterByDistance);
            //.TakeWhile(filterByDistance);
            return hotSpotsInDB;
        }
        private IEnumerable<layerhotspot> GetHotSpotsInDbOptimized(LayerQueryParam layerQueryParams)
        {
            Func<layerhotspot, bool> filter = layerhotspot => layerhotspot.poiType == "geo";

            var layerhotspots = LayerHotSpotsRepository.UnitOfWork.ExecuteQuery<layerhotspot>
                ("exec GetHotSpotsWithinDistance @p1,@p2,@p3",
                 new SqlParameter {ParameterName = "p1", Value = Convert.ToDouble(layerQueryParams.lat)},
                 new SqlParameter {ParameterName = "p2", Value = Convert.ToDouble(layerQueryParams.lon)},
                 new SqlParameter
                     {
                         ParameterName = "p3",
                         Value = Convert.ToDouble(layerQueryParams.radius) + Convert.ToDouble(layerQueryParams.accuracy)
                     });

            return layerhotspots.Where(filter);
        }
    }
    #endregion
}
