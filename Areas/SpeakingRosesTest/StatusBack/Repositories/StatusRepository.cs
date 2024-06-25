using Microsoft.EntityFrameworkCore;
using SpeakingRosesTest.Areas.CMS.UserBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.DTOs;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Interfaces;
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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        protected readonly TaskDBContext _context;

        public StatusRepository(TaskDBContext context)
        {
            _context = context;
        }

        public IQueryable<Status> AsQueryable()
        {
            try
            {
                return _context.Status.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Status.Count();
            }
            catch (Exception) { throw; }
        }

        public Status? GetByStatusId(int statusId)
        {
            try
            {
                return _context.Status
                            .FirstOrDefault(x => x.StatusId == statusId);
            }
            catch (Exception) { throw; }
        }

        public List<Status?> GetAll()
        {
            try
            {
                return _context.Status.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<Status> GetAllByStatusIdForModal(string textToSearch)
        {
            try
            {
                var query = from status in _context.Status
                            select new { Status = status};

                // Extraemos los resultados en listas separadas
                List<Status> lstStatus = query.Select(result => result.Status)
                        .Where(x => x.StatusId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstStatus;
            }
            catch (Exception) { throw; }
        }

        public List<Status?> GetAllByStatusId(List<int> lstStatusChecked)
        {
            try
            {
                List<Status?> lstStatus = [];

                foreach (int StatusId in lstStatusChecked)
                {
                    Status status = _context.Status.Where(x => x.StatusId == StatusId).FirstOrDefault();

                    if (status != null)
                    {
                        lstStatus.Add(status);
                    }
                }

                return lstStatus;
            }
            catch (Exception) { throw; }
        }

        public paginatedStatusDTO GetAllByStatusIdPaginated(string textToSearch,
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

                int TotalStatus = _context.Status.Count();

                List<Status> lstStatus = _context.Status
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.StatusId.ToString().Contains(word)) :
                            words.Any(word => x.StatusId.ToString().Contains(word)))
                        .OrderByDescending(x => x.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (Status status in lstStatus)
                {

                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == status.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == status.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedStatusDTO
                {
                    lstStatus = lstStatus,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalStatus,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Status status)
        {
            try
            {
                _context.Status.Add(status);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Status status)
        {
            try
            {
                _context.Status.Update(status);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByStatusId(int statusId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.StatusId == statusId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByStatusIdInDataTable(List<int> lstStatusChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("StatusId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (int StatusId in lstStatusChecked)
                {
                    Status status = _context.Status.Where(x => x.StatusId == StatusId).FirstOrDefault();

                    if (status != null)
                    {
                        DataTable.Rows.Add(
                        status.StatusId,
                        status.Active,
                        status.DateTimeCreation,
                        status.DateTimeLastModification,
                        status.UserCreationId,
                        status.UserLastModificationId,
                        status.Name
                        
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
                List<Status> lstStatus = _context.Status.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("StatusId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (Status status in lstStatus)
                {
                    DataTable.Rows.Add(
                        status.StatusId,
                        status.Active,
                        status.DateTimeCreation,
                        status.DateTimeLastModification,
                        status.UserCreationId,
                        status.UserLastModificationId,
                        status.Name
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
