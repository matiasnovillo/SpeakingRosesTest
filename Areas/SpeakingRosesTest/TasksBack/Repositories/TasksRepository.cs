using Microsoft.EntityFrameworkCore;
using SpeakingRosesTest.Areas.CMS.UserBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.DTOs;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Interfaces;
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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        protected readonly TaskDBContext _context;

        public TasksRepository(TaskDBContext context)
        {
            _context = context;
        }

        public IQueryable<Tasks> AsQueryable()
        {
            try
            {
                return _context.Tasks.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Tasks.Count();
            }
            catch (Exception) { throw; }
        }

        public Tasks? GetByTasksId(int tasksId)
        {
            try
            {
                return _context.Tasks
                            .FirstOrDefault(x => x.TasksId == tasksId);
            }
            catch (Exception) { throw; }
        }

        public List<Tasks?> GetAll()
        {
            try
            {
                return _context.Tasks.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<Tasks> GetAllByTasksIdForModal(string textToSearch)
        {
            try
            {
                var query = from tasks in _context.Tasks
                            select new { Tasks = tasks};

                // Extraemos los resultados en listas separadas
                List<Tasks> lstTasks = query.Select(result => result.Tasks)
                        .Where(x => x.TasksId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstTasks;
            }
            catch (Exception) { throw; }
        }

        public List<Tasks?> GetAllByTasksId(List<int> lstTasksChecked)
        {
            try
            {
                List<Tasks?> lstTasks = [];

                foreach (int TasksId in lstTasksChecked)
                {
                    Tasks tasks = _context.Tasks.Where(x => x.TasksId == TasksId).FirstOrDefault();

                    if (tasks != null)
                    {
                        lstTasks.Add(tasks);
                    }
                }

                return lstTasks;
            }
            catch (Exception) { throw; }
        }

        public paginatedTasksDTO GetAllByTasksIdPaginated(string textToSearch,
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

                int TotalTasks = _context.Tasks.Count();

                List<Tasks> lstTasks = _context.Tasks
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.TasksId.ToString().Contains(word)) :
                            words.Any(word => x.TasksId.ToString().Contains(word)))
                        .OrderByDescending(x => x.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (Tasks tasks in lstTasks)
                {

                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == tasks.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == tasks.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedTasksDTO
                {
                    lstTasks = lstTasks,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalTasks,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Tasks tasks)
        {
            try
            {
                _context.Tasks.Add(tasks);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Tasks tasks)
        {
            try
            {
                _context.Tasks.Update(tasks);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByTasksId(int tasksId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.TasksId == tasksId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByTasksIdInDataTable(List<int> lstTasksChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("TasksId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Title", typeof(string));
                DataTable.Columns.Add("Description", typeof(string));
                DataTable.Columns.Add("PriorityId", typeof(string));
                DataTable.Columns.Add("DueDate", typeof(string));
                DataTable.Columns.Add("StatusId", typeof(string));
                

                foreach (int TasksId in lstTasksChecked)
                {
                    Tasks tasks = _context.Tasks.Where(x => x.TasksId == TasksId).FirstOrDefault();

                    if (tasks != null)
                    {
                        DataTable.Rows.Add(
                        tasks.TasksId,
                        tasks.Active,
                        tasks.DateTimeCreation,
                        tasks.DateTimeLastModification,
                        tasks.UserCreationId,
                        tasks.UserLastModificationId,
                        tasks.Title,
                        tasks.Description,
                        tasks.PriorityId,
                        tasks.DueDate,
                        tasks.StatusId
                        
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
                List<Tasks> lstTasks = _context.Tasks.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("TasksId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Title", typeof(string));
                DataTable.Columns.Add("Description", typeof(string));
                DataTable.Columns.Add("PriorityId", typeof(string));
                DataTable.Columns.Add("DueDate", typeof(string));
                DataTable.Columns.Add("StatusId", typeof(string));
                

                foreach (Tasks tasks in lstTasks)
                {
                    DataTable.Rows.Add(
                        tasks.TasksId,
                        tasks.Active,
                        tasks.DateTimeCreation,
                        tasks.DateTimeLastModification,
                        tasks.UserCreationId,
                        tasks.UserLastModificationId,
                        tasks.Title,
                        tasks.Description,
                        tasks.PriorityId,
                        tasks.DueDate,
                        tasks.StatusId
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
