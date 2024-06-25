using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.DTOs;
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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Interfaces
{
    public interface IPriorityRepository
    {
        IQueryable<Priority> AsQueryable();

        #region Queries
        int Count();

        Priority? GetByPriorityId(int priorityId);

        List<Priority?> GetAll();

        List<Priority?> GetAllByPriorityId(List<int> lstPriorityChecked);

        List<Priority> GetAllByPriorityIdForModal(string textToSearch);

        paginatedPriorityDTO GetAllByNamePaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(Priority priority);

        bool Update(Priority priority);

        bool DeleteByPriorityId(int priority);
        #endregion

        #region Methods for DataTable
        DataTable GetAllByPriorityIdInDataTable(List<int> lstPriorityChecked);

        DataTable GetAllInDataTable();
        #endregion
    }
}
