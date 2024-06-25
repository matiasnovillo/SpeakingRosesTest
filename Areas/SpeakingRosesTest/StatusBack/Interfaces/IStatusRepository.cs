using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.DTOs;
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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Interfaces
{
    public interface IStatusRepository
    {
        IQueryable<Status> AsQueryable();

        #region Queries
        int Count();

        Status? GetByStatusId(int statusId);

        List<Status?> GetAll();

        List<Status?> GetAllByStatusId(List<int> lstStatusChecked);

        List<Status> GetAllByStatusIdForModal(string textToSearch);

        paginatedStatusDTO GetAllByStatusIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(Status status);

        bool Update(Status status);

        bool DeleteByStatusId(int status);
        #endregion

        #region Methods for DataTable
        DataTable GetAllByStatusIdInDataTable(List<int> lstStatusChecked);

        DataTable GetAllInDataTable();
        #endregion
    }
}
