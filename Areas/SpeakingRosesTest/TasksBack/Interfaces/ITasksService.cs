using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Entities;
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
    public interface ITasksService
    {
        void ExportToExcel(string path, DataTable dtTasks);

        void ExportToCSV(string path, List<Tasks> lstTasks);

        void ExportToPDF(string path, List<Tasks> lstTasks);

        List<Tasks> ImportExcel(string path);
    }
}