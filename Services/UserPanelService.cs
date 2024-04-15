using Microsoft.AspNetCore.Identity;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Services
{
    public class UserPanelService : UserPanelInterface
    {
        private UserManager<Guser> _userManager;
        private SiteDbContext _siteDbContext;
        public UserPanelService(UserManager<Guser> userManager, SiteDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
            _userManager = userManager;
        }
        public UserViewModel ShowUserList(string sortOrder)
        {
            var gusers = from u in _siteDbContext.Guser select u;
            var userModel = new UserViewModel();
            switch (sortOrder)
            {
                case "name":
                    gusers = gusers.OrderBy(u => u.UserName);
                    break;
                case "email":
                    gusers = gusers.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    gusers = gusers.OrderByDescending(u => u.Email);
                    break;
                default:
                    gusers = gusers.OrderByDescending(u => u.UserName);
                    break;
            }
            userModel.Gusers = gusers.ToList();
            return userModel;
        }
        public EditModel ShowEdit(string id)
        {
            var editModel = new EditModel();
            editModel.guser = _siteDbContext.Guser.Find(id);
            return editModel;
        }
        public async Task EditUserList(EditModel editModel)
        {
            var guser = _siteDbContext.Guser.Find(editModel.guser.Id);
            var isRoleOwner = await _userManager.IsInRoleAsync(guser, "Owner");
            var oldRole = await _userManager.GetRolesAsync(guser);
            if (guser != null && isRoleOwner == false)
            {
                guser.UserName = editModel.guser.UserName;
                guser.Email = editModel.guser.Email;
                guser.NormalizedEmail = _userManager.NormalizeEmail(editModel.guser.Email);
                guser.NormalizedUserName = _userManager.NormalizeName(editModel.guser.UserName);
                var isRoleSame = await _userManager.IsInRoleAsync(guser, editModel.role.Name);
                if (isRoleSame == false)
                {
                    await _userManager.AddToRoleAsync(guser, editModel.role.Name);
                    await _userManager.RemoveFromRoleAsync(guser, oldRole[0]);
                }
                _siteDbContext.SaveChanges();
            }
        }
        public async Task DeleteUserList(EditModel editModel)
        {
            var guser = _siteDbContext.Guser.Find(editModel.guser.Id);
            var isRoleOwner = await _userManager.IsInRoleAsync(guser, "Owner");
            if (guser != null && isRoleOwner == false)
            {
                _siteDbContext.Guser.Remove(guser);
                await _siteDbContext.SaveChangesAsync();
            }
        }
    }
}
