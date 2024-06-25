using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.DTOs;
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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Interfaces
{
    public interface ITasksRepository
    {
        IQueryable<Tasks> AsQueryable();

        #region Queries
        int Count();

        Tasks? GetByTasksId(int tasksId);

        List<Tasks?> GetAll();

        List<Tasks?> GetAllByTasksId(List<int> lstTasksChecked);

        List<Tasks> GetAllByTasksIdForModal(string textToSearch);

        paginatedTasksDTO GetAllByTasksIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(Tasks tasks);

        bool Update(Tasks tasks);

        bool DeleteByTasksId(int tasks);
        #endregion

        #region Methods for DataTable
        DataTable GetAllByTasksIdInDataTable(List<int> lstTasksChecked);

        DataTable GetAllInDataTable();
        #endregion
    }
}
