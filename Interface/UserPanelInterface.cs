using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface UserPanelInterface
    {
        public UserViewModel ShowUserList(string sortOrder);
        public EditModel ShowEdit(string id);
        public Task EditUserList(EditModel editModel);
        public Task DeleteUserList(EditModel editModel);
    }
}
