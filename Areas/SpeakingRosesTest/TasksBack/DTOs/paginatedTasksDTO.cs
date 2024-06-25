using SpeakingRosesTest.Areas.CMS.UserBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Entities;

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

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.DTOs
{
    public class paginatedTasksDTO
    {
        public List<Tasks?> lstTasks { get; set; }
        public List<User?> lstUserCreation { get; set; }
        public List<User?> lstUserLastModification { get; set; }

        //FOREIGN LISTS (TABLES)
        public List<Priority?> lstPriority { get; set; }
        public List<Status?> lstStatus { get; set; }
        

        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}