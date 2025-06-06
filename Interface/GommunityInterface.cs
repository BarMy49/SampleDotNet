using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface GommunityInterface
    {
        public Gommunity GetGommunityByName(string gommunityName);
        public void SavePost(Post post, Guser guser);
        public Post GetPostById(Guid postId);
        public void DeletePost(Guid postId);
        public void SaveReaction(Reaction reaction);
        public void DeleteReaction(Reaction reaction);
        public void AddComment(Comment comment);
        public void DeleteComment(Guid commentId);
    }
}
