using MongoDBAPI.Models;

namespace MongoDBAPI.Repositories
{
    public interface ISocialMediaCollection
    {
        Task InsertSocialMedia(SocialMedia socialMedia);
        Task UpdateSocialMedia(SocialMedia socialMedia);
        Task DeleteSocialMedia(string code);

        Task<List<SocialMedia>> GetAllSocialMedia();
        Task<SocialMedia> GetSocialMediaById(string code);
    }
}
