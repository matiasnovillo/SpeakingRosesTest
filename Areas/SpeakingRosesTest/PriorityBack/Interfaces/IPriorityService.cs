using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Entities;
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
    public interface IPriorityService
    {
        void ExportToExcel(string path, DataTable dtPriority);

        void ExportToCSV(string path, List<Priority> lstPriority);

        void ExportToPDF(string path, List<Priority> lstPriority);

        List<Priority> ImportExcel(string path);
    }
}