using Microsoft.EntityFrameworkCore;
using SpeakingRosesTest.DatabaseContexts;
using System.Text.RegularExpressions;
using System.Data;
using SpeakingRosesTest.Areas.CMS.UserBack.Entities;
using SpeakingRosesTest.Areas.CMS.MenuBack.DTOs;
using SpeakingRosesTest.Areas.CMS.MenuBack.Entities;
using SpeakingRosesTest.Areas.CMS.RoleMenuBack.DTOs;
using SpeakingRosesTest.Areas.CMS.RoleMenuBack.Entities;
using SpeakingRosesTest.Areas.CMS.RoleMenuBack.Interfaces;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace SpeakingRosesTest.Areas.CMS.RoleMenuBack.Repositories
{
    public class RoleMenuRepository : IRoleMenuRepository
    {
        protected readonly TaskDBContext _context;

        public RoleMenuRepository(TaskDBContext context)
        {
            _context = context;
        }

        public IQueryable<RoleMenu> AsQueryable()
        {
            try
            {
                return _context.RoleMenu.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.RoleMenu.Count();
            }
            catch (Exception) { throw; }
        }

        public RoleMenu? GetByRoleMenuId(int rolemenuId)
        {
            try
            {
                return _context.RoleMenu
                            .FirstOrDefault(x => x.RoleMenuId == rolemenuId);
            }
            catch (Exception) { throw; }
        }

        public List<RoleMenu?> GetAll()
        {
            try
            {
                return _context.RoleMenu.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<RoleMenu> GetAllByRoleMenuIdForModal(string textToSearch)
        {
            try
            {
                var query = from rolemenu in _context.RoleMenu
                            select new { RoleMenu = rolemenu };

                // Extraemos los resultados en listas separadas
                List<RoleMenu> lstRoleMenu = query.Select(result => result.RoleMenu)
                        .Where(x => x.RoleMenuId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstRoleMenu;
            }
            catch (Exception) { throw; }
        }

        public List<RoleMenu?> GetAllByRoleMenuId(List<int> lstRoleMenuChecked)
        {
            try
            {
                List<RoleMenu?> lstRoleMenu = [];

                foreach (int RoleMenuId in lstRoleMenuChecked)
                {
                    RoleMenu rolemenu = _context.RoleMenu.Where(x => x.RoleMenuId == RoleMenuId).FirstOrDefault();

                    if (rolemenu != null)
                    {
                        lstRoleMenu.Add(rolemenu);
                    }
                }

                return lstRoleMenu;
            }
            catch (Exception) { throw; }
        }

        public paginatedRoleMenuDTO GetAllByRoleMenuIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            try
            {
                //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
                string[] words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                int TotalRoleMenu = _context.RoleMenu.Count();

                List<RoleMenu> lstRoleMenu = _context.RoleMenu
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.RoleMenuId.ToString().Contains(word)) :
                            words.Any(word => x.RoleMenuId.ToString().Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (RoleMenu rolemenu in lstRoleMenu)
                {
                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == rolemenu.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == rolemenu.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedRoleMenuDTO
                {
                    lstRoleMenu = lstRoleMenu,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalRoleMenu,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }

        public List<Menu> GetAllByRoleId(int roleId, List<Menu> lstMenu)
        {
            try
            {
                List<RoleMenu> lstRoleMenu = GetAll();

                var lstMenuResult = (from rm in lstRoleMenu
                                     where rm.RoleId == roleId
                                     join m in lstMenu on rm.MenuId equals m.MenuId
                                     select m)
                                          .OrderBy(x => x.Order)
                                          .ToList();

                return lstMenuResult;
            }
            catch (Exception) { throw; }
        }

        public List<Menu> GetAllByRoleIdAndPathForPermission(int roleId, string path)
        {
            try
            {
                var query = from rolemenu in _context.RoleMenu
                            where rolemenu.RoleId == roleId
                            join menu in _context.Menu on rolemenu.MenuId equals menu.MenuId
                            where menu.URLPath == path
                            select new
                            {
                                Menu = menu,
                            };

                List<Menu> lstMenu = query.Select(result => result.Menu).ToList();

                return lstMenu;
            }
            catch (Exception) { throw; }
        }

        public List<folderForDashboard> GetAllPagesAndFoldersForDashboardByRoleId(int roleId)
        {
            try
            {
                List<itemForFolderForDashboard> menuItems = [];

                List<Menu> lstMenu = (from rm in _context.RoleMenu
                                      where rm.RoleId == roleId
                                      join m in _context.Menu on rm.MenuId equals m.MenuId
                                      select m)
                                          .OrderBy(x => x.Order)
                                          .ToList();

                foreach (Menu menu in lstMenu)
                {
                    itemForFolderForDashboard item = new itemForFolderForDashboard
                    {
                        MenuId = menu.MenuId,
                        Name = menu.Name,
                        MenuFatherId = menu.MenuFatherId,
                        Order = menu.Order,
                        URLPath = menu.URLPath,
                        IconURLPath = menu.IconURLPath,
                        Active = menu.Active,
                        UserCreationId = menu.UserCreationId,
                        UserLastModificationId = menu.UserLastModificationId,
                        DateTimeCreation = menu.DateTimeCreation,
                        DateTimeLastModification = menu.DateTimeLastModification
                    };
                    menuItems.Add(item);
                }

                var foldersAndPages = menuItems.Where(item => item.MenuFatherId == 0).Select(folder => new folderForDashboard
                {
                    Folder = folder,
                    Pages = menuItems.Where(page => page.MenuFatherId == folder.MenuId).ToList()
                }).ToList();

                return foldersAndPages;
            }
            catch (Exception) { throw; }

        }

        public List<menuWithStateDTO> GetAllWithStateByRoleId(int roleId, List<Menu> lstMenu)
        {
            try
            {
                List<RoleMenu> lstRoleMenu = GetAll();

                var lstMenuWithState = lstMenu
                    .Select(menu =>
                        new menuWithStateDTO
                        {
                            MenuId = menu.MenuId,
                            Name = menu.Name,
                            MenuFatherId = menu.MenuFatherId,
                            Order = menu.Order,
                            URLPath = menu.URLPath,
                            IconURLPath = menu.IconURLPath,
                            Active = menu.Active,
                            IsSelected = lstRoleMenu
                                .Any(rm => rm.RoleId == roleId && rm.MenuId == menu.MenuId)
                        }
                    ).ToList();

                return lstMenuWithState;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(RoleMenu rolemenu)
        {
            try
            {
                _context.RoleMenu.Add(rolemenu);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(RoleMenu rolemenu)
        {
            try
            {
                _context.RoleMenu.Update(rolemenu);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByRoleMenuId(int rolemenuId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.RoleMenuId == rolemenuId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByMenuIdAndRoleId(int roleId,
            int menuId)
        {
            try
            {
                AsQueryable()
                .Where(x => x.MenuId == menuId)
                .Where(x => x.RoleId == roleId)
                .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByRoleMenuIdInDataTable(List<int> lstRoleMenuChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("RoleMenuId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("MenuId", typeof(string));
                DataTable.Columns.Add("RoleId", typeof(string));


                foreach (int RoleMenuId in lstRoleMenuChecked)
                {
                    RoleMenu rolemenu = _context.RoleMenu.Where(x => x.RoleMenuId == RoleMenuId).FirstOrDefault();

                    if (rolemenu != null)
                    {
                        DataTable.Rows.Add(
                        rolemenu.RoleMenuId,
                        rolemenu.Active,
                        rolemenu.DateTimeCreation,
                        rolemenu.DateTimeLastModification,
                        rolemenu.UserCreationId,
                        rolemenu.UserLastModificationId,
                        rolemenu.MenuId,
                        rolemenu.RoleId

                        );
                    }
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }

        public DataTable GetAllInDataTable()
        {
            try
            {
                List<RoleMenu> lstRoleMenu = _context.RoleMenu.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("RoleMenuId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("MenuId", typeof(string));
                DataTable.Columns.Add("RoleId", typeof(string));


                foreach (RoleMenu rolemenu in lstRoleMenu)
                {
                    DataTable.Rows.Add(
                        rolemenu.RoleMenuId,
                        rolemenu.Active,
                        rolemenu.DateTimeCreation,
                        rolemenu.DateTimeLastModification,
                        rolemenu.UserCreationId,
                        rolemenu.UserLastModificationId,
                        rolemenu.MenuId,
                        rolemenu.RoleId

                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
