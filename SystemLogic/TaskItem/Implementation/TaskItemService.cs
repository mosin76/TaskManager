using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.TaskItem.API.Common;
using System.TaskItem.API.Model;
using System.TaskItem.API.Model.ApplicationModel;
using TaskManagmentAPI.SystemLogic.TaskItem.Interface;

namespace TaskManagmentAPI.SystemLogic.TaskItem.Implementation
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDbContext _context;
        public TaskItemService(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<SprintTaskViewModel> GetAllTaskAsync(TaskSearchModel taskSearchModel)
        {
           return await GetAllTaskBySearch(taskSearchModel);
        }
        public async Task<SprintTask?> GetTaskByIdAsync(int id)
        {
            return await _context.SprintTask.FindAsync(id);
        }
        public async Task<bool?> UpdateSprintTaskAsync(SprintTask sprintTask)
        {
            _context.Entry(sprintTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool?> SaveSprintTaskAsync(SprintTask sprintTask)
        {
            _context.SprintTask.Add(sprintTask);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool?> DeleteSprintTaskAsync(int id)
        {
            var sprintTask = await _context.SprintTask.FindAsync(id);
            if (sprintTask == null)
            {
                return false;
            }
            _context.SprintTask.Remove(sprintTask);
            await _context.SaveChangesAsync();
            return true;
        }
        private bool SprintTaskExist(int id)
        {
            return _context.SprintTask.Any(e => e.TaskId == id);
        }

        private async Task<SprintTaskViewModel> GetAllTaskBySearch(TaskSearchModel taskSearchModel)
        {
            int pageSize = !string.IsNullOrEmpty(taskSearchModel.PazeSize) ? Convert.ToInt16(taskSearchModel.PazeSize) : 10;
            int pageNo = taskSearchModel != null && !string.IsNullOrEmpty(taskSearchModel.PageNo) ? Convert.ToInt16(taskSearchModel.PageNo) : 1;
            SprintTaskViewModel searchViewModel=new SprintTaskViewModel();
            int statusfilter = !string.IsNullOrEmpty(taskSearchModel.Status) ? Convert.ToInt32(taskSearchModel.Status) : 0;
            if (taskSearchModel != null)
            {
                searchViewModel = await SearchBy(searchViewModel, taskSearchModel, statusfilter);
                searchViewModel = OrderBy(searchViewModel, taskSearchModel);
            }
            searchViewModel.Total = searchViewModel.sprintTasks != null ? searchViewModel.sprintTasks.Count : 0;
            if (searchViewModel.sprintTasks != null)
                searchViewModel.sprintTasks = searchViewModel.sprintTasks
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize).ToList();
           
            searchViewModel.PageSize = pageSize;
            searchViewModel.PageNo = pageNo; 
            searchViewModel.Success = true;
           
            return searchViewModel;
        }
        private async Task<SprintTaskViewModel> SearchBy(SprintTaskViewModel searchViewModel,TaskSearchModel taskSearchModel, int StatusFilter)
        {
           
            if (!string.IsNullOrEmpty(taskSearchModel.SearchFor) && !string.IsNullOrEmpty(taskSearchModel.SearchValue))
            {
                switch (taskSearchModel.SearchFor.ToLower())
                {
                    case TaskConstant.SearchTitle:
                        searchViewModel = await SearchByTitle(taskSearchModel, StatusFilter);
                        break;
                    case TaskConstant.SearchDesciption:
                        searchViewModel = await SearchByDescription(taskSearchModel, StatusFilter);
                        break;
                    default:
                        searchViewModel = await DefaultSearch(taskSearchModel);
                       break;
                }

            }
            else
            {
                searchViewModel= await DefaultSearch(taskSearchModel);
            }
            return searchViewModel;
        }
        private async Task<SprintTaskViewModel> DefaultSearch(TaskSearchModel taskSearchModel)
        {
            SprintTaskViewModel searchTitleViewModel = new SprintTaskViewModel();
            if(!string.IsNullOrEmpty(taskSearchModel.Status))
            {
                int statusfilter = Convert.ToInt32(taskSearchModel.Status);
                searchTitleViewModel.sprintTasks=await _context.SprintTask.Where(e => e.Status == statusfilter && e.UserId== taskSearchModel.userId).ToListAsync();
            }
            else
            {
                searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e=>e.UserId== taskSearchModel.userId).ToListAsync();
            }
            return searchTitleViewModel;
        }
        private async Task<SprintTaskViewModel> SearchByTitle(TaskSearchModel taskSearchModel, int StatusFilter)
        {
            SprintTaskViewModel searchTitleViewModel = new SprintTaskViewModel();
            if(taskSearchModel.SearchValue!=null)
            {
                if (StatusFilter == 0)
                {
                    searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.Title.Contains(taskSearchModel.SearchValue) && e.UserId== taskSearchModel.userId).ToListAsync();
                }
                else
                {
                    searchTitleViewModel.sprintTasks =await _context.SprintTask.Where(e => e.Title.Contains(taskSearchModel.SearchValue) && e.Status == StatusFilter && e.UserId == taskSearchModel.userId).ToListAsync();
                }
                
            }
            else
            {
                searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e=>e.UserId== taskSearchModel.userId).ToListAsync();
            }
            return searchTitleViewModel;
        }
        private async Task<SprintTaskViewModel> SearchByDescription(TaskSearchModel taskSearchModel, int StatusFilter)
        {
            SprintTaskViewModel searchTitleViewModel = new SprintTaskViewModel();
            if (taskSearchModel.SearchValue != null)
            {
                if (StatusFilter == 0)
                {
                    searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.Description.Contains(taskSearchModel.SearchValue) && e.UserId == taskSearchModel.userId).ToListAsync();
                }
                else
                {
                    searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.Description.Contains(taskSearchModel.SearchValue) && e.Status == StatusFilter && e.UserId == taskSearchModel.userId).ToListAsync();
                }

            }
            else
            {
                searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.UserId == taskSearchModel.userId).ToListAsync();
            }
            return searchTitleViewModel;
        }
        private SprintTaskViewModel OrderBy(SprintTaskViewModel searchViewModel, TaskSearchModel taskSearchModel)
        {
            if (!string.IsNullOrEmpty(taskSearchModel.SortBy))
            {
                switch (taskSearchModel.SortBy.ToLower())
                {
                    case TaskConstant.SearchTitle:
                        searchViewModel = OrderByTitle(searchViewModel,taskSearchModel);
                        break;
                    case TaskConstant.SearchDesciption:
                        searchViewModel = OrderByDescription(searchViewModel, taskSearchModel);
                        break;
                    default:
                        searchViewModel = OrderByTitle(searchViewModel, taskSearchModel);
                        break;
                }

            }
            else
            {
                searchViewModel = OrderByTitle(searchViewModel, taskSearchModel);
            }
            return searchViewModel;
        }
        private SprintTaskViewModel OrderByTitle(SprintTaskViewModel searchViewModel,TaskSearchModel taskSearchModel)
        {
            if (taskSearchModel != null)
            {
                switch (!string.IsNullOrEmpty(taskSearchModel.SortOrder) ?taskSearchModel.SortOrder.ToLower(): TaskConstant.OrderByAsc)
                {
                    case TaskConstant.OrderByDesc:
                        searchViewModel.sprintTasks = searchViewModel.sprintTasks.OrderByDescending(a => a.Title).ToList();
                        break;
                    case TaskConstant.OrderByAsc:
                        searchViewModel.sprintTasks = searchViewModel.sprintTasks.OrderBy(a => a.Title).ToList(); ;
                        break;
                    default:
                        searchViewModel.sprintTasks = searchViewModel.sprintTasks.OrderByDescending(a => a.Title).ToList();                        break;
                }
            }
            return searchViewModel;
        }
        private SprintTaskViewModel OrderByDescription(SprintTaskViewModel searchViewModel, TaskSearchModel taskSearchModel)
        {
            if (taskSearchModel != null)
            {
                switch (!string.IsNullOrEmpty(taskSearchModel.SortOrder) ? taskSearchModel.SortOrder.ToLower() : TaskConstant.OrderByDesc)
                {
                    case TaskConstant.OrderByDesc:
                        searchViewModel.sprintTasks = searchViewModel.sprintTasks.OrderByDescending(a => a.Description).ToList();
                        break;
                    case TaskConstant.OrderByAsc:
                        searchViewModel.sprintTasks = searchViewModel.sprintTasks.OrderBy(a => a.Description).ToList(); ;
                        break;
                    default:
                        searchViewModel.sprintTasks = searchViewModel.sprintTasks.OrderByDescending(a => a.Description).ToList(); break;
                }
            }
            return searchViewModel;
        }
        private int GetStatusFlag(TaskSearchModel taskSearchModel) 
        {
            int StatusFlag = 0;
            if(!string.IsNullOrEmpty(taskSearchModel.Status))
            {
                if (taskSearchModel.Status == nameof(SprintTaskStatus.ToDo))
                    StatusFlag = 1;
                if (taskSearchModel.Status == nameof(SprintTaskStatus.InProgress))
                    StatusFlag = 2;
                if (taskSearchModel.Status == nameof(SprintTaskStatus.Done))
                    StatusFlag = 3;
            }
            return StatusFlag;
        }
    }
}
