using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.BusinessLayer;
using NoblePermission.BusinessLayer.Services.PaymentLimitServices;
using NoblePermission.BusinessLayer.Services.PermissionServices;
using NoblePermission.BusinessLayer.Services.UserServices;
using NoblePermission.Persistance;
using NoblePermission.Persistance.Entities;
using NoblePermission.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;
using NoblePermission.BusinessLayer.Services.FtpAccountServices;
using Microsoft.Extensions.Logging;

namespace NoblePermission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoblePermissionController : ControllerBase
    {
        public readonly ApplicationDbContext Context;
        public IPermission Permission;
        private IUser _user;
        private IPaymentLimit _paymentLimit;
        private IFtpAccountDetail _ftpAccountDetail;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<NoblePermissionController> _logger;
        public NoblePermissionController(ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment,
            IPermission permission, IUser user, IPaymentLimit paymentLimit,
            IFtpAccountDetail ftpAccountDetail,
            ILogger<NoblePermissionController> logger)
        {
            Context = context;
            Permission = permission;
            _user = user;
            _paymentLimit = paymentLimit;
            _hostingEnvironment = hostEnvironment;
            _ftpAccountDetail = ftpAccountDetail;
            _logger = logger;
        }
        #region NoblePermission


        [Route("api/NoblePermission/SaveNoblePermission")]
        [HttpPost("SaveNoblePermission")]
        public IActionResult SaveNoblePermission([FromBody] NoblePermissionLookUp noblePermissionLook)
        {
            var result = Permission.AddPermission(noblePermissionLook);
            if (result == "Success")
            {
                return Ok(new { IsSuccess = true });
            }
            return Ok(new { IsSuccess = false, Message = result });
        }

        [Route("api/NoblePermission/GetNoblePermissionByGroupId")]
        [HttpGet("GetNoblePermissionByGroupId")]
        public IActionResult GetNoblePermissionByGroupId(Guid id)
        {
            var result = Permission.GetPermissionByGroupId(id);

            return Ok(new { result });
        }

        [Route("api/NoblePermission/GetAllPermissionData")]
        [HttpGet("GetAllPermissionData")]
        public IActionResult GetAllPermissionData(Guid id, string systemType)
        {
            var result = Permission.GetAllPermissionDataAsync(id, systemType);

            return Ok(new { result });
        }

        [Route("api/NoblePermission/GetNobleGroupById")]
        [HttpGet("GetNobleGroupById")]
        public IActionResult GetNobleGroupById(Guid id)
        {
            var result = Permission.GetNobleGroupById(id);

            return Ok(new { result });
        }

        [Route("api/NoblePermission/GetAllNobleModule")]
        [HttpGet("GetAllNobleModule")]
        public IActionResult GetAllNobleModule()
        {
            var result = Permission.GetAllNobleModule();

            return Ok(new { result });
        }

        [Route("api/NoblePermission/GetNobleGroup")]
        [HttpGet("GetNobleGroup")]
        public IActionResult GetNobleGroup()
        {
            var nobleGroup = Context.NobleGroups.ToList();
            var groupLookUpList = new List<GroupLookUp>();
            foreach (var group in nobleGroup)
            {
                groupLookUpList.Add(new GroupLookUp()
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    GroupType = Enum.GetName(typeof(GroupType), group.GroupType)
                });
            }
            return Ok(groupLookUpList);
        }
        [Route("api/NoblePermission/AddNobleGroup")]
        [HttpPost("AddNobleGroup")]
        public IActionResult AddNobleGroup([FromBody] GroupLookUp groupLookUp)
        {
            try
            {
                var isGroupNameExist =
                    Context.NobleGroups.Any(x => x.GroupName.ToLower() == groupLookUp.GroupName.ToLower() && x.GroupType == (GroupType)Enum.Parse(typeof(GroupType), groupLookUp.GroupType));
                if (!isGroupNameExist)
                {
                    var nobleGroup = new NobleGroup()
                    {
                        GroupName = groupLookUp.GroupName,
                        GroupType = (GroupType)Enum.Parse(typeof(GroupType), groupLookUp.GroupType)
                    };
                    Context.NobleGroups.Add(nobleGroup);
                    Context.SaveChanges();
                    groupLookUp.Id = nobleGroup.Id;
                    return Ok(new { IsSuccess = true, nobleGroup = groupLookUp });
                }
                return Ok(new { IsSuccess = false, Message = "Group already exist" });

            }
            catch (Exception ex)
            {
                return Ok(new { IsSuccess = false, Message = ex.Message });
            }
        }



        #endregion


        #region UserLogin
        [Route("api/NoblePermission/UserLogin")]
        [HttpPost("UserLogin")]
        public IActionResult UserLogin([FromBody] LoginVm loginVm)
        {
            if (loginVm.Email == null || loginVm.Password == null)
                return Ok("Please enter email and password");
            var user = _user.LoginUser(loginVm.Email, loginVm.Password);
            if (user == null)
                return Ok("Please valid email or password");
            if (user is string)
                return Ok(user);
            //var userVm = _mapper.Map<UserVm>(user);

            //var token = GetToken(userVm);
            //var success = "success_" + token;
            return Ok("success");

        }


        #endregion

        #region AutoPermissionToLocation

        [Route("api/NoblePermission/CompanyInformation")]
        [HttpPost("CompanyInformation")]
        public IActionResult CompanyInformation([FromBody] CompanyLookUp companyLookUp)
        {
            var result = Permission.AddCompanyInformation(companyLookUp);
            if ((string)result == "Success" || (string)result == "Updated")
            {
                return Ok(new { IsSuccess = true, Message = result });
            }
            return Ok(new { IsSuccess = false, Message = result });
        }
        [Route("api/NoblePermission/GetCompanyList")]
        [HttpGet("GetCompanyList")]
        public IActionResult GetCompanyList(string searchTerm, int? pageNumber)
        {
            var result = Permission.GetCompanyList(searchTerm, pageNumber);

            return Ok(new { IsSuccess = true, Message = result });
        }
        [Route("api/NoblePermission/CompanyLicensing")]
        [HttpPost("CompanyLicensing")]
        public IActionResult CompanyLicensing([FromBody] CompanyLicenseLookUp companyLicenseLookUp)
        {
            var result = Permission.CompanyLicensing(companyLicenseLookUp);
            if ((string)result == "Success")
            {
                return Ok(new { IsSuccess = true, Message = result });
            }

            return Ok(new { IsSuccess = false, Message = result });
        }
        [Route("api/NoblePermission/GetLicenseDetail")]
        [HttpGet("GetLicenseDetail")]
        public IActionResult GetLicenseDetail(Guid companyId)
        {
            var result = Permission.GetLicenseDetail(companyId);


            return Ok(new { IsSuccess = true, Message = result });
        }

        [Route("api/NoblePermission/GetCompanyPermissionById")]
        [HttpGet("GetCompanyPermissionById")]
        public IActionResult GetCompanyPermissionById(Guid id)
        {
            var result = Permission.GetCompanyPermissionById(id);

            return Ok(new { result });
        }

        [Route("api/NoblePermission/GetWhiteLabelingData")]
        [HttpGet("GetWhiteLabelingData")]
        public IActionResult GetWhiteLabelingData()
        {
            var result = Permission.GetWhiteLabelingData();

            return Ok(new { result });
        }

        [Route("api/NoblePermission/SaveCompanyPermissions")]
        [HttpPost("SaveCompanyPermissions")]
        public IActionResult SaveCompanyPermissions([FromBody] NoblePermissionLookUp noblePermissionLook)
        {
            var result = Permission.SaveCompanyPermissions(noblePermissionLook);
            if (result == "Success")
            {
                return Ok(new { IsSuccess = true });
            }
            return Ok(new { IsSuccess = false, Message = result });
        }
        [Route("api/NoblePermission/DisActiveLicense")]
        [HttpGet("DisActiveLicense")]
        public IActionResult DisActiveLicense(Guid companyId, bool isCompany)
        {
            var result = Permission.DisActiveLicense(companyId, isCompany);
            if (result is String && result.ToString() == "Success")
            {
                return Ok(new { IsSuccess = true });
            }
            return Ok(new { IsSuccess = false, Message = result });
        }
        #endregion

        #region PaymentLimit

        [Route("api/NoblePermission/AddUpdatePaymentLimit")]
        [HttpPost("AddUpdatePaymentLimit")]
        public IActionResult AddUpdatePaymentLimit([FromBody] PaymentLimit paymentLimit)
        {
            var result = _paymentLimit.AddUpdatePaymentLimit(paymentLimit);
            if ((string)result == "Add" || (string)result == "Update")
            {
                return Ok(new { IsSuccess = true, Message = result });
            }
            return Ok(new { IsSuccess = false, Message = result });
        }
        [Route("api/NoblePermission/GetLastPaymentLimit")]
        [HttpGet("GetLastPaymentLimit")]
        public IActionResult GetLastPaymentLimit(Guid companyId)
        {
            var result = _paymentLimit.GetLastPaymentLimit(companyId);
            if (result is PaymentLimit)
            {
                return Ok(new { IsSuccess = true, Message = result });
            }
            return Ok(new { IsSuccess = false, Message = result });
        }

        #endregion

        #region WhiteLabelling

        [Route("api/NoblePermission/UploadFilesAsync")]
        [HttpPost("UploadFilesAsync")]
        public IActionResult UploadFilesAsync(List<IFormFile> files, string imageName)
        {
            var dbPath = string.Empty;
            if (files != null && files.Any())
            {
                //check if Attachment folder is not created
                var pathWithFolderName = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Attachment");
                if (!Directory.Exists(pathWithFolderName))
                {
                    var di = Directory.CreateDirectory(pathWithFolderName);
                }
                foreach (var file in files)
                {
                    //save file in attachment folder
                    if (file.Length > 0)
                    {
                        var extenstion = Path.GetExtension(file.FileName).ToLowerInvariant();
                        var myUniqueFileName = Guid.NewGuid().ToString();
                        string dbFilePath = string.IsNullOrEmpty(imageName) ? myUniqueFileName : imageName;
                        dbPath = "/Attachment/" + dbFilePath;
                        string filePath = Path.Combine(pathWithFolderName, dbFilePath);
                        var fileStream = new FileStream(filePath, FileMode.Create);
                        file.CopyTo(fileStream);
                        fileStream.Close();
                    }
                }
            }
            return Ok(dbPath);
        }

        [Route("api/NoblePermission/SaveWhiteLabelInfo")]
        [HttpPost("SaveWhiteLabelInfo")]
        public IActionResult SaveWhiteLabelInfo([FromBody] WhiteLabelLookUp whitelabelLookUp)
        {
            try
            {
                if (whitelabelLookUp.Id == Guid.Empty)
                {
                    var whitelabel = new WhiteLabelling()
                    {
                        Id = new Guid(),
                        Heading = whitelabelLookUp.Heading,
                        Description = whitelabelLookUp.Description,
                        TagImage1Name = whitelabelLookUp.TagImage1Name,
                        TagImage1Path = whitelabelLookUp.TagImage1Path,
                        TagImage2Name = whitelabelLookUp.TagImage2Name,
                        TagImage2Path = whitelabelLookUp.TagImage2Path,
                        CompanyName = whitelabelLookUp.CompanyName,
                        ApplicationName = whitelabelLookUp.ApplicationName,
                        AddressLine1 = whitelabelLookUp.AddressLine1,
                        AddressLine2 = whitelabelLookUp.AddressLine2,
                        AddressLine3 = whitelabelLookUp.AddressLine3,
                        LoginLogoName = whitelabelLookUp.LoginLogoName,
                        LoginLogoPath = whitelabelLookUp.LoginLogoPath,
                        LoginScreenImageName = whitelabelLookUp.LoginScreenImageName,
                        LoginScreenImagePath = whitelabelLookUp.LoginScreenImagePath,
                        BackgroundImageName = whitelabelLookUp.BackgroundImageName,
                        BackgroundImagePath = whitelabelLookUp.BackgroundImagePath,
                        SidebarImageName = whitelabelLookUp.SidebarImageName,
                        SidebarImagePath = whitelabelLookUp.SidebarImagePath,
                        FavIconName = whitelabelLookUp.FavIconName,
                        FavIconPath = whitelabelLookUp.FavIconPath,
                        FavName = whitelabelLookUp.FavName,
                        Email = whitelabelLookUp.Email,
                        SideMenuColor = whitelabelLookUp.SideMenuColor,
                        SideMenuBtnColor = whitelabelLookUp.SideMenuBtnColor,
                        SideMenuBtnClickColor = whitelabelLookUp.SideMenuBtnClickColor,
                        SaveBtnBgColor = whitelabelLookUp.SaveBtnBgColor,
                        SaveBtnColor = whitelabelLookUp.SaveBtnColor,
                        CancelBgBtnColor = whitelabelLookUp.CancelBgBtnColor,
                        CancelBtnColor = whitelabelLookUp.CancelBtnColor,
                        HeadingColor = whitelabelLookUp.HeadingColor,
                        TableHeaderBgColor = whitelabelLookUp.TableHeaderBgColor,
                        TableHeaderColor = whitelabelLookUp.TableHeaderColor,
                        InvoiceTitleBgColor = whitelabelLookUp.InvoiceTitleBgColor,
                        InvoiceTitleColor = whitelabelLookUp.InvoiceTitleColor,

                        ApplicationBgColor = whitelabelLookUp.ApplicationBgColor,
                        ApplicationTextColor = whitelabelLookUp.ApplicationTextColor,
                        CardBgColor = whitelabelLookUp.CardBgColor,
                        CardTextColor = whitelabelLookUp.CardTextColor,
                        SetupBgColor = whitelabelLookUp.SetupBgColor,
                        SetupTextColor = whitelabelLookUp.SetupTextColor,
                        SetupCssFilter = whitelabelLookUp.SetupCssFilter,
                        SideMenuBtnColorFilter = whitelabelLookUp.SideMenuBtnColorFilter,
                        SideMenuBtnClickColorFilter = whitelabelLookUp.SideMenuBtnClickColorFilter,
                        SideMenuBtnClickBgColor = whitelabelLookUp.SideMenuBtnClickBgColor,

                    };
                    Context.WhiteLabellings.Add(whitelabel);
                    Context.SaveChanges();
                    return Ok(new { IsSuccess = true, Message = "K" });
                }
                else
                {
                    var form = Context.WhiteLabellings.FirstOrDefault(x => x.Id == whitelabelLookUp.Id);

                    form.Id = whitelabelLookUp.Id;
                    form.Heading = whitelabelLookUp.Heading;
                    form.Description = whitelabelLookUp.Description;
                    form.TagImage1Name = whitelabelLookUp.TagImage1Name;
                    form.TagImage1Path = whitelabelLookUp.TagImage1Path;
                    form.TagImage2Name = whitelabelLookUp.TagImage2Name;
                    form.TagImage2Path = whitelabelLookUp.TagImage2Path;
                    form.CompanyName = whitelabelLookUp.CompanyName;
                    form.ApplicationName = whitelabelLookUp.ApplicationName;
                    form.AddressLine1 = whitelabelLookUp.AddressLine1;
                    form.AddressLine2 = whitelabelLookUp.AddressLine2;
                    form.AddressLine3 = whitelabelLookUp.AddressLine3;
                    form.LoginLogoName = whitelabelLookUp.LoginLogoName;
                    form.LoginLogoPath = whitelabelLookUp.LoginLogoPath;
                    form.LoginScreenImageName = whitelabelLookUp.LoginScreenImageName;
                    form.LoginScreenImagePath = whitelabelLookUp.LoginScreenImagePath;
                    form.BackgroundImageName = whitelabelLookUp.BackgroundImageName;
                    form.BackgroundImagePath = whitelabelLookUp.BackgroundImagePath;
                    form.SidebarImageName = whitelabelLookUp.SidebarImageName;
                    form.SidebarImagePath = whitelabelLookUp.SidebarImagePath;
                    form.FavIconName = whitelabelLookUp.FavIconName;
                    form.FavIconPath = whitelabelLookUp.FavIconPath;
                    form.FavName = whitelabelLookUp.FavName;
                    form.Email = whitelabelLookUp.Email;
                    form.SideMenuColor = whitelabelLookUp.SideMenuColor;
                    form.SideMenuBtnColor = whitelabelLookUp.SideMenuBtnColor;
                    form.SideMenuBtnClickColor = whitelabelLookUp.SideMenuBtnClickColor;
                    form.SaveBtnBgColor = whitelabelLookUp.SaveBtnBgColor;
                    form.SaveBtnColor = whitelabelLookUp.SaveBtnColor;
                    form.CancelBgBtnColor = whitelabelLookUp.CancelBgBtnColor;
                    form.CancelBtnColor = whitelabelLookUp.CancelBtnColor;
                    form.HeadingColor = whitelabelLookUp.HeadingColor;
                    form.TableHeaderBgColor = whitelabelLookUp.TableHeaderBgColor;
                    form.TableHeaderColor = whitelabelLookUp.TableHeaderColor;
                    form.InvoiceTitleBgColor = whitelabelLookUp.InvoiceTitleBgColor;
                    form.InvoiceTitleColor = whitelabelLookUp.InvoiceTitleColor;

                    form.ApplicationBgColor = whitelabelLookUp.ApplicationBgColor;
                    form.ApplicationTextColor = whitelabelLookUp.ApplicationTextColor;
                    form.CardBgColor = whitelabelLookUp.CardBgColor;
                    form.CardTextColor = whitelabelLookUp.CardTextColor;
                    form.SetupBgColor = whitelabelLookUp.SetupBgColor;
                    form.SetupTextColor = whitelabelLookUp.SetupTextColor;
                    form.SetupCssFilter = whitelabelLookUp.SetupCssFilter;
                    form.SideMenuBtnColorFilter = whitelabelLookUp.SideMenuBtnColorFilter;
                    form.SideMenuBtnClickColorFilter = whitelabelLookUp.SideMenuBtnClickColorFilter;
                    form.SideMenuBtnClickBgColor = whitelabelLookUp.SideMenuBtnClickBgColor;
                    Context.SaveChanges();
                    // Context.WhiteLabellings.Update(whitelabel);

                    return Ok(new { IsSuccess = true, Message = "K" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { IsSuccess = false, Message = ex.Message });
            }
        }

        [Route("api/NoblePermission/GetFormData")]
        [HttpGet("GetFormData")]
        public WhiteLabelLookUp GetFormData()
        {
            try
            {
                var data = Context.WhiteLabellings.AsNoTracking().FirstOrDefault();
                var whiteLabellingLookup = new WhiteLabelLookUp();
                if (data != null)
                {
                    whiteLabellingLookup.Id = data.Id;
                    whiteLabellingLookup.Heading = data.Heading;
                    whiteLabellingLookup.Description = data.Description;
                    whiteLabellingLookup.TagImage1Name = data.TagImage1Name;
                    whiteLabellingLookup.TagImage1Path = data.TagImage1Path;
                    whiteLabellingLookup.TagImage2Name = data.TagImage2Name;
                    whiteLabellingLookup.TagImage2Path = data.TagImage2Path;
                    whiteLabellingLookup.CompanyName = data.CompanyName;
                    whiteLabellingLookup.ApplicationName = data.ApplicationName;
                    whiteLabellingLookup.AddressLine1 = data.AddressLine1;
                    whiteLabellingLookup.AddressLine2 = data.AddressLine2;
                    whiteLabellingLookup.AddressLine3 = data.AddressLine3;
                    whiteLabellingLookup.LoginLogoName = data.LoginLogoName;
                    whiteLabellingLookup.LoginLogoPath = data.LoginLogoPath;
                    whiteLabellingLookup.LoginScreenImageName = data.LoginScreenImageName;
                    whiteLabellingLookup.LoginScreenImagePath = data.LoginScreenImagePath;
                    whiteLabellingLookup.BackgroundImageName = data.BackgroundImageName;
                    whiteLabellingLookup.BackgroundImagePath = data.BackgroundImagePath;
                    whiteLabellingLookup.SidebarImageName = data.SidebarImageName;
                    whiteLabellingLookup.SidebarImagePath = data.SidebarImagePath;
                    whiteLabellingLookup.FavIconName = data.FavIconName;
                    whiteLabellingLookup.FavIconPath = data.FavIconPath;
                    whiteLabellingLookup.FavName = data.FavName;
                    whiteLabellingLookup.Email = data.Email;
                    whiteLabellingLookup.SideMenuColor = data.SideMenuColor;
                    whiteLabellingLookup.SideMenuBtnColor = data.SideMenuBtnColor;
                    whiteLabellingLookup.SideMenuBtnClickColor = data.SideMenuBtnClickColor;
                    whiteLabellingLookup.SaveBtnBgColor = data.SaveBtnBgColor;
                    whiteLabellingLookup.SaveBtnColor = data.SaveBtnColor;
                    whiteLabellingLookup.CancelBgBtnColor = data.CancelBgBtnColor;
                    whiteLabellingLookup.CancelBtnColor = data.CancelBtnColor;
                    whiteLabellingLookup.HeadingColor = data.HeadingColor;
                    whiteLabellingLookup.TableHeaderBgColor = data.TableHeaderBgColor;
                    whiteLabellingLookup.TableHeaderColor = data.TableHeaderColor;
                    whiteLabellingLookup.InvoiceTitleBgColor = data.InvoiceTitleBgColor;
                    whiteLabellingLookup.InvoiceTitleColor = data.InvoiceTitleColor;

                    whiteLabellingLookup.ApplicationBgColor = data.ApplicationBgColor;
                    whiteLabellingLookup.ApplicationTextColor = data.ApplicationTextColor;
                    whiteLabellingLookup.CardBgColor = data.CardBgColor;
                    whiteLabellingLookup.CardTextColor = data.CardTextColor;
                    whiteLabellingLookup.SetupBgColor = data.SetupBgColor;
                    whiteLabellingLookup.SetupTextColor = data.SetupTextColor;
                    whiteLabellingLookup.SetupCssFilter = data.SetupCssFilter;
                    whiteLabellingLookup.SideMenuBtnColorFilter = data.SideMenuBtnColorFilter;
                    whiteLabellingLookup.SideMenuBtnClickColorFilter = data.SideMenuBtnClickColorFilter;
                    whiteLabellingLookup.SideMenuBtnClickBgColor = data.SideMenuBtnClickBgColor;


                }
                return whiteLabellingLookup;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("api/NoblePermission/GetBaseImage")]
        [HttpGet("GetBaseImage")]

        public async Task<IActionResult> GetBaseImage(string filePath)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            var base64 = Convert.ToBase64String(bytes);
            return Ok(base64);
        }

        [Route("api/NoblePermission/DownloadFile")]
        [HttpGet("DownloadFile")]
        //[Authorize(Roles = "User, Super Admin")]
        public async Task<IActionResult> DownloadFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/pdf" /*"image/png"*/, Path.GetFileName(path));

        }

        #endregion

        #region FtpAccountDetail
        [Route("api/NoblePermission/SaveFtpAccountDetail")]
        [HttpPost("SaveFtpAccountDetail")]
        public IActionResult SaveFtpAccountDetail([FromBody] FtpAccountDetailLookup ftpAccountDetailLookup)
        {
            try
            {
                var result = _ftpAccountDetail.AddUpdateFtpDetail(ftpAccountDetailLookup);
                return Ok(new { IsSuccess = true, Message = result });
            }
            catch (Exception ex)
            {
                return Ok(new { IsSuccess = false, Message = ex.Message });
            }
        }
        [Route("api/NoblePermission/GetFtpAccountList")]
        [HttpGet("GetFtpAccountList")]
        public IActionResult GetFtpAccountList(Guid companyId)
        {
            try
            {
                var result = _ftpAccountDetail.GetFtpAccountDetailsList(companyId);
                return Ok(new { IsSuccess = true, Message = result });
            }
            catch (Exception ex)
            {
                return Ok(new { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

    }
}
