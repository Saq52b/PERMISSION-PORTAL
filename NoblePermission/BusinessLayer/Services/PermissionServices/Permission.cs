using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using NoblePermission.Persistance;
using NoblePermission.Persistance.Entities;
using NoblePermission.ViewModel;

namespace NoblePermission.BusinessLayer.Services.PermissionServices
{
    public class Permission : IPermission
    {
        public readonly ApplicationDbContext Context;
        private IWebHostEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        public Permission(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            Context = context;
            _hostingEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        public string AddPermission(NoblePermissionLookUp noblePermissionLook)
        {
            try
            {
                //Add Modules if not exist in database
                var moduleList = Context.NobleModules.Select(x => x.Id).ToList();
                if (moduleList.Count != noblePermissionLook.Modules.Count)
                {
                    foreach (var module in noblePermissionLook.Modules)
                    {
                        if (!moduleList.Contains(module.Id))
                        {
                            var nobleModule = new NobleModule()
                            {
                                Id = module.Id,
                                Name = module.Name,
                                Description = module.Description
                            };
                            Context.NobleModules.Add(nobleModule);
                        }
                    }


                }
                //Add Permission
                var noblePermissionList = Context.NoblePermissions.Where(x => x.NobleGroupId == noblePermissionLook.GroupId).ToList();


                //Add and remove permission according to group


                if (noblePermissionList.Count > 0)
                {
                    foreach (var permission in noblePermissionList)
                    {
                        if (noblePermissionLook.Permissions.Count(x =>
                            x.Key == permission.Key && x.Checked) <= 0)
                        {
                            var companyPermissions = Context.CompanyPermissions.Where(x => x.Key == permission.Key);
                            Context.CompanyPermissions.RemoveRange(companyPermissions);
                            Context.NoblePermissions.Remove(permission);
                        }
                    }
                    Context.SaveChanges();
                    var permissionList = Context.NoblePermissions.Where(x => x.NobleGroupId == noblePermissionLook.GroupId).ToList();
                    var companyPermissionList = new List<CompanyPermission>();
                    var noblePermissionListData = new List<Persistance.Entities.NoblePermission>();
                    foreach (var permission in noblePermissionLook.Permissions)
                    {
                        //if(companyPermission.Count(x=>x.Key == permission.Key))
                        if (permission.Checked && permissionList.Any(x => x.Key != permission.Key))
                        {
                            noblePermissionListData.Add(new Persistance.Entities.NoblePermission()
                            {
                                PermissionName = permission.PermissionName,
                                Key = permission.Key,
                                Value = permission.Value,
                                NobleGroupId = noblePermissionLook.GroupId,
                                NobleModuleId = permission.ModuleId
                            });


                            var companyList =
                                Context.Companies.Where(x => x.NobleGroupId == noblePermissionLook.GroupId);
                            var c = companyList.Count();
                            foreach (var company in companyList)
                            {
                                companyPermissionList.Add(new CompanyPermission()
                                {
                                    Key = permission.Key,
                                    NobleGroupId = noblePermissionLook.GroupId,
                                    NobleModuleId = permission.ModuleId,
                                    PermissionName = permission.PermissionName,
                                    Value = permission.Value,
                                    CompanyId = company.Id
                                });
                            }
                        }
                    }

                    if (noblePermissionListData.Count > 0)
                        Context.NoblePermissions.AddRange(noblePermissionListData);
                    if (companyPermissionList.Count > 0)
                        Context.CompanyPermissions.AddRange(companyPermissionList);
                }
                else
                {
                    var permissionList = new List<Persistance.Entities.NoblePermission>();
                    foreach (var permission in noblePermissionLook.Permissions)
                    {
                        //if(companyPermission.Count(x=>x.Key == permission.Key))
                        if (permission.Checked)
                        {
                            permissionList.Add(new Persistance.Entities.NoblePermission()
                            {
                                PermissionName = permission.PermissionName,
                                Key = permission.Key,
                                Value = permission.Value,
                                NobleGroupId = noblePermissionLook.GroupId,
                                NobleModuleId = permission.ModuleId

                            });

                        }
                    }
                    Context.NoblePermissions.AddRange(permissionList);
                }
                Context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.Message;
            }
        }


        public List<Persistance.Entities.NoblePermission> GetPermissionByGroupId(Guid id)
        {
            try
            {
                var data = Context.NoblePermissions.AsNoTracking().Where(x => x.NobleGroupId == id).ToList();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public object GetNobleGroupById(Guid id)
        {
            try
            {
                var data = Context.NobleGroups.AsNoTracking().FirstOrDefault(x => x.Id == id);
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<NobleModule> GetAllNobleModule()
        {
            try
            {
                var data = Context.NobleModules.AsNoTracking().ToList();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public GetAllPermissionModuleAndGroup GetAllPermissionDataAsync(Guid id, string systemType)
        {
            try
            {
        
                var permissionOfGroup = Context.CompanyPermissions.AsNoTracking().Where(x => x.CompanyId == id).ToList();
                var moduleList = Context.NobleModules.AsNoTracking().ToList();
                var companyLicenseList = Context.CompanyLicenses.AsNoTracking().Where(x => x.CompanyId == id).ToList();
                var whiteLabelingLookup = Context.WhiteLabellings.FirstOrDefault();
                var groupData = Context.NobleGroups.AsNoTracking().FirstOrDefault(x => x.Id == permissionOfGroup.ElementAt(0).NobleGroupId);
                //var groupData = permissionOfGroup?.FirstOrDefault()?.NobleGroupId != null ? Context.NobleGroups.AsNoTracking().FirstOrDefault(x => x.Id == permissionOfGroup.First().NobleGroupId) : null;

                var languge = "";

                var lookup = new GetAllPermissionModuleAndGroup
                {
                    GroupLookUp = new GroupLookUp()
                    {
                        GroupName = groupData?.GroupName,
                        GroupType = Enum.GetName(typeof(GroupType), groupData.GroupType),
                        Id = groupData.Id,
                    },
                    ModuleLookUps = new List<ModuleLookUp>(),
                    PermissionsLookUps = new List<PermissionsLookUp>(),
                    CompanyLicenseLookUps = new List<CompanyLicenseLookUp>(),
                    WhiteLabelLookUp = new WhiteLabelLookUp()
                };
                foreach (var module in moduleList)
                {
                    lookup.ModuleLookUps.Add(new ModuleLookUp()
                    {
                        Id = module.Id,
                        Value = module.Name,
                        Description = module.Description
                    });
                }
                foreach (var permission in permissionOfGroup)
                {
                    lookup.PermissionsLookUps.Add(new PermissionsLookUp()
                    {
                        Id = permission.Id,
                        Value = permission.Value,
                        Key = permission.Key,
                        PermissionName = permission.PermissionName,
                        ModuleId = permission.NobleModuleId,
                        GroupId = permission.NobleGroupId
                    });
                    if (permission.PermissionName == "Arabic")
                    {
                        languge = "Arabic";
                    }
                    else if (permission.PermissionName == "Portugues")
                    {
                        languge = "Portugese";
                    }



                }
                foreach (var license in companyLicenseList)
                {
                    lookup.CompanyLicenseLookUps.Add(new CompanyLicenseLookUp()
                    {
                        FromDate = license.FromDate,
                        ToDate = license.ToDate,
                        IsBlock = license.IsBlock,
                        IsActive = license.IsActive,
                        CompanyId = license.CompanyId,
                        LicenseType = license.LicenseType,
                        IsTechnicalSupport = license.IsTechnicalSupport,
                        IsUpdateTechnicalSupport = license.IsUpdateTechnicalSupport,
                        TechnicalSupportPeriod = license.TechnicalSupportPeriod,
                        PaymentFrequency = license.PaymentFrequency,
                        GracePeriod = license.GracePeriod,
                        ActivationPlatform = Enum.GetName(typeof(ActivationPlatform), license.ActivationPlatform)

                    });
                }
                var path = "";
                if (languge == "Arabic")
                {
                    path = Path.Combine(_hostingEnvironment.WebRootPath) + "\\LanguageCoaTemplate\\englishAndArabic.js";

                }
                else if (languge == "Portugese")
                {
                    path = Path.Combine(_hostingEnvironment.WebRootPath) + "\\LanguageCoaTemplate\\englishAndPortugal.js";

                }
                else
                {

                    path = Path.Combine(_hostingEnvironment.WebRootPath) + "\\LanguageCoaTemplate\\english.js";
                }

                string Json = System.IO.File.ReadAllText(path);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var templateList = ser.Deserialize<AccountTemplateDto>(Json);
                lookup.TemplateLists = templateList;
                if (whiteLabelingLookup != null)
                {
                    lookup.WhiteLabelLookUp.Heading = whiteLabelingLookup.Heading;
                    lookup.WhiteLabelLookUp.Description = whiteLabelingLookup.Description;
                    lookup.WhiteLabelLookUp.CompanyName = whiteLabelingLookup.CompanyName;
                    lookup.WhiteLabelLookUp.AddressLine1 = whiteLabelingLookup.AddressLine1;
                    lookup.WhiteLabelLookUp.AddressLine2 = whiteLabelingLookup.AddressLine2;
                    lookup.WhiteLabelLookUp.AddressLine3 = whiteLabelingLookup.AddressLine3;
                    lookup.WhiteLabelLookUp.ApplicationName = whiteLabelingLookup.ApplicationName;

                }
                return lookup;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async void PushWhiteLabelingImagesAsync(List<string> imagePath, Guid id, string systemType)
        {
            //string sourcePath = _hostingEnvironment.ContentRootPath + _configuration.GetSection("Ftp:LocalFolder").Value; ;


            try
            {
                var ftpAccountDetail = Context.FtpAccountDetails.FirstOrDefault(x => x.CompanyId == id && x.SystemType == systemType);
                if (ftpAccountDetail != null)
                {
                    string host = "ftp://" + ftpAccountDetail.Host;
                    if (string.IsNullOrEmpty(ftpAccountDetail.Port))
                        host += ":" + ftpAccountDetail.Port;

                    foreach (var fileName in imagePath)
                    {
                        if (string.IsNullOrEmpty(fileName))
                            continue;
                        var splitFileName = fileName.Split("/");

                        var path = Path.Combine(_hostingEnvironment.WebRootPath, splitFileName[1], splitFileName[2]);
                        var filename = Path.GetFileName(path);
                        string targetPath = filename == "logo-mini.svg" ? "/images/" : "/";
                        FtpWebRequest uploadRequest =
                        (FtpWebRequest)WebRequest.Create(host + targetPath + filename);
                        uploadRequest.Credentials = new NetworkCredential(ftpAccountDetail.Username, ftpAccountDetail.Password);
                        uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                        using (Stream fileStream = System.IO.File.OpenRead(path))
                        using (Stream ftpStream = uploadRequest.GetRequestStream())
                        {
                            await fileStream.CopyToAsync(ftpStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async void ReadCssFile(WhiteLabelling whiteLabelLookUp, Guid id, string systemType)
        {
            var ftpAccountDetail = Context.FtpAccountDetails.FirstOrDefault(x => x.CompanyId == id && x.SystemType == systemType);
            if (ftpAccountDetail != null)
            {
                string host = "ftp://" + ftpAccountDetail.Host;
                if (string.IsNullOrEmpty(ftpAccountDetail.Port))
                    host += ":" + ftpAccountDetail.Port;

                var rootPath = _hostingEnvironment.WebRootPath;
                var originalFile = rootPath + "\\paper-dashboard.txt";
                var duplicateFile = rootPath + "\\Attachment\\paper-dashboard.css";
                if (File.Exists(originalFile))
                {

                    //duplicateFile = Path.ChangeExtension(duplicateFile, ".css");
                    //File.Copy(originalFile, duplicateFile, true);

                    string[] lines = File.ReadAllLines(originalFile);
                    var menuColor = false;
                    var appColor = false;
                    var menuClickBg = false;
                    var headingColor = false;
                    var buttonColor = false;
                    var cancelColor = false;
                    var theadColor = false;
                    var ivoicePicColor = false;
                    var cardColor = false;
                    var setupMenu = false;
                    var count = 0;
                    var lineEdit = new Dictionary<int, string>();
                    foreach (string line in lines)
                    {

                        //Cancel color
                        if (line.Contains(".setup_menu {") || line.Contains(".setup_icon_wrapper {") || line.Contains(".setup_menu:hover .setup_menu_link .setup_menu_titile {") || line.Contains(".setup_menu:hover .setup_menu_link .setup_menu_desc {") || line.Contains(".setup_menu:hover .setup_icon_wrapper img {"))
                            setupMenu = true;
                        //SideBar color
                        else if (line.Contains(".sidebar .sidebar-wrapper,") || line.Contains(".sidebar:after") || line.Contains(".sidebar .nav li > a,") || line.Contains(".sidebar .sidebar-wrapper > .nav [data-toggle=") || line.Contains(".ImageWidth {") || line.Contains(".router-link-exact-active img, active {") || line.Contains(".setup_menu_titile {") || line.Contains(".setup_menu:hover {") || line.Contains(".setup_menu_desc {") || line.Contains(".setup_icon_wrapper img {"))
                            menuColor = true;
                        //SideBar color
                        else if (line.Contains(".router-link-exact-active, active {") || line.Contains(".router-link-exact-active, active span {") || line.Contains(".router-link-exact-active .sidebar-mini-icon {") || line.Contains(".router-link-exact-active i {"))
                            menuClickBg = true;
                        //SideBar color
                        else if (line.Contains(".invoice_top_bg {") || line.Contains(".bt_bg_color {") || line.Contains(".title_heading {") || line.Contains(".txt_description {"))
                            ivoicePicColor = true;
                        //Heading Color
                        else if (line.Contains(".page_title") || line.Contains(".headingOfVersion {") || line.Contains(".lightParagraph {") || line.Contains(".nobleHeading {") || line.Contains(".view_page_title") || line.Contains(".MainLightHeading {") || line.Contains(".DayHeading {") || line.Contains(".page_title {") || line.Contains(".DayHeading1 {"))
                            headingColor = true;
                        //Primary Button color
                        else if (line.Contains(".btn-primary {") || line.Contains(".btn-outline-primary {"))
                            buttonColor = true;
                        //Cancel color
                        else if (line.Contains(".btn-danger {") || line.Contains(".btn-outline-danger {"))
                            cancelColor = true;
                        //Cancel color
                        else if (line.Contains(".tableHeaderColor {") || line.Contains(".add_table_list_bg thead tr {"))
                            theadColor = true;
                        //Cancel color
                        else if (line.Contains(".main-panel {") || line.Contains(".perfect-scrollbar-on .main-panel {") || line.Contains(".backgroundColorlightBlue {") || line.Contains(".navbar.navbar-transparent {") || line.Contains(".main {") || line.Contains(".el-input__inner {") || (line.Contains(".form-control {") && line.Length < 20) || (line.Contains(".multiselect__tags {")) || (line.Contains(".multiselect__input, .multiselect__single {")) || (line.Contains(".multiselect--disabled .multiselect__current, .multiselect--disabled .multiselect__select {")))
                            appColor = true;
                        //Cancel color
                        else if (line.Contains(".card {") || line.Contains(".loginScreen {") || line.Contains(".pd_payment_methd {") || line.Contains(".setup_sidebar {") || (line.Contains(".form-control:focus {") && line.Length < 30) || (line.Contains(".card label {")))
                            cardColor = true;



                        if (menuColor && line.Contains("background") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuColor))
                        {
                            lineEdit.Add(count, "background: " + whiteLabelLookUp.SideMenuColor + ";");
                            menuColor = false;
                        }
                        else if (menuColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.SideMenuBtnColor + ";");
                            menuColor = false;
                        }
                        else if (menuColor && line.Contains("filter") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColor) && whiteLabelLookUp.SideMenuBtnColor != "#FFFFFF" && whiteLabelLookUp.SideMenuBtnColor.ToLower() != "white")
                        {
                            if (line.Contains("filter: invert(0%) sepia(0%) saturate(0%) hue-rotate(116deg) brightness(106%) contrast(101%);") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColorFilter)) lineEdit.Add(count, "filter: " + whiteLabelLookUp.SideMenuBtnColorFilter + ";");
                            if (line.Contains("invert(38%) sepia(93%) saturate(7000%) hue-rotate(206deg) brightness(100%) contrast(96%)") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickColorFilter)) lineEdit.Add(count, "filter: " + whiteLabelLookUp.SideMenuBtnClickColorFilter + ";");
                            menuColor = false;
                        }
                        else if (menuClickBg && line.Contains("background") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.SideMenuBtnClickBgColor + " !important;");
                            menuClickBg = false;
                        }
                        else if (menuClickBg && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.SideMenuBtnClickColor + " !important;");
                            menuClickBg = false;
                        }
                        else if (ivoicePicColor && line.Contains("background") && !string.IsNullOrEmpty(whiteLabelLookUp.InvoiceTitleBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.InvoiceTitleBgColor + " !important;");
                            ivoicePicColor = false;
                        }
                        else if (ivoicePicColor && line.Contains("color") && !line.Contains(".bt_bg_color {") && !string.IsNullOrEmpty(whiteLabelLookUp.InvoiceTitleColor))
                        {
                            lineEdit.Add(count, " color: " + whiteLabelLookUp.InvoiceTitleColor + " !important;");
                            ivoicePicColor = false;
                        }
                        else if (headingColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.HeadingColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.HeadingColor + " !important;");
                            headingColor = false;
                        }
                        else if (appColor && line.Contains("background-color") && !string.IsNullOrEmpty(whiteLabelLookUp.ApplicationBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.ApplicationBgColor + " !important;");
                            //appColor = false;
                        }
                        else if (appColor && line.Contains("border:") && !string.IsNullOrEmpty(whiteLabelLookUp.ApplicationBgColor))
                        {
                            lineEdit.Add(count, "border: 1px solid " + whiteLabelLookUp.ApplicationBgColor + " !important;");
                            //appColor = false;
                        }
                        else if (appColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.ApplicationTextColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.ApplicationTextColor + " !important;");
                            appColor = false;
                        }
                        else if (cardColor && line.Contains("background-color") && !string.IsNullOrEmpty(whiteLabelLookUp.CardBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.CardBgColor + " !important;");
                            if (!lines[count + 1].Contains("color"))
                                cardColor = false;
                        }
                        else if (cardColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.CardTextColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.CardTextColor + " !important;");
                            cardColor = false;
                        }
                        else if (setupMenu && line.Contains("color: #ffffff;") && !string.IsNullOrEmpty(whiteLabelLookUp.SetupTextColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.SetupTextColor + ";");
                            setupMenu = false;
                        }
                        else if (setupMenu && line.Contains("background-color") && !string.IsNullOrEmpty(whiteLabelLookUp.SetupBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.SetupBgColor + ";");
                            setupMenu = false;
                        }
                        else if (setupMenu && line.Contains("filter") && !string.IsNullOrEmpty(whiteLabelLookUp.SetupCssFilter))
                        {
                            var index = whiteLabelLookUp.SetupCssFilter.IndexOf("brightness");
                            var middleIndex = index + 16;
                            var data = whiteLabelLookUp.SetupCssFilter.Substring(0, index) + "brightness(0%) " + whiteLabelLookUp.SetupCssFilter.Substring(middleIndex, whiteLabelLookUp.SetupCssFilter.Length - middleIndex);
                            lineEdit.Add(count, "filter: " + whiteLabelLookUp.SetupCssFilter + ";");
                            setupMenu = false;
                        }
                        else if (theadColor && (line.Contains("background-color: #3178F6 !important;") || line.Contains("background-color: #3178f6;")))
                        {
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.TableHeaderBgColor)) lineEdit.Add(count, "background-color: " + whiteLabelLookUp.TableHeaderBgColor + " !important;");
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.TableHeaderColor)) lineEdit.Add(++count, "color: " + whiteLabelLookUp.TableHeaderColor + " !important;");
                            theadColor = false;
                            continue;
                        }
                        else if (buttonColor)
                        {
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && (line.Contains("background-color: #3178F6;") || line.Contains("background-color: #5491ff !important;") || line.Contains("background-color: #3178F6 !important;"))) lineEdit.Add(count, "background-color: " + whiteLabelLookUp.SaveBtnBgColor + " !important;");
                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && (line.Contains("border: 1px solid #3178F6;") || line.Contains("border: 1px solid #5491ff !important;") || line.Contains("border: 1px solid #3178F6 !important;"))) lineEdit.Add(count, "border: 1px solid " + whiteLabelLookUp.SaveBtnBgColor + " !important;");
                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnColor) && (line.Contains("color: #3178F6;") || line.Contains("color: #FFFFFF;") || line.Contains("color: #6bd098 !important;"))) lineEdit.Add(count, "color: " + whiteLabelLookUp.SaveBtnColor + " !important;");

                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && (line.Contains("border-color: #3178f6;") || line.Contains("border-color: #3178F6 !important;")))
                            {
                                lineEdit.Add(count, "border-color: " + whiteLabelLookUp.TableHeaderColor + ";");
                                buttonColor = false;
                            }

                        }
                        else if (cancelColor)
                        {
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && (line.Contains("background-color: #EB5757;") || line.Contains("background-color: #e77676 !important;") || line.Contains("background-color: #e77676;") || line.Contains("background-color: #EB5757 !important;"))) lineEdit.Add(count, "background-color: " + whiteLabelLookUp.CancelBgBtnColor + " !important;");

                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && (line.Contains("border: 1px solid #EB5757;") || line.Contains(" border: 1px solid #e77676 !important;") || line.Contains("border: 1px solid #EB5757 !important;"))) lineEdit.Add(count, " border: 1px solid  " + whiteLabelLookUp.CancelBgBtnColor + " !important;");
                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBtnColor) && (line.Contains("color: #EB5757;") || line.Contains("color: #FFFFFF;"))) lineEdit.Add(count, "color: " + whiteLabelLookUp.CancelBtnColor + ";");

                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && (line.Contains(" border-color: #ef8157;") || line.Contains("border-color: #ef8157 !important;")))
                            {
                                lineEdit.Add(count, "border-color: " + whiteLabelLookUp.CancelBgBtnColor + ";");
                                cancelColor = false;
                            }

                        }

                        count++;
                    }
                    foreach (var line in lineEdit)
                    {
                        lines[line.Key] = line.Value;
                    }
                    File.WriteAllLines(duplicateFile, lines);
                    var bytes = File.ReadAllBytes(duplicateFile);
                    FtpWebRequest uploadRequest =
                           (FtpWebRequest)WebRequest.Create(host + "/assets/css/paper-dashboard.css");
                    uploadRequest.Credentials = new NetworkCredential(ftpAccountDetail.Username, ftpAccountDetail.Password);
                    uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    uploadRequest.ContentLength = bytes.Length;
                    using (Stream ftpStream = uploadRequest.GetRequestStream())
                    {
                        await ftpStream.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
            }
        }
        public object AddCompanyInformation(CompanyLookUp companyLookUp)
        {
            try
            {
                foreach (var lookup in companyLookUp.CompanyInfoLookUps)
                {
                    var companyData = Context.Companies.FirstOrDefault(x => x.Id == lookup.Id);
                    if (companyData != null)
                    {

                        // Context.CompanyLicenses.AddRange(companyLicenseList);
                        companyData.Id = lookup.Id;
                        companyData.CreatedDate = lookup.CreatedDate;
                        companyData.Blocked = lookup.Blocked;
                        companyData.LogoPath = lookup.LogoPath;
                        companyData.HouseNumber = lookup.HouseNumber;
                        companyData.CompanyRegNo = lookup.CompanyRegNo;
                        companyData.NameEnglish = lookup.NameEnglish;
                        companyData.NameArabic = lookup.NameArabic;
                        companyData.VatRegistrationNo = lookup.VatRegistrationNo;
                        companyData.CompanyEmail = lookup.CompanyEmail;
                        companyData.CityEnglish = lookup.CityEnglish;
                        companyData.CityArabic = lookup.CityArabic;
                        companyData.CountryEnglish = lookup.CountryEnglish;
                        companyData.CountryArabic = lookup.CountryArabic;
                        companyData.CategoryInEnglish = lookup.CategoryInEnglish;
                        companyData.CategoryInArabic = lookup.CategoryInArabic;
                        companyData.AddressEnglish = lookup.AddressEnglish;
                        companyData.AddressArabic = lookup.AddressArabic;
                        companyData.PhoneNo = lookup.PhoneNo;
                        companyData.Landline = lookup.Landline;
                        companyData.Website = lookup.Website;
                        companyData.Postcode = lookup.Postcode;
                        companyData.Town = lookup.Town;
                        companyData.ClientNo = lookup.ClientNo;
                        companyData.ParentId = lookup.ParentId;
                        companyData.BusinessParentId = lookup.BusinessParentId;
                        companyData.ClientParentId = lookup.ClientParentId;
                        companyData.CompanyNameEnglish = lookup.CompanyNameEnglish;
                        companyData.CompanyNameArabic = lookup.CompanyNameArabic;

                    }
                    else
                    {
                        var companyLicenseList = new List<CompanyLicense>();
                        if (companyLookUp.CompanyLicenseLookUps.Count > 0 &&
                            companyLookUp.CompanyLicenseLookUps.ElementAt(0).CompanyId == lookup.Id)
                        {
                            foreach (var license in companyLookUp.CompanyLicenseLookUps)
                            {
                                companyLicenseList.Add(new CompanyLicense
                                {
                                    FromDate = license.FromDate,
                                    ToDate = license.ToDate,
                                    IsBlock = license.IsBlock,
                                    IsActive = license.IsActive,
                                    CompanyId = license.CompanyId,
                                });
                            }

                            Context.CompanyLicenses.AddRange(companyLicenseList);
                        }

                        var company = new Company()
                        {
                            Id = lookup.Id,
                            CreatedDate = lookup.CreatedDate,
                            Blocked = lookup.Blocked,
                            LogoPath = lookup.LogoPath,
                            HouseNumber = lookup.HouseNumber,
                            CompanyRegNo = lookup.CompanyRegNo,
                            NameEnglish = lookup.NameEnglish,
                            NameArabic = lookup.NameArabic,
                            VatRegistrationNo = lookup.VatRegistrationNo,
                            CompanyEmail = lookup.CompanyEmail,
                            CityEnglish = lookup.CityEnglish,
                            CityArabic = lookup.CityArabic,
                            CountryEnglish = lookup.CountryEnglish,
                            CountryArabic = lookup.CountryArabic,
                            CategoryInEnglish = lookup.CategoryInEnglish,
                            CategoryInArabic = lookup.CategoryInArabic,
                            AddressEnglish = lookup.AddressEnglish,
                            AddressArabic = lookup.AddressArabic,
                            PhoneNo = lookup.PhoneNo,
                            Landline = lookup.Landline,
                            Website = lookup.Website,
                            Postcode = lookup.Postcode,
                            Town = lookup.Town,
                            ClientNo = lookup.ClientNo,
                            ParentId = lookup.ParentId,
                            BusinessParentId = lookup.BusinessParentId,
                            ClientParentId = lookup.ClientParentId,
                            CompanyNameEnglish = lookup.CompanyNameEnglish,
                            CompanyNameArabic = lookup.CompanyNameArabic,
                        };
                        Context.Companies.Add(company);

                    }
                }
                Context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Fail-" + e.Message;
            }
        }

        public object GetCompanyList(string searchTerm, int? pageNumber)
        {
            try
            {
                var companyData = Context.Companies.AsNoTracking().Include(x => x.NobleGroup).Include(x => x.CompanyLicenses).ToList();


                var companyInfoLookUpList = new List<CompanyInfoLookUp>();
                foreach (var company in companyData)
                {
                    var lookUp = new CompanyInfoLookUp()
                    {
                        Id = company.Id,
                        CreatedDate = company.CreatedDate,
                        Blocked = company.Blocked,
                        LogoPath = company.LogoPath,
                        HouseNumber = company.HouseNumber,
                        CompanyRegNo = company.CompanyRegNo,
                        NameEnglish = company.NameEnglish,
                        NameArabic = company.NameArabic,
                        VatRegistrationNo = company.VatRegistrationNo,
                        CompanyEmail = company.CompanyEmail,
                        CityEnglish = company.CityEnglish,
                        CityArabic = company.CityArabic,
                        CountryEnglish = company.CountryEnglish,
                        CountryArabic = company.CountryArabic,
                        CategoryInEnglish = company.CategoryInEnglish,
                        CategoryInArabic = company.CategoryInArabic,
                        AddressEnglish = company.AddressEnglish,
                        AddressArabic = company.AddressArabic,
                        PhoneNo = company.PhoneNo,
                        Landline = company.Landline,
                        Website = company.Website,
                        Postcode = company.Postcode,
                        Town = company.Town,
                        ClientNo = company.ClientNo,
                        ParentId = company.ParentId,
                        ClientParentId = company.ClientParentId,
                        BusinessParentId = company.BusinessParentId,
                        CompanyNameEnglish = company.CompanyNameEnglish,
                        CompanyNameArabic = company.CompanyNameArabic,
                        NobleGroupId = company.NobleGroupId,
                        LicenseType = company.CompanyLicenses.Count > 0
                            ? company.CompanyLicenses.LastOrDefault()?.LicenseType
                            : "",
                        GroupName = company.NobleGroup == null ? "" : company.NobleGroup.GroupName + '-' + Enum.GetName(typeof(GroupType), company.NobleGroup.GroupType),
                        EndDate = company.CompanyLicenses.Count > 0
                            ? company.CompanyLicenses.LastOrDefault()?.ToDate.ToString("d")
                            : "",
                        TechnicalSupportPeriod = company.CompanyLicenses.Count > 0
                            ? company.CompanyLicenses.LastOrDefault()?.TechnicalSupportPeriod
                            : "",
                        IsTechnicalSupport = company.CompanyLicenses.Count > 0 && (company.CompanyLicenses.LastOrDefault().IsTechnicalSupport || company.CompanyLicenses.LastOrDefault().IsUpdateTechnicalSupport),
                        CompanyLicenseLookUps = company.CompanyLicenses.Select(x =>
                            new CompanyLicenseLookUp()
                            {
                                FromDate = x.FromDate,
                                ToDate = x.ToDate,
                                IsTechnicalSupport = x.IsTechnicalSupport,
                                IsUpdateTechnicalSupport = x.IsUpdateTechnicalSupport,
                                LicenseType = x.LicenseType,
                                PaymentFrequency = x.PaymentFrequency,
                                IsActive = x.IsActive,
                                IsBlock = x.IsBlock

                            }).ToList()
                    };
                    companyInfoLookUpList.Add(lookUp);
                }
                return new GetCompanyDataLookUp()
                {
                    Companies = companyInfoLookUpList.Where(x =>
                        x.ParentId != Guid.Empty && x.ClientParentId == null && x.BusinessParentId == null).ToList(),
                    Businesses = companyInfoLookUpList.Where(x => x.ParentId != Guid.Empty && x.ClientParentId != null && x.BusinessParentId == null).ToList(),
                    Locations = companyInfoLookUpList.Where(x => x.ParentId != Guid.Empty && x.ClientParentId != null && x.BusinessParentId != null).ToList(),
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException(e.Message);

            }
        }

        public object CompanyLicensing(CompanyLicenseLookUp companyLicenseLookUp)
        {
            try
            {

                var company = Context.Companies.FirstOrDefault(x => x.Id == companyLicenseLookUp.CompanyId);

                //Add Company Permission
                if (company is { NobleGroupId: null })
                {
                    var noblePermission =
                        Context.NoblePermissions.Where(x => x.NobleGroupId == companyLicenseLookUp.NobleGroupId);
                    var companyPermissionList = new List<CompanyPermission>();
                    foreach (var permission in noblePermission)
                    {
                        companyPermissionList.Add(new CompanyPermission()
                        {
                            Key = permission.Key,
                            NobleGroupId = permission.NobleGroupId,
                            NobleModuleId = permission.NobleModuleId,
                            PermissionName = permission.PermissionName,
                            Value = permission.Value,
                            CompanyId = companyLicenseLookUp.CompanyId
                        });
                    }
                    Context.CompanyPermissions.AddRange(companyPermissionList);
                }
                else if (company != null && company.NobleGroupId != companyLicenseLookUp.NobleGroupId)
                {
                    var permissionList = Context.CompanyPermissions
                        .Where(x => x.CompanyId == companyLicenseLookUp.CompanyId).ToList();
                    if (permissionList.Count > 0) Context.CompanyPermissions.RemoveRange(permissionList);
                    var noblePermission =
                        Context.NoblePermissions.Where(x => x.NobleGroupId == companyLicenseLookUp.NobleGroupId);
                    var companyPermissionData = new List<CompanyPermission>();
                    foreach (var permission in noblePermission)
                    {
                        companyPermissionData.Add(new CompanyPermission()
                        {
                            Key = permission.Key,
                            NobleGroupId = permission.NobleGroupId,
                            NobleModuleId = permission.NobleModuleId,
                            PermissionName = permission.PermissionName,
                            Value = permission.Value,
                            CompanyId = companyLicenseLookUp.CompanyId
                        });
                    }
                    Context.CompanyPermissions.AddRange(companyPermissionData);
                }

                //Update Noble Group id in company Table
                if (company != null) company.NobleGroupId = companyLicenseLookUp.NobleGroupId;

                //Add New License 
                var licenseLookUp = new CompanyLicense
                {
                    FromDate = companyLicenseLookUp.FromDate,
                    ToDate = companyLicenseLookUp.ToDate,
                    IsBlock = companyLicenseLookUp.IsBlock,
                    IsActive = companyLicenseLookUp.IsActive,
                    CompanyId = companyLicenseLookUp.CompanyId,
                    LicenseType = companyLicenseLookUp.LicenseType,
                    IsTechnicalSupport = companyLicenseLookUp.IsTechnicalSupport,
                    IsUpdateTechnicalSupport = companyLicenseLookUp.IsUpdateTechnicalSupport,
                    TechnicalSupportPeriod = companyLicenseLookUp.TechnicalSupportPeriod,
                    PaymentFrequency = companyLicenseLookUp.PaymentFrequency,
                    GracePeriod = companyLicenseLookUp.GracePeriod,
                    ActivationPlatform = (ActivationPlatform)Enum.Parse(typeof(ActivationPlatform), companyLicenseLookUp.ActivationPlatform),
                };
                Context.CompanyLicenses.Add(licenseLookUp);
                Context.SaveChanges();
                return "Success";



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Fail-" + e.Message;
            }
        }

        public object GetLicenseDetail(Guid companyId)
        {
            try
            {
                var license = Context.CompanyLicenses.Include(x => x.Company).ToList().LastOrDefault(x => x.CompanyId == companyId);

                //return companyData;

                if (license != null)
                    return new CompanyLicenseLookUp()
                    {
                        CompanyId = license.CompanyId,
                        NobleGroupId = license.Company.NobleGroupId ?? Guid.Empty,
                        ToDate = license.ToDate,
                        FromDate = license.FromDate,
                        IsActive = license.IsActive,
                        IsBlock = license.IsBlock,
                        LicenseType = license.LicenseType,
                        IsUpdateTechnicalSupport = license.IsUpdateTechnicalSupport,
                        IsTechnicalSupport = license.IsTechnicalSupport,
                        TechnicalSupportPeriod = license.TechnicalSupportPeriod,
                        PaymentFrequency = license.PaymentFrequency,
                        GracePeriod = license.GracePeriod,
                        ActivationPlatform = Enum.GetName(typeof(ActivationPlatform), license.ActivationPlatform),
                    };
                return new CompanyLicenseLookUp();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException(e.Message);

            }
        }

        public object GetCompanyPermissionById(Guid id)
        {
            try
            {
                var data = Context.CompanyPermissions.AsNoTracking().Where(x => x.CompanyId == id).ToList();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public object DisActiveLicense(Guid companyId, bool isCompany)
        {
            try
            {
                var companyLocationList = isCompany ? Context.Companies.Where(x => x.ClientParentId == companyId).ToList()
                    : Context.Companies.Where(x => x.BusinessParentId == companyId).ToList();
                foreach (var location in companyLocationList)
                {
                    var license = Context.CompanyLicenses.ToList().LastOrDefault(x => x.CompanyId == location.Id);
                    if (license != null)
                    {
                        license.IsActive = false;
                        license.GracePeriod = false;
                    }
                }

                Context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string SaveCompanyPermissions(NoblePermissionLookUp noblePermissionLook)
        {
            try
            {
                //Add Modules if not exist in database
                var moduleList = Context.NobleModules.Select(x => x.Id).ToList();
                if (moduleList.Count != noblePermissionLook.Modules.Count)
                {
                    foreach (var module in noblePermissionLook.Modules)
                    {
                        if (!moduleList.Contains(module.Id))
                        {
                            var nobleModule = new NobleModule()
                            {
                                Id = module.Id,
                                Name = module.Name,
                                Description = module.Description
                            };
                            Context.NobleModules.Add(nobleModule);
                        }
                    }


                }
                //Add Permission
                var isExistCompanyData = Context.CompanyPermissions.Where(x => x.CompanyId == noblePermissionLook.CompanyId).ToList();
                if (isExistCompanyData.Count > 0)
                    Context.CompanyPermissions.RemoveRange(isExistCompanyData);

                // Get Company Permission of this group

                foreach (var permission in noblePermissionLook.Permissions)
                {

                    if (permission.Checked)
                    {
                        var per = new CompanyPermission()
                        {
                            PermissionName = permission.PermissionName,
                            Key = permission.Key,
                            Value = permission.Value,
                            NobleGroupId = noblePermissionLook.GroupId,
                            NobleModuleId = permission.ModuleId,
                            CompanyId = noblePermissionLook.CompanyId.Value
                        };
                        Context.CompanyPermissions.Add(per);
                    }
                }
                Context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.Message;
            }
        }

        public WhiteLabelLookUp GetWhiteLabelingData()
        {
            try
            {
                var whiteLabeling = Context.WhiteLabellings.FirstOrDefault();
                if (whiteLabeling == null)
                    throw new ApplicationException("Please insert white labeling record first");


                var lookUp = new WhiteLabelLookUp();
                lookUp.TagImage1Path =  !string.IsNullOrEmpty(whiteLabeling.TagImage1Path) ? GetBase64Image(whiteLabeling.TagImage1Path).Result : "";
                lookUp.TagImage1Name = !string.IsNullOrEmpty(whiteLabeling.TagImage1Path) ? Path.GetFileName(whiteLabeling.TagImage1Path) : "";

                lookUp.LoginScreenImagePath = !string.IsNullOrEmpty(whiteLabeling.LoginScreenImagePath) ? GetBase64Image(whiteLabeling.LoginScreenImagePath).Result : "";
                lookUp.LoginScreenImageName = !string.IsNullOrEmpty(whiteLabeling.LoginScreenImagePath) ? Path.GetFileName(whiteLabeling.LoginScreenImagePath) : "";

                lookUp.LoginLogoPath = !string.IsNullOrEmpty(whiteLabeling.LoginLogoPath) ? GetBase64Image(whiteLabeling.LoginLogoPath).Result : "";
                lookUp.LoginLogoName = !string.IsNullOrEmpty(whiteLabeling.LoginLogoPath) ? Path.GetFileName(whiteLabeling.LoginLogoPath) : "";

                lookUp.BackgroundImagePath = !string.IsNullOrEmpty(whiteLabeling.BackgroundImagePath) ? GetBase64Image(whiteLabeling.BackgroundImagePath).Result : "";
                lookUp.BackgroundImageName = !string.IsNullOrEmpty(whiteLabeling.BackgroundImagePath) ? Path.GetFileName(whiteLabeling.BackgroundImagePath) : "";

                lookUp.SidebarImagePath = !string.IsNullOrEmpty(whiteLabeling.SidebarImagePath) ? GetBase64Image(whiteLabeling.SidebarImagePath).Result : "";
                lookUp.SidebarImageName = !string.IsNullOrEmpty(whiteLabeling.SidebarImagePath) ? Path.GetFileName(whiteLabeling.SidebarImagePath) : "";

                lookUp.FavIconPath = !string.IsNullOrEmpty(whiteLabeling.FavIconPath) ? GetBase64Image(whiteLabeling.FavIconPath).Result : "";
                lookUp.FavIconName = !string.IsNullOrEmpty(whiteLabeling.FavIconPath) ? Path.GetFileName(whiteLabeling.FavIconPath) : "";

                lookUp.SideMenuColor = whiteLabeling.SideMenuColor;
                lookUp.SideMenuBtnColor = whiteLabeling.SideMenuBtnColor;
                lookUp.SideMenuBtnClickColor = whiteLabeling.SideMenuBtnClickColor;
                lookUp.SaveBtnBgColor = whiteLabeling.SaveBtnBgColor;
                lookUp.SaveBtnColor = whiteLabeling.SaveBtnColor;
                lookUp.CancelBgBtnColor = whiteLabeling.CancelBgBtnColor;
                lookUp.CancelBtnColor = whiteLabeling.CancelBtnColor;
                lookUp.HeadingColor = whiteLabeling.HeadingColor;
                lookUp.TableHeaderBgColor = whiteLabeling.TableHeaderBgColor;
                lookUp.TableHeaderColor = whiteLabeling.TableHeaderColor;
                lookUp.InvoiceTitleBgColor = whiteLabeling.InvoiceTitleBgColor;
                lookUp.InvoiceTitleColor = whiteLabeling.InvoiceTitleColor;

                lookUp.ApplicationBgColor = whiteLabeling.ApplicationBgColor;
                lookUp.ApplicationTextColor = whiteLabeling.ApplicationTextColor;
                lookUp.CardBgColor = whiteLabeling.CardBgColor;
                lookUp.CardTextColor = whiteLabeling.CardTextColor;
                lookUp.SetupBgColor = whiteLabeling.SetupBgColor;
                lookUp.SetupTextColor = whiteLabeling.SetupTextColor;
                lookUp.SetupCssFilter = whiteLabeling.SetupCssFilter;
                lookUp.SideMenuBtnColorFilter = whiteLabeling.SideMenuBtnColorFilter;
                lookUp.SideMenuBtnClickColorFilter = whiteLabeling.SideMenuBtnClickColorFilter;
                lookUp.SideMenuBtnClickBgColor = whiteLabeling.SideMenuBtnClickBgColor;
                return lookUp;
            }
            catch
            {
                throw new ApplicationException("Something went wrong");
            }
        }

        public async Task<string> GetBase64Image(string fileName)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + fileName;

            if (File.Exists(path))
            {
                var bytes = await File.ReadAllBytesAsync(path);
                return Convert.ToBase64String(bytes);


            }
            return "";
        }
    }
}
