using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
            SprintTaskViewModel searchViewModel=new SprintTaskViewModel();
            int StatusFilter = GetStatusFlag(taskSearchModel);
            if (taskSearchModel != null)
            {
                searchViewModel = await SearchBy(searchViewModel, taskSearchModel, StatusFilter);
                searchViewModel = OrderBy(searchViewModel, taskSearchModel);
            }
            searchViewModel.Total = searchViewModel.sprintTasks != null ? searchViewModel.sprintTasks.Count : 0;
            searchViewModel.PageSize = taskSearchModel!=null ? taskSearchModel.PazeSize:10;
            searchViewModel.PageNo = taskSearchModel != null ? taskSearchModel.PageNo:1;
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
                        searchViewModel = await DefaultSearch(taskSearchModel, StatusFilter);
                       break;
                }

            }
            else
            {
                searchViewModel= await DefaultSearch(taskSearchModel, StatusFilter);
            }
            return searchViewModel;
        }
        private async Task<SprintTaskViewModel> DefaultSearch(TaskSearchModel taskSearchModel, int StatusFilter)
        {
            SprintTaskViewModel searchTitleViewModel = new SprintTaskViewModel();
            if(StatusFilter!=0)
            {
                searchTitleViewModel.sprintTasks=await _context.SprintTask.Where(e => e.Status == StatusFilter).ToListAsync();
            }
            else
            {
                searchTitleViewModel.sprintTasks = await _context.SprintTask.ToListAsync();
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
                    searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.Title.Contains(taskSearchModel.SearchValue)).ToListAsync();
                }
                else
                {
                    searchTitleViewModel.sprintTasks =await _context.SprintTask.Where(e => e.Title.Contains(taskSearchModel.SearchValue) && e.Status == StatusFilter).ToListAsync();
                }
                
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
                    searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.Description.Contains(taskSearchModel.SearchValue)).ToListAsync();
                }
                else
                {
                    searchTitleViewModel.sprintTasks = await _context.SprintTask.Where(e => e.Description.Contains(taskSearchModel.SearchValue) && e.Status == StatusFilter).ToListAsync();
                }

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
            SprintTaskViewModel searchTitleViewModel = new SprintTaskViewModel();
            if (taskSearchModel != null)
            {
                switch (!string.IsNullOrEmpty(taskSearchModel.SortBy) ?taskSearchModel.SortBy.ToLower(): TaskConstant.OrderByDesc)
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
            return searchTitleViewModel;
        }
        private SprintTaskViewModel OrderByDescription(SprintTaskViewModel searchViewModel, TaskSearchModel taskSearchModel)
        {
            SprintTaskViewModel searchTitleViewModel = new SprintTaskViewModel();
            if (taskSearchModel != null)
            {
                switch (!string.IsNullOrEmpty(taskSearchModel.SortBy) ? taskSearchModel.SortBy.ToLower() : TaskConstant.OrderByDesc)
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
            return searchTitleViewModel;
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
