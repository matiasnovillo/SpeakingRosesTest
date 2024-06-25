using Microsoft.EntityFrameworkCore;
using SpeakingRosesTest.Areas.CMS.UserBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.DTOs;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Interfaces;
using SpeakingRosesTest.DatabaseContexts;
using System.Text.RegularExpressions;
using System.Data;

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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        protected readonly TaskDBContext _context;

        public PriorityRepository(TaskDBContext context)
        {
            _context = context;
        }

        public IQueryable<Priority> AsQueryable()
        {
            try
            {
                return _context.Priority.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Priority.Count();
            }
            catch (Exception) { throw; }
        }

        public Priority? GetByPriorityId(int priorityId)
        {
            try
            {
                return _context.Priority
                            .FirstOrDefault(x => x.PriorityId == priorityId);
            }
            catch (Exception) { throw; }
        }

        public List<Priority?> GetAll()
        {
            try
            {
                return _context.Priority.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<Priority> GetAllByPriorityIdForModal(string textToSearch)
        {
            try
            {
                var query = from priority in _context.Priority
                            select new { Priority = priority};

                // Extraemos los resultados en listas separadas
                List<Priority> lstPriority = query.Select(result => result.Priority)
                        .Where(x => x.PriorityId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstPriority;
            }
            catch (Exception) { throw; }
        }

        public List<Priority?> GetAllByPriorityId(List<int> lstPriorityChecked)
        {
            try
            {
                List<Priority?> lstPriority = [];

                foreach (int PriorityId in lstPriorityChecked)
                {
                    Priority priority = _context.Priority.Where(x => x.PriorityId == PriorityId).FirstOrDefault();

                    if (priority != null)
                    {
                        lstPriority.Add(priority);
                    }
                }

                return lstPriority;
            }
            catch (Exception) { throw; }
        }

        public paginatedPriorityDTO GetAllByNamePaginated(string textToSearch,
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

                int TotalPriority = _context.Priority.Count();

                List<Priority> lstPriority = _context.Priority
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.Name.Contains(word)) :
                            words.Any(word => x.Name.Contains(word)))
                        .OrderBy(x => x.Name)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (Priority priority in lstPriority)
                {

                    User UserCreation = new() 
                    { 
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        Email = "",
                        Password = "",
                        ProfilePicture = "",
                        RoleId = 1,
                        UserCreationId = 1,
                        UserId = 1,
                        UserLastModificationId = 1
                    };

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = new()
                    {
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        Email = "",
                        Password = "",
                        ProfilePicture = "",
                        RoleId = 1,
                        UserCreationId = 1,
                        UserId = 1,
                        UserLastModificationId = 1
                    };

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedPriorityDTO
                {
                    lstPriority = lstPriority,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalPriority,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Priority priority)
        {
            try
            {
                _context.Priority.Add(priority);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Priority priority)
        {
            try
            {
                _context.Priority.Update(priority);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByPriorityId(int priorityId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.PriorityId == priorityId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByPriorityIdInDataTable(List<int> lstPriorityChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("PriorityId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (int PriorityId in lstPriorityChecked)
                {
                    Priority priority = _context.Priority.Where(x => x.PriorityId == PriorityId).FirstOrDefault();

                    if (priority != null)
                    {
                        DataTable.Rows.Add(
                        priority.PriorityId,
                        priority.Active,
                        priority.DateTimeCreation,
                        priority.DateTimeLastModification,
                        priority.UserCreationId,
                        priority.UserLastModificationId,
                        priority.Name
                        
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
                List<Priority> lstPriority = _context.Priority.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("PriorityId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (Priority priority in lstPriority)
                {
                    DataTable.Rows.Add(
                        priority.PriorityId,
                        priority.Active,
                        priority.DateTimeCreation,
                        priority.DateTimeLastModification,
                        priority.UserCreationId,
                        priority.UserLastModificationId,
                        priority.Name
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
